using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Threading;

namespace hairCam
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
        }

        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private bool drag = false;
        private int maxsize;
        private bool scale = false;
        private int countTime = 0;

        private void MainScreen_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            maxsize = FinalFrame.VideoCapabilities.Length-1;
            FinalFrame.VideoResolution = FinalFrame.VideoCapabilities[maxsize];
            FinalFrame.Start();
            panel2.BackColor = Color.FromArgb(40, Color.White);
            panel1.BackColor = Color.FromArgb(0, Color.White);
            pictureBox1.Parent = photoMain;
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.Top = (this.Height / 2) - (pictureBox1.Width / 2);
            pictureBox1.Left = (this.Width / 2) - (pictureBox1.Width / 2);
            panel3.Left = pictureBox1.Right;
            panel3.Top = pictureBox1.Bottom;
        }


        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            photoMain.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (FinalFrame.IsRunning)
            {

                FinalFrame.Stop();
                button3.Visible = true;
                button2.Visible = true;

                pictureBox1.BringToFront();
            }
            else
            {
                if (hairSelection.Visible) { hairSelection.Visible = false; }
                FinalFrame.Start();
                panel3.Visible = false;
                button3.Visible = false;
                button2.Visible = false;
                pictureBox1.Visible = false;
            }

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e){}
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) { drag = true; }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) { drag = false; }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                pictureBox1.Left += e.X - (pictureBox1.Width / 2);
                pictureBox1.Top += e.Y - (pictureBox1.Height / 2);
                panel3.Left = pictureBox1.Right;
                panel3.Top = pictureBox1.Bottom;
            }
        }

        private void scale_PictureBox1_MouseDown(object sender, MouseEventArgs e) { scale = true; }
        private void scale_PictureBox1_MouseUp(object sender, MouseEventArgs e) { scale = false; }
        private void scale_pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (scale)
            {
                pictureBox1.Height += e.Y;
                pictureBox1.Width += e.X;
                panel3.Left = pictureBox1.Right;
                panel3.Top = pictureBox1.Bottom;

                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (hairSelection.Visible)
            { hairSelection.Visible = false; } else { hairSelection.Visible = true; }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            hairSelection.Visible = false;

            //call timer
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        public void SetupPrintHandler()
        {
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(OnPrintPage);

            printDoc.Print();
        }

        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs args)
        {
            using (Image image = Image.FromFile(@"C:\Temp\printscreen.jpg"))
            {
                Graphics g = args.Graphics;
                g.DrawImage(image, args.PageBounds);
               // g.DrawImage(image, 0, 0);
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,bitmap.Size,CopyPixelOperation.SourceCopy);
            bitmap.Save(@"C:\Temp\printscreen.jpg", ImageFormat.Jpeg);
            SetupPrintHandler();
            timer1.Enabled = false;
            button1.Visible = true;
            button3.Visible = true;
            button2.Visible = true;
            panel2.Visible = true;

            if (pictureBox1.Visible) { panel3.Visible = true; }

        }

        private void changeImage(string path)
        {
            pictureBox1.ImageLocation = @path;
            pictureBox1.Visible = true;
            panel3.Visible = true;
            
        }

        private void hair3_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\3.png"); }
        private void hair4_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\4.png"); }
        private void hair5_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\5.png"); }
        private void hair6_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\6.png"); }
        private void hair7_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\7.png"); }
        private void hair8_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\8.png"); }
        private void hair9_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\9.png"); }
        private void hair10_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\10.png"); }
        private void hair11_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\11.png"); }
        private void hair12_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\12.png"); }
        private void hair13_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\13.png"); }
        private void hair14_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\14.png"); }
        private void hair18_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\18.png"); }
        private void hair20_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\20.png"); }
        private void hair21_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\21.png"); }
        private void hair22_Click(object sender, EventArgs e) { changeImage("C:\\Temp\\22.png"); }

        private void photoMain_Click(object sender, EventArgs e)
        {

        }
    }
}
