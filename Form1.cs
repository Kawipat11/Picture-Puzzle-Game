using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int p = 0;
        int T;
        int[,] X = new int[4, 4];
        int[,] Y = new int[4, 4];
        string[,] NT = new string[4, 4];
        bool start = false;
        bool can_start = false;
        Random num = new Random();
        List<Point> list_p = new List<Point>();
        List<Point> list_p2 = new List<Point>();
        List<Point> list_p3 = new List<Point>();
        List<Point> list_p4 = new List<Point>();
        public void setting()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    X[i, j] = j * 75;
                    Y[i, j] = i * 75;
                    NT[i, j] =(i * 4 + j +1).ToString() ;
                }
            can_start = true;
        }
        public bool startt()
        {
            if (pictureBox1.Image == null)
            {
                return false;
            }
            else return true;
        }
        public void reset()
        {
            list_p3.Clear();
            T = 0;
            p = 0;
            list_p4.Clear();
            pictureBox2.Image = null;
            timer1.Stop();
            label1.Text = "time(sec) : " + T.ToString();
            start = false;
            setting();
            can_start = false;
            button3.Visible = true;
        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(startt())
            {
                Graphics G = e.Graphics;
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        Rectangle rect = new Rectangle(i * 75, j * 75, 75, 75);
                        Pen pennies = new Pen(Color.Black, 2);
                        G.DrawRectangle(pennies, rect);
                    }
                for (int k = 0; k < list_p3.Count; k++)
                {
                    Rectangle rect = new Rectangle(list_p3[k].X + 1, list_p3[k].Y + 1, 73, 73);
                    SolidBrush Brushies = new SolidBrush(Color.White);
                    G.FillRectangle(Brushies, rect);
                    SolidBrush Num_text = new SolidBrush(Color.Black);
                    G.DrawString(((list_p3[k].X) / 75 + 1 + 4 * (list_p3[k].Y) / 75).ToString(), Font, Num_text, new Point(list_p3[k].X + 36, list_p3[k].Y + 36));
                }
            }
        }
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (startt())
            {
                Graphics G = e.Graphics;
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        Rectangle rect = new Rectangle(i * 75, j * 75, 75, 75);
                        Pen pennies = new Pen(Color.Black, 2);
                        G.DrawRectangle(pennies, rect);
                        SolidBrush Num_text = new SolidBrush(Color.Black);
                        G.DrawString(NT[i,j], Font, Num_text, new Point(j * 75 + 37, i * 75 + 37));
                    }
            }
        }
        private void check()
        {
            Bitmap bm = (Bitmap)pictureBox2.Image;
            Bitmap bm2 = (Bitmap)pictureBox3.Image;
            if (list_p4.Count == 16)
            {
                for (int i = 0; i < pictureBox2.Width; i++)
                    for (int j = 0; j < pictureBox2.Height; j++)
                    {
                        Color c = bm.GetPixel(i, j);
                        Color c2 = bm2.GetPixel(i, j);
                        if (c.R == c2.R && c.G == c2.G && c.B == c2.B)
                        {
                            p++;
                        }
                    }
                if (p == 90000)
                {
                    timer1.Stop();
                    MessageBox.Show("YOU WINNNNN");
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("PLEASE TRY AGAIN");
                }
                p = 0;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            T++;
            label1.Text = "time(sec) : " + T.ToString();
            if (T == 120)
            {
                timer1.Stop();
                MessageBox.Show("GAME OVER");
            }
            check();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            reset();
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            Bitmap bm = new Bitmap(openFileDialog1.FileName);
            Bitmap bm2 = new Bitmap(bm, pictureBox1.Size);
            pictureBox1.Image = bm2;
            pictureBox3.Image = bm2;
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox3.Image;
            reset();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            timer1.Start();
            setting();
            Bitmap bm = (Bitmap)pictureBox1.Image;
            Bitmap bm2 = new Bitmap(bm.Width, bm.Height);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    Point tmp = new Point(i, j);
                    list_p.Add(tmp);
                    list_p2.Add(tmp);
                }
            while (list_p.Count > 0)
            {
                int NUM = num.Next(0, list_p.Count);
                Point tmp2 = new Point(list_p[NUM].X, list_p[NUM].Y);
                Point tmp3 = new Point(list_p2[0].X, list_p2[0].Y);
                list_p.Remove(tmp2);
                for (int i = 0; i < 75; i++)
                    for (int j = 0; j < 75; j++)
                    {
                        Color c = bm.GetPixel(i + tmp2.X * 75, j + tmp2.Y * 75);
                        bm2.SetPixel(i + tmp3.X * 75, j + tmp3.Y * 75, c);
                        list_p2.Remove(tmp3);
                    }
            }
            pictureBox1.Image = bm2;
            Refresh();
            button3.Visible = false;
        }
        private void back()
        {
            if (start)
            {
                list_p3.RemoveAt(list_p3.Count - 1);
            }
            else
            {
                if (list_p3.Count == 0) return;
                int a = list_p4.Count - 1;
                NT[list_p4[a].Y/75, list_p4[a].X/75] = (list_p4[a].Y /75* 4 + list_p4[a].X/75 + 1).ToString();
                list_p4.RemoveAt(list_p4.Count - 1);
                list_p3.RemoveAt(list_p3.Count - 1);
                display();
            }
            start = false;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            back();
            Refresh();
            
        }
        private void display()
        {
            Bitmap bm = (Bitmap)pictureBox1.Image;
            Bitmap bm2 = new Bitmap(bm.Width, bm.Height);
            for (int k = 0; k < list_p3.Count; k++)
            {
                for (int A2 = 0; A2 < 75; A2++)
                    for (int B = 0; B < 75; B++)
                    {
                        Color c = bm.GetPixel(list_p3[k].X + A2, list_p3[k].Y + B);
                        bm2.SetPixel(list_p4[k].X + A2, list_p4[k].Y + B, c);
                    }
            }
            pictureBox2.Image = bm2;
        }

        private void PictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (start)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        if (X[i, j] <= e.X && e.X < X[i, j] + 75 && Y[i, j] <= e.Y && e.Y < Y[i, j] + 75)
                        {
                            Point tmpp = new Point(X[i, j], Y[i, j]);
                            foreach (Point i2 in list_p4)
                            {
                                if (tmpp.X == i2.X && tmpp.Y == i2.Y)
                                {
                                    start = true;
                                    return;
                                }
                            }
                            list_p4.Add(tmpp);
                            NT[tmpp.Y / 75, tmpp.X / 75] = "";
                        }
                    }
                display();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        if (X[i, j] <= e.X && e.X < X[i, j] + 75 && Y[i, j] <= e.Y && e.Y < Y[i, j] + 75)
                        {
                            Point tmpp = new Point(X[i, j], Y[i, j]);
                            foreach (Point i3 in list_p4)
                            {
                                if (tmpp.X == i3.X && tmpp.Y == i3.Y)
                                {
                                    int a = list_p4.IndexOf(tmpp);
                                    NT[list_p4[a].Y / 75, list_p4[a].X / 75] = (list_p4[a].Y / 75 * 4 + list_p4[a].X / 75 + 1).ToString();
                                    list_p4.Remove(tmpp);
                                    list_p3.RemoveAt(a);
                                    timer1.Start();
                                    display();
                                    Refresh();
                                    return;
                                }
                            }
                        }
                    }
            }
            start = false;
            Refresh();
        }
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!can_start) return;
            if(!start)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        if (X[i, j] <= e.X && e.X < X[i, j] + 75 && Y[i, j] <= e.Y && e.Y < Y[i, j] + 75)
                        {
                            Point tmp = new Point(X[i, j], Y[i, j]);
                            foreach (Point i2 in list_p3)
                            {
                                if(tmp.X == i2.X&&tmp.Y==i2.Y)
                                {
                                    start = false;
                                    return;
                                }
                            }
                            list_p3.Add(tmp);
                        }
                    }
            }
            timer1.Start();
            start = true;
            Refresh();
        }

    }

}

    

