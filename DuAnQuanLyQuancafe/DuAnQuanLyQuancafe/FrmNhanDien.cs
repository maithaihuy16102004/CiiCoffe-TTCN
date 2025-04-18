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

        public FrmNhanDien()
        {
            InitializeComponent();

            this.Text = "Nhận Diện Khuôn Mặt";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox
            {
                Size = new Size(800, 500),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(imageBoxFrameGrabber);

            try
            {
                faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể load Haar Cascade: " + ex.Message);
            }

            recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 200);
        }

        private bool SoSanhKhuonMat(Image<Gray, byte> faceFromCamera, out string tenDangNhap, out string loaiTaiKhoan)
        {
            tenDangNhap = "";
            loaiTaiKhoan = "";

            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT TAIKHOAN.MaNV, TAIKHOAN.TenDangNhap, TAIKHOAN.LoaiTaiKhoan, NHANVIEN.HinhAnh 
                    FROM TAIKHOAN 
                    INNER JOIN NHANVIEN ON TAIKHOAN.MaNV = NHANVIEN.MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<Image<Gray, byte>> trainingFaces = new List<Image<Gray, byte>>();
                List<int> labels = new List<int>();

                Dictionary<int, string> labelToTenDangNhap = new Dictionary<int, string>();
                Dictionary<int, string> labelToLoaiTaiKhoan = new Dictionary<int, string>();

                int labelCounter = 0;

                while (reader.Read())
                {
                    byte[] imageBytes = reader["HinhAnh"] as byte[];
                    if (imageBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            Bitmap bitmap = new Bitmap(ms);
                            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(200, 200));
                            Image<Bgr, byte> img = resizedBitmap.ToImage<Bgr, byte>();
                            Image<Gray, byte> grayFace = img.Convert<Gray, byte>();
                            grayFace._EqualizeHist();

                            trainingFaces.Add(grayFace);
                            labels.Add(labelCounter);
                            labelToTenDangNhap[labelCounter] = reader.GetString(1); // TenDangNhap
                            labelToLoaiTaiKhoan[labelCounter] = reader.GetString(2); // LoaiTaiKhoan
                            labelCounter++;
                        }
                    }
                }

                if (trainingFaces.Count == 0 || labels.Count == 0)
                {
                    digThatBai.Show("Không có dữ liệu khuôn mặt để huấn luyện", "Thông báo");
                    return false;
                }

                using (VectorOfMat trainingImages = new VectorOfMat())
                {
                    foreach (var face in trainingFaces)
                    {
                        trainingImages.Push(face);
                    }

                    using (VectorOfInt trainingLabels = new VectorOfInt(labels.ToArray()))
                    {
                        recognizer.Train(trainingImages, trainingLabels);
                    }
                }

                var result = recognizer.Predict(faceFromCamera);

                if (result.Label >= 0 && result.Distance < 100)
                {
                    tenDangNhap = labelToTenDangNhap[result.Label];
                    loaiTaiKhoan = labelToLoaiTaiKhoan[result.Label];
                    return true;
                }

                return false;
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.IsOpened)
            {
                capture.Read(frame);
                if (frame.IsEmpty) return;

                Image<Bgr, Byte> image = frame.ToImage<Bgr, Byte>();
                Rectangle[] faces = faceDetector.DetectMultiScale(image, 1.1, 10, new Size(20, 20));

                foreach (Rectangle face in faces)
                {
                    image.Draw(face, new Bgr(Color.Red), 2);

                    Image<Gray, byte> faceFromCamera = image.Copy(face).Convert<Gray, byte>();
                    faceFromCamera = faceFromCamera.Resize(200, 200, Emgu.CV.CvEnum.Inter.Linear);
                    faceFromCamera._EqualizeHist();
                    faceFromCamera._GammaCorrect(2.0);

                    if (!isLoggedIn && SoSanhKhuonMat(faceFromCamera, out string tenDangNhap, out string loaiTaiKhoan))
                    {
                        isLoggedIn = true;

                        // Gọi sự kiện và đóng form
                        LoginByFace?.Invoke(tenDangNhap, loaiTaiKhoan);
                        this.Invoke(new Action(() =>
                        {
                            this.Close();
                        }));
                        break;
                    }
                }

                imageBoxFrameGrabber.Image = image;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isCapturing)
            {
                try
                {
                    capture = new VideoCapture(0);
                    if (!capture.IsOpened)
                    {
                        digThatBai.Show("Không thể mở camera", "Thông báo");
                        return;
                    }

                    Application.Idle += ProcessFrame;
                    isCapturing = true;
                    btnStart.Text = "Stop";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mở camera: " + ex.Message);
                }
            }
            else
            {
                Application.Idle -= ProcessFrame;
                capture?.Dispose();
                capture = null;
                isCapturing = false;
                btnStart.Text = "Start";
                imageBoxFrameGrabber.Image = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNhanDien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= ProcessFrame;
            capture?.Dispose();
        }
    }
}
