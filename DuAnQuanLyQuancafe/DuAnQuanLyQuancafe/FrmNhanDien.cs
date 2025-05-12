using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe
{
    public partial class FrmNhanDien : Form
    {

        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.AliceBlue;

        public event Action<string, string> LoginByFace;

        private VideoCapture capture;
        private CascadeClassifier faceDetector;
        private Mat frame = new Mat();
        private bool isCapturing = false;
        private bool isLoggedIn = false;
        private LBPHFaceRecognizer recognizer;
        private string modelPath = "trainedModel.yml";
        private Dictionary<int, string> labelToTenDangNhap = new Dictionary<int, string>();
        private Dictionary<int, string> labelToLoaiTaiKhoan = new Dictionary<int, string>();

        public FrmNhanDien()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(borderSize);
            this.BackColor = borderColor;

            InitializeComponent();
            this.Text = "Nhận Diện Khuôn Mặt";

            // Thiết lập ImageBox để hiển thị hình ảnh
            Emgu.CV.UI.ImageBox imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            imageBoxFrameGrabber.Size = new Size(800, 500);
            imageBoxFrameGrabber.Location = new Point(10, 10);
            imageBoxFrameGrabber.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(imageBoxFrameGrabber);

            // Khởi tạo bộ phát hiện khuôn mặt
            faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
            recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);

            // Kiểm tra nếu mô hình đã tồn tại, nếu không sẽ huấn luyện lại
            if (File.Exists(modelPath))
            {
                recognizer.Read(modelPath);
                LoadUserMappings();
            }
            else
            {
                TrainAndSaveRecognizer();
            }
        }

        // Tải dữ liệu từ cơ sở dữ liệu và ánh xạ với các label
        private void LoadUserMappings()
        {
            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TAIKHOAN.MaNV, TAIKHOAN.LoaiTaiKhoan FROM TAIKHOAN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int label = 0;
                            while (reader.Read())
                            {
                                labelToTenDangNhap[label] = reader.GetString(0); // MaNV
                                labelToLoaiTaiKhoan[label] = reader.GetString(1); // LoaiTaiKhoan
                                label++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu người dùng: {ex.Message}");
                // Log lỗi thay vì hiển thị popup
            }
        }

        // Huấn luyện lại mô hình nhận diện khuôn mặt và lưu trữ
        private void TrainAndSaveRecognizer()
        {
            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
            List<Image<Gray, byte>> trainingFaces = new List<Image<Gray, byte>>();
            List<int> labels = new List<int>();
            int labelCounter = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TAIKHOAN.MaNV, TAIKHOAN.LoaiTaiKhoan, NHANVIEN.HinhAnh 
                                     FROM TAIKHOAN 
                                     INNER JOIN NHANVIEN ON TAIKHOAN.MaNV = NHANVIEN.MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imageBytes = reader["HinhAnh"] as byte[];
                                if (imageBytes != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        Bitmap bmp = new Bitmap(ms);
                                        Image<Gray, byte> grayFace = bmp.ToImage<Gray, byte>().Resize(200, 200, Emgu.CV.CvEnum.Inter.Cubic);
                                        grayFace._EqualizeHist();

                                        trainingFaces.Add(grayFace);
                                        labels.Add(labelCounter);

                                        // Ghi ánh xạ label -> MaNV + Role
                                        labelToTenDangNhap[labelCounter] = reader.GetString(0);      // MaNV
                                        labelToLoaiTaiKhoan[labelCounter] = reader.GetString(1);     // LoaiTaiKhoan
                                        labelCounter++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (trainingFaces.Count > 0)
                {
                    VectorOfMat images = new VectorOfMat();
                    foreach (var face in trainingFaces)
                        images.Push(face);

                    VectorOfInt labelVec = new VectorOfInt(labels.ToArray());
                    recognizer.Train(images, labelVec);
                    recognizer.Write(modelPath);
                    Console.WriteLine("Đã train và lưu model nhận diện khuôn mặt.");
                }
                else
                {
                    MessageBox.Show("Không có ảnh nào để train!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi huấn luyện mô hình: {ex.Message}");
                // Log lỗi thay vì hiển thị popup
            }
        }

        // Xử lý từng frame để nhận diện khuôn mặt và thực hiện đăng nhập
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture == null || !capture.IsOpened) return;

            capture.Read(frame);
            if (frame.IsEmpty) return;

            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            Rectangle[] faces = faceDetector.DetectMultiScale(image, 1.1, 10, new Size(50, 50));

            foreach (Rectangle face in faces)
            {
                image.Draw(face, new Bgr(Color.Red), 2);
                Image<Gray, byte> gray = image.Copy(face).Convert<Gray, byte>().Resize(200, 200, Emgu.CV.CvEnum.Inter.Cubic);
                gray._EqualizeHist();

                var result = recognizer.Predict(gray);
                Console.WriteLine($"Predicted Label: {result.Label}, Distance: {result.Distance}");

                if (labelToTenDangNhap.ContainsKey(result.Label) && result.Distance < 100)
                {
                    string username = labelToTenDangNhap[result.Label];
                    string role = labelToLoaiTaiKhoan[result.Label];
                    Console.WriteLine($"User: {username}, Role: {role}");

                    // Gọi sự kiện đăng nhập thành công chỉ một lần
                    if (!isLoggedIn)
                    {
                        LoginByFace?.Invoke(username, role);
                        isLoggedIn = true; // Đảm bảo chỉ gọi 1 lần
                        //this.Invoke(new Action(() => this.Close()));
                    }
                }
                else
                {
                    Console.WriteLine($"Không nhận diện được hoặc label {result.Label} không khớp.");
                }
            }

            imageBoxFrameGrabber.Image = image;
        }

        // Bắt đầu hoặc dừng việc nhận diện khuôn mặt
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isCapturing)
            {
                capture = new VideoCapture(0);
                if (!capture.IsOpened)
                {
                    MessageBox.Show("Không mở được camera.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Application.Idle += ProcessFrame;
                isCapturing = true;
                btnStart.Text = "Stop";
            }
            else
            {
                Application.Idle -= ProcessFrame;
                capture?.Dispose();
                isCapturing = false;
                btnStart.Text = "Start";
                imageBoxFrameGrabber.Image = null;
            }
        }

        // Đóng form nhận diện khuôn mặt
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        // Xử lý khi đóng form
        private void FrmNhanDien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= ProcessFrame;
            capture?.Dispose();
        }
    }
}


