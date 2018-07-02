using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void laodI_Click(object sender, EventArgs e)
        {
            string file_name="";
            listView1.Clear();
            imageList1.Images.Clear();
            openF.InitialDirectory = "C:";
            openF.Title = "Add Image";
            openF.Filter = "JPEG|*.jpg|PNG|*.png|GIF|*.gif|ALL Images|*.*";
            openF.FileName = file_name;
            openF.ShowDialog();
            if (DialogResult == DialogResult.Cancel) { return; }
            try
            {
                int num_of_files = openF.FileNames.Length;
                string[] arryfiles = new string[num_of_files ];
                int counter = 0;
                foreach (string singl_file in openF.FileNames)
                {
                    arryfiles[counter] = singl_file;
                    imageList1.Images.Add(Image.FromFile(singl_file));
                    
                    counter++;
                }
                    listView1.LargeImageList = imageList1;
                    for (int i = 0; i < counter; i++)
                    {
                        listView1.Items.Add(arryfiles[i], i);
                    }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string big_filename;
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                big_filename = listView1.SelectedItems[i].Text;
                pictureBox1.Image = Image.FromFile(big_filename);
                panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap btm = new Bitmap(pictureBox1.Image);
                btm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = btm;
            }
            else
            {
                MessageBox.Show("No Image");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap btm = new Bitmap(pictureBox1.Image);
                btm.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = btm;
            }
            else
            {
                MessageBox.Show("No Image");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bitm = new Bitmap(pictureBox1.Image);
                Rectangle rec = new Rectangle(0, 0, 100, 100);
                Bitmap cloneIm = bitm.Clone(rec, System.Drawing.Imaging.PixelFormat.DontCare);
                pictureBox1.Image = cloneIm;
            }
            else
            {
                MessageBox.Show("No Such Image!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bim = new Bitmap(pictureBox1.Image);
                int x, y;
                for (x = 0; x < bim.Width; x++)
                {
                    for (y = 0; y < bim.Height; y++)
                    {
                        Color old_color = bim.GetPixel(x, y);
                        Color new_color = Color.FromArgb(old_color.R, 0, 0);
                        bim.SetPixel(x, y, new_color);

                    }
                }
                pictureBox1.Image = bim;
            }
            else
            {
                MessageBox.Show("No Such Image");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bim = new Bitmap(pictureBox1.Image);
                int x, y;
                for (x = 0; x < bim.Width; x++)
                {
                    for (y = 0; y < bim.Height; y++)
                    {
                        Color old_color = bim.GetPixel(x, y);
                        Color new_color = Color.FromArgb(255-old_color.R,255-old_color.G  ,255-old_color.B  );
                        bim.SetPixel(x, y, new_color);

                    }
                }
                pictureBox1.Image = bim;
            }
            else
            {
                MessageBox.Show("No Such Image");
            }
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            
            string  savepath = "";           
            saveF.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
              
            saveF.Title = "Save As..";
            saveF.FileName = "Untiteled";
            saveF.Filter = "JPEG|*.jpg";
            if (saveF.ShowDialog() != DialogResult.Cancel)
            {
                savepath = saveF.FileName;
                Bitmap bitm = new Bitmap(pictureBox1.Image);
                bitm.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Image Saved!");
            }

        }
        private void button7_Click(object sender, EventArgs e)
        {
            string savepath = "";

            saveF.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            saveF.Title = "Save As..";
            saveF.FileName = "Untiteled";
            saveF.Filter = "Icon|*.ico";
            if (saveF.ShowDialog() != DialogResult.Cancel)
            {
                savepath = saveF.FileName;
                Icon ico = new System.Drawing.Icon(savepath );
                Bitmap bitm = new Bitmap(pictureBox1.Image);
                bitm.Save(savepath, System.Drawing.Imaging.ImageFormat.Icon);
                MessageBox.Show("Image Saved!");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            for(int i=0;i<listView1 .SelectedItems.Count ;i++)
            if(listView1 .SelectedItems[i]!=null)
            {
                    listView1.SelectedItems[i].Remove();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap bitm = new Bitmap(pictureBox1.Image);
            Bitmap bitmnew = new Bitmap(Convert.ToInt32(pictureBox1.Image.Width / 2), Convert.ToInt32(pictureBox1.Image.Height / 2));
            Graphics graf = Graphics.FromImage(bitmnew);
            graf.DrawImage(bitm, 0, 0, bitmnew.Width, bitmnew.Height);
            pictureBox1.Image = bitmnew;
            panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Bitmap bitm = new Bitmap(pictureBox1.Image);
            Bitmap bitmnew = new Bitmap(Convert.ToInt32(pictureBox1.Image.Width * 2), Convert.ToInt32(pictureBox1.Image.Height * 2));
            Graphics graf = Graphics.FromImage(bitmnew);
            graf.DrawImage(bitm, 0, 0, bitmnew.Width, bitmnew.Height);
            pictureBox1.Image = bitmnew;
            panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
            
        }
    }
}
