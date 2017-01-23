using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace PaintSomething
{
    public partial class PaintSomething : Form
    {
        Color paintcolor = System.Drawing.Color.Black;
        Color paintcolor2;
        bool draw = false;
        int x, y, lx, ly = 0;
        Item currentItem;

        public PaintSomething()
        {
            Thread t = new Thread(new ThreadStart(SplashScreen));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
        }

        public void SplashScreen()
        {
            Application.Run(new Form2());
        }

        public enum Item
        {
            Rectangle, Ellipse, Line, Text, Brush, Eraser
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            x = e.X;
            y = e.Y;

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = e.X;
            ly = e.Y;
            
            if(currentItem == Item.Line){
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x,y), new Point(lx,ly));
                g.Dispose();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                Graphics g = pictureBox1.CreateGraphics();
                switch (currentItem)
                {
                    
                    case Item.Rectangle:
                        g.FillRectangle(new SolidBrush(paintcolor), x, y, e.X - x, e.Y - y);
                        break;

                    case Item.Ellipse:
                        g.FillEllipse(new SolidBrush(paintcolor), x, y, e.X - x , e.Y - y);
                        break;
                        
                    
                    case Item.Brush:
                        g.FillEllipse(new SolidBrush(paintcolor), e.X - x + x, e.Y - y + y, 
                            Convert.ToInt32(toolStripTextBox1.Text), Convert.ToInt32(toolStripTextBox1.Text));
                        break;

                    case Item.Eraser:
                        g.FillEllipse(new SolidBrush(pictureBox1.BackColor), e.X - x + x, e.Y - y + y,
                            Convert.ToInt32(toolStripTextBox1.Text), Convert.ToInt32(toolStripTextBox1.Text));
                        break;

                    case Item.Text:
                        break;
                }
                g.Dispose();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentItem = Item.Line;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.paintcolor = colorDialog1.Color;
            pictureBox2.BackColor = paintcolor;
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            currentItem = Item.Rectangle;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            currentItem = Item.Ellipse;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            currentItem = Item.Brush;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentItem = Item.Eraser;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        DialogResult result3 = MessageBox.Show("Ertu viss um að þú viljir gera nýja mynd?",
        "Bíddu aðeins!",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button2);
            
            if (result3 == DialogResult.Yes)
            {
                pictureBox1.Refresh();
            }
            
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Png files|*.png|jpeg files|*.jpg";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image = (Image)Image.FromFile(open.FileName).Clone();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = pictureBox1.RectangleToScreen(pictureBox1.ClientRectangle);
            g.CopyFromScreen(rect.Location, Point.Empty, pictureBox1.Size);
            g.Dispose();
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Png files|*.png|jpeg files|*.jpg";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(File.Exists(save.FileName)){
                    File.Delete(save.FileName);
                }
                if (save.FileName.Contains(".jpg"))
                {
                    bmp.Save(save.FileName, ImageFormat.Jpeg);
                }
                else if(save.FileName.Contains(".png")){
                    bmp.Save(save.FileName, ImageFormat.Png);
                }
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void umForritToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog(); 
        }

        private void hjálpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            this.paintcolor2 = colorDialog2.Color;
            pictureBox1.BackColor = paintcolor2;
        }

    }
}
