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
                updateLayout();
            }
        }

        // private string[] m_files;
        private List<string> m_files = new List<string>();
        private int m_currentIndex = 0;
        private void makeFileList(string currentfile)
        {
            m_files.Clear();

            string folder = Path.GetDirectoryName(currentfile);
            string[] files = Directory.GetFiles(folder);
            foreach (string name in files)
            {
                string ext = Path.GetExtension(name).ToLower();
                if (ext == ".jpg" || ext == ".png")
                {
                    this.m_files.Add(name);
                }
            }

            //m_files = files;

            for (int i = 0; i < m_files.Count(); i++)
            {
                if (currentfile == m_files[i])
                {
                    m_currentIndex = i;
                    break;
                }
            }

            this.Text = folder;
        }

        private int loadOne(int index)
        {
            if (index < 0)
                return index;

            if (index >= m_files.Count())
                return index;

            pictureBox2.Load(m_files[index]);
            this.Text = m_files[index];
            index++;

            return index;
        }

        private int loadTwo(int index)
        {
            if (index < 0)
                return index;

            if (index >= m_files.Count())
                return index;

            pictureBox2.Load(m_files[index]);
            this.Text = m_files[index];
            index++;

            if (index >= m_files.Count())
                return index;

            pictureBox1.Load(m_files[index]);
            index++;

            return index;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private int m_pageviewcount = 2;

        private void updateLayout()
        {
            if (m_pageviewcount == 1)
            {
                loadOne(m_currentIndex);
                pageOneLayout();
            }
            else
            {
                loadTwo(m_currentIndex);
                pageTwoLayout();
            }
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            updateLayout();
        }

        private void pageOneLayout()
        {
            pictureBox1.Hide();

            Image im2 = pictureBox2.Image;
            if (im2 != null)
            {
                pictureBox2.Height = this.ClientSize.Height;
                pictureBox2.Width = this.ClientSize.Width;
                pictureBox2.Location = new Point(0, 0);
            }
        }

        private void pageTwoLayout()
        {
            pictureBox1.Show();

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
                    goNext(m_pageviewcount);
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
                    goPrev(m_pageviewcount);
                }
            }

            if (e.KeyCode == Keys.D1)
            {
                m_pageviewcount = 1;
                updateLayout();
            }
            if (e.KeyCode == Keys.D2)
            {
                m_pageviewcount = 2;
                updateLayout();
            }
        }

        private void goNext(int step)
        {
            this.m_currentIndex += step;
            updateLayout();
        }

        private void goPrev(int step)
        {
            this.m_currentIndex -= step;
            updateLayout();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" page move : left/right arrow \n 1page move : ctrl + left/right \n ");
        }

        private void pageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_pageviewcount = 1;
            updateLayout();
        }

        private void pageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_pageviewcount = 2;
            updateLayout();
        }
    }
}
