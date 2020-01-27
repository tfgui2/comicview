using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace comicview
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pictureBox1.Load("il2.jpg");
            //pictureBox2.Load("il2.jpg");

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string currentFile = openFileDialog1.FileName;
                makeFileList(currentFile);
                showTwo(m_currentIndex);
            }
        }

        private string[] m_files;
        private int m_currentIndex = 0;
        private void makeFileList(string currentfile)
        {
            string folder = Path.GetDirectoryName(currentfile);
            string[] files = Directory.GetFiles(folder);
            //foreach (string name in files)
            //{
            //    string ext = Path.GetExtension(name).ToLower();
            //    if ()
            //}
            m_files = files;
            for (int i = 0; i < m_files.Length; i++)
            {
                if (currentfile == m_files[i])
                {
                    m_currentIndex = i;
                    break;
                }
            }

            this.Text = folder;
        }

        private int showTwo(int index)
        {
            if (index < 0)
                return index;

            if (index >= m_files.Length)
                return index;

            pictureBox2.Load(m_files[index]);
            index++;

            if (index >= m_files.Length)
                return index;

            pictureBox1.Load(m_files[index]);
            index++;

            return index;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            Image im1 = pictureBox1.Image;
            if (im1 != null)
            {
                pictureBox1.Height = this.ClientSize.Height;
                pictureBox1.Width = (int)(pictureBox1.Height * ((float)(im1.Width) / (float)(im1.Height)));
                int x = this.ClientSize.Width / 2 - pictureBox1.Width;
                pictureBox1.Location = new Point(x, 0);
            }

            Image im2 = pictureBox2.Image;
            if (im2 != null)
            {
                pictureBox2.Height = this.ClientSize.Height;
                pictureBox2.Width = (int)(pictureBox2.Height * ((float)(im2.Width) / (float)(im2.Height)));
                int x = this.ClientSize.Width / 2;
                pictureBox2.Location = new Point(x, 0);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (e.Modifiers == Keys.Control) 
                {
                    goNext(1);
                }
                else
                {
                    goNext(2);
                }
            }

            if (e.KeyCode == Keys.Left)
            {
                if (e.Modifiers == Keys.Control)
                {
                    goPrev(1);
                }
                else
                {
                    goPrev(2);
                }
            }

            Form1_ClientSizeChanged(this, new EventArgs());
        }

        private void goNext(int step)
        {
            this.m_currentIndex += step;
            showTwo(m_currentIndex);
        }

        private void goPrev(int step)
        {
            this.m_currentIndex -= step;
            showTwo(m_currentIndex);
        }
    }
}
