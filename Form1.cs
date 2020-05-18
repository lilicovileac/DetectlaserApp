using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace DetectLaserApp
{
    public partial class Form1 : Form
    {   

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        private VideoCapture capture;  //takes images from camera as image frames 640x480
        private bool captureInProgress;

        Bgr red = new Bgr(140, 140, 255);  // in EmguCV the order of the colour codes is Blue-Green-Red and not RGB. 
        Bgr white = new Bgr(255, 255, 255);
        Bgr green = new Bgr(128, 255, 128);
        Bgr blue = new Bgr(255, 204, 153);
        Bgr colorToDetect = new Bgr(0, 0, 0);
        bool drawing = false;
        Point prevPoint = new Point();
        Point currentPoint = new Point();
        Point allScreenPoint = new Point();
        Bgr currentPixel;
        String currentCol;
        double threshold = 25;
        double prevToCurrent;
        Bitmap drawBox = new Bitmap(640, 480);
        Graphics g;
        Point origin = new Point(0, 0);
        bool backgroundWhite = false;

        Rectangle Res = Screen.PrimaryScreen.Bounds;
        double widthM = 0;
        double heightM = 0;

        int timerCount = 0;


        Pen pen = new Pen(Color.Black, 1);
        public Form1()
        {
            InitializeComponent();

            if (capture == null)
            {
                try
                {
                    capture = new VideoCapture();
                    widthM = (double)Res.Width / (double)capture.Width;
                    heightM = (double)Res.Height / (double)capture.Height;
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
            else if (currentCol == "White") colorToDetect = white;
            else colorToDetect = red;
        }

        public void DetectPix(Image<Bgr, byte> imageFrame)
        {
            int wdh = imageFrame.Width; //640
            int hgt = imageFrame.Height; //480
            this.Cursor = new Cursor(Cursor.Current.Handle);

            float avgX = 0;
            float avgY = 0;

            int count = 0;
            for (int i = 1; i < (wdh - 4); i = i + 3)
            {
                for (int j = 1; j < (hgt - 4); j = j + 3)
                {
                    currentPixel = imageFrame[j, i];

                    double dist = Math.Sqrt(Math.Pow(colorToDetect.Red - currentPixel.Red, 2) + Math.Pow(colorToDetect.Green - currentPixel.Green, 2) + Math.Pow(colorToDetect.Blue - currentPixel.Blue, 2));

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
                if (prevPoint == origin)
                {
                    prevPoint = currentPoint;
                }

            }

            if (!drawing)
            {
                if (count > 0)
                {
                    //SetCursorPos((int)(currentPoint.X * widthM), (int)(currentPoint.Y * heightM));
                    //SetCursorPos(currentPoint.X ,  + Location.Y);
                    allScreenPoint.X = (int)(currentPoint.X * widthM);
                    allScreenPoint.Y = (int)(currentPoint.Y * heightM);
                    Cursor.Position = allScreenPoint;

                    prevToCurrent = Math.Sqrt(Math.Pow(prevPoint.X - currentPoint.X, 2) + Math.Pow(prevPoint.Y - currentPoint.Y, 2));
                    if (prevToCurrent <= 7)
                    {
                        if (!timer.Enabled) timer.Start();
                        if(timerCount >= 5)
                        {
                            sendMouseDoubleClick(allScreenPoint);
                            timer.Stop();
                            timerCount = 0;
                        }
                        else if(timerCount >= 3)
                        {
                            sendMouseDown(allScreenPoint);
                            
                        }
                    }
                    else
                    {
                        if (timer.Enabled) 
                        {
                            timer.Stop();
                            timerCount = 0;
                        }
                       
                    }

                   
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

                    g.DrawLine(pen, prevPoint, currentPoint);
                    
                    //DrawPictureBox.Image = drawBox;
                }
            }
            prevPoint = currentPoint;
            DrawPictureBox.Image = drawBox;
        }

        private void sendMouseDoubleClick(Point p)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);

            Thread.Sleep(150);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }
        private void sendMouseDown(Point p)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Snow; //setting the background colour to a unusual color 
            this.TransparencyKey = Color.Snow; // Sets to transparent everything that is Salmon coloured
            this.FormBorderStyle = FormBorderStyle.None;
            colorToDetect = white;    //setting the detected color to white by default
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            g = Graphics.FromImage(drawBox);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            if (!drawing)
            {
                DrawGroupBox.Visible = true;
                drawing = true;
                g.Flush();
                g.Clear(Color.Snow);
                //g = Graphics.FromImage(drawBox);
                BtnDraw.Text = "Stop Drawing";
            }
            else
            {
                DrawGroupBox.Visible = false;
                drawing = false;
                g.Flush();
                g.Clear(Color.Snow);
                BtnDraw.Text = "Start Drawing";
            }
        }

        private void saveImageBTN_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
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
            pen.Color = Color.Snow;
            colorBox.BackColor = Color.Snow;
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

        private void timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
        }

        private void pictureBoxBgrBTN_Click(object sender, EventArgs e)
        {
            string message = "You are about to change the background color of the Drawing box, this will reset the DrawingBox and remove everything in it. Do you want to change the background color? ";
            string caption = "You want to change the background color";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {

                if (!backgroundWhite)
                {
                    g.Flush();
                    g.Clear(Color.White);
                    pictureBoxBgrBTN.Text = "Change background to transparent";
                    backgroundWhite = true;
                }
                else
                {
                    g.Flush();
                    g.Clear(Color.Snow);
                    pictureBoxBgrBTN.Text = "Change Background to White";
                    backgroundWhite = false;
                }
            }
        }
    }
}