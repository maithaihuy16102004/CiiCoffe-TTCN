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
            InitializeComponent();
            this.Text = "Nhận Diện Khuôn Mặt";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Emgu.CV.UI.ImageBox imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            imageBoxFrameGrabber.Size = new Size(800, 500);
            imageBoxFrameGrabber.Location = new Point(10, 10);
            imageBoxFrameGrabber.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(imageBoxFrameGrabber);

            faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
            recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 200);

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

        private void LoadUserMappings()
        {
            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
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

        private void TrainAndSaveRecognizer()
        {
            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
            List<Image<Gray, byte>> trainingFaces = new List<Image<Gray, byte>>();
            List<int> labels = new List<int>();
            int labelCounter = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT TAIKHOAN.MaNV, NHANVIEN.HinhAnh FROM TAIKHOAN INNER JOIN NHANVIEN ON TAIKHOAN.MaNV = NHANVIEN.MaNV";
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
                                    labelToTenDangNhap[labelCounter] = reader.GetString(0); // MaNV
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
                recognizer.Write(modelPath);  // Kiểm tra xem tệp có được lưu thành công không
                Console.WriteLine("Model saved successfully.");

            }
        }

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

                if (labelToTenDangNhap.ContainsKey(result.Label))
                {
                    string username = labelToTenDangNhap[result.Label];
                    string role = labelToLoaiTaiKhoan[result.Label];
                    Console.WriteLine($"User: {username}, Role: {role}");  // Log user info
                    LoginByFace?.Invoke(username, role);
                    this.Invoke(new Action(() => this.Close()));
                }
                else
                {
                    // Log if label not found
                    Console.WriteLine($"Label {result.Label} not found in dictionary.");
                }

            }

            imageBoxFrameGrabber.Image = image;
        }

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

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void FrmNhanDien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= ProcessFrame;
            capture?.Dispose();
        }
    }
}
