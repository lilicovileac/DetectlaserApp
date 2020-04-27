using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DetectLaserApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (capture == null)
            {
                try
                {
                    capture = new VideoCapture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            if (capture != null)
            {
                if (captureInProgress)
                {  //if camera is getting frames then stop the capture and set button Text
                   // "Start" for resuming capture
                    btnStart.Text = "Start!"; //
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    //if camera is NOT getting frames then start the capture and set button
                    // Text to "Stop" for pausing capture
                    btnStart.Text = "Stop";
                    Application.Idle += ProcessFrame;
                }
                captureInProgress = !captureInProgress;
            }

        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        private VideoCapture capture;  //takes images from camera as image frames
        private bool captureInProgress;

        int[] red = { 140, 140, 255 };
        int[] green = { 0, 90, 100, 255, 0, 90 };
        int[] blue = { 90, 255, 0, 80, 0, 80 };
        int[] colorToDetect = new int[6];
        bool drawing = false;
        Point prevPoint = new Point();
        Point currentPoint = new Point();
        Bgr currentColor;
        String currentCol;
        double threshold = 25;
        Bitmap drawBox = new Bitmap(640, 480);
        Graphics g;
        Point origin = new Point(0, 0);

        Pen pen = new Pen(Color.Black, 1);

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Mat mat = capture.QueryFrame();
            Image<Bgr, Byte> ImageFrame = mat.ToImage<Bgr, Byte>();
            DetectPix(ImageFrame);
        }

        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentCol = comboBoxColor.Text;

            if (currentCol == "Blue") colorToDetect = blue;
            else if (currentCol == "Green") colorToDetect = green;
            else colorToDetect = red;
        }

        public void DetectPix(Image<Bgr, byte> imageFrame)
        {
            int wdh = imageFrame.Width; //640
            int hgt = imageFrame.Height; //480

            float avgX = 0;
            float avgY = 0;

            int count = 0;
            for (int i = 1; i < (wdh - 4); i = i + 3)
            {
                for (int j = 1; j < (hgt - 4); j = j + 3)
                {
                    currentColor = imageFrame[j, i];

                    //due to the bad quality of the camera, red lazer will look as an extremly white dot(as it it extremly light) with a light pink circle arround them, 
                    //so during the night i use the light pink as the detected color and during the day i will use the white dot.
                    //int red = 255;
                    //int blue = 140;
                    //int green = 140; //light pink
                    int red = 255;
                    int blue = 255;
                    int green = 255;  //white

                    double dist = Math.Sqrt(Math.Pow(red - currentColor.Red, 2) + Math.Pow(green - currentColor.Green, 2) + Math.Pow(blue - currentColor.Blue, 2));

                    if (dist < threshold)
                    {
                        avgX += i;
                        avgY += j;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                avgX = avgX / count;
                avgY = avgY / count;
                currentPoint.X = (int)avgX;
                currentPoint.Y = (int)avgY;

            }

            if (!drawing)
            {
                if (count > 0)
                {
                    SetCursorPos(currentPoint.X + Location.X, currentPoint.Y + Location.Y);
                }

            }
            else
            {
                if (count == 0)
                {
                    prevPoint = origin;
                }
                if (count > 0)
                {


                    if (prevPoint == origin)
                    {
                        prevPoint = currentPoint;
                    }

                    g.DrawLine(pen, prevPoint, currentPoint);
                    prevPoint = currentPoint;
                    //DrawPictureBox.Image = drawBox;
                }
            }
            DrawPictureBox.Image = drawBox;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //setting the detected color to red by default
            colorToDetect = red;

            g = Graphics.FromImage(drawBox);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            if (!drawing)
            {
                DrawGroupBox.Visible = true;
                drawing = true;
                g.Flush();
                g.Clear(Color.White);
                //g = Graphics.FromImage(drawBox);
                BtnDraw.Text = "Stop Drawing";
            }
            else
            {
                DrawGroupBox.Visible = false;
                drawing = false;
                g.Flush();
                g.Clear(Color.White);
                BtnDraw.Text = "Start Drawing";
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                if (!drawing)
                {
                    drawing = true;
                    g.Flush();
                    g.Clear(Color.White);
                }
                else
                {
                    drawing = false;
                    g.Flush();
                    g.Clear(Color.White);

                }
            }
        }

        private void saveImageBTN_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.DrawPictureBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.DrawPictureBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.DrawPictureBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }


        private void colorBox_Click(object sender, EventArgs e)
        {

            colorDialog1.ShowDialog();
            pen.Color = colorDialog1.Color;
            colorBox.BackColor = colorDialog1.Color;


        }

        private void setEraser_Click(object sender, EventArgs e)
        {
            pen.Color = Color.White;
            colorBox.BackColor = Color.White;
        }

        private void penThickness_Scroll(object sender, EventArgs e)
        {
            pen.Width = penThickness.Value;
        }

        private void openImgBTN_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open Image";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap btm = new Bitmap(openFileDialog.FileName);
                    g.DrawImage(btm, origin);
                }
            }
        }
    }
}