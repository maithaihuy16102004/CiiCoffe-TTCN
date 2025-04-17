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
        private LBPHFaceRecognizer recognizer;

        public FrmNhanDien()
        {
            InitializeComponent();

            this.Text = "Nhận Diện Khuôn Mặt";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox
            {
                Size = new Size(700, 500),
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
                    string maNV = reader.GetString(0);
                    string tenDangNhapDB = reader.GetString(1);
                    string loaiTaiKhoanDB = reader.GetString(2);
                    byte[] imageBytes = reader["HinhAnh"] as byte[];

                    if (imageBytes != null)
                    {
                        
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            Bitmap bitmap = new Bitmap(ms);
                            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(200, 200));
                            Image<Bgr, byte> img = new Image<Bgr, byte>(resizedBitmap.Width, resizedBitmap.Height);
                           // img.Bitmap = resizedBitmap;
                            Image<Gray, byte> grayFace = img.Convert<Gray, byte>();
                            grayFace._EqualizeHist();

                            trainingFaces.Add(grayFace);
                            labels.Add(labelCounter);
                            labelToTenDangNhap[labelCounter] = tenDangNhapDB;
                            labelToLoaiTaiKhoan[labelCounter] = loaiTaiKhoanDB;
                            labelCounter++;
                        }
                    }
                }

                if (trainingFaces.Count == 0 || labels.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu khuôn mặt để huấn luyện!");
                    return false;
                }
                //else
                //{
                //    MessageBox.Show($"Số lượng khuôn mặt huấn luyện: {trainingFaces.Count}");
                //}

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
                
                if (result.Label >= 0 && result.Distance < 700)
                {
                    tenDangNhap = labelToTenDangNhap[result.Label];
                    loaiTaiKhoan = labelToLoaiTaiKhoan[result.Label];
                    return true;
                }
                else
                {
                    MessageBox.Show("Không nhận diện được khuôn mặt! Vui lòng thử lại.");
                    return false;
                }
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.IsOpened)
            {
                capture.Read(frame);
                if (frame.IsEmpty)
                {
                    MessageBox.Show("Không đọc được frame từ camera!");
                    return;
                }

                Image<Bgr, Byte> image = frame.ToImage<Bgr, Byte>();
                Rectangle[] faces = faceDetector.DetectMultiScale(image, 1.1, 10, new Size(20, 20));
                foreach (Rectangle face in faces)
                {
                    image.Draw(face, new Bgr(Color.Red), 2);

                    Image<Gray, byte> faceFromCamera = image.Copy(face).Convert<Gray, byte>();
                    faceFromCamera = faceFromCamera.Resize(200, 200, Emgu.CV.CvEnum.Inter.Linear);
                    faceFromCamera._EqualizeHist();
                    faceFromCamera._GammaCorrect(2.0);

                    string tenDangNhap, loaiTaiKhoan;
                    if (SoSanhKhuonMat(faceFromCamera, out tenDangNhap, out loaiTaiKhoan))
                    {
                        LoginByFace?.Invoke(tenDangNhap, loaiTaiKhoan);
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
                        MessageBox.Show("Không mở được camera. Kiểm tra lại thiết bị!");
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
                if (capture != null)
                {
                    capture.Dispose();
                    capture = null;
                }
                isCapturing = false;
                btnStart.Text = "Start";
                imageBoxFrameGrabber.Image = null;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (capture != null)
            {
                capture.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void FrmNhanDien_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                capture.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}