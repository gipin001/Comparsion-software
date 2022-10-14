using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace hashvalue2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string e2;
        public string e3;
        //string folderPath = "";

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string fPath = e.Argument.ToString();
            byte[] buffer;
            int bytesRead;
            long size;
            long totalBytesRead = 0;
            using (Stream file = File.OpenRead(fPath))
            {
                size = file.Length;
                using (HashAlgorithm hasher = MD5.Create())
                {
                    do
                    {
                        buffer = new byte[4096];
                        bytesRead = file.Read(buffer, 0, buffer.Length);
                        totalBytesRead += bytesRead;
                        hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
                        backgroundWorker1.ReportProgress((int)((double)totalBytesRead / size * 100));
                    }
                    while (bytesRead != 0);

                    hasher.TransformFinalBlock(buffer, 0, 0);
                    e.Result = MakeHashString(hasher.Hash);
                }
            }
        }
        private static string MakeHashString(byte[] hashBytes)
        {
            StringBuilder hash = new StringBuilder(32);
            foreach (byte b in hashBytes)
            {
                hash.Append(b.ToString("X2").ToLower());
            }
            return hash.ToString();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            e2 = e.Result.ToString();
            //Console.WriteLine(typeof(e2));
            //MessageBox.Show(e2);
            backgroundWorker2.RunWorkerAsync(textBox2.Text);

            //progressBar1.Value = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowser.FileName;
                textBox1.Text = folderPath;
                label3.Text = "OK";
                label3.BackColor = Color.Green;
                // ...
            }
            //progressBar1.Value = 1;

            //backgroundWorker1.RunWorkerAsync(textBox1.Text);
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            string fPath = e.Argument.ToString();
            byte[] buffer;
            int bytesRead;
            long size;
            long totalBytesRead = 0;

            using (Stream file = File.OpenRead(fPath))
            {
                size = file.Length;
                using (HashAlgorithm hasher = MD5.Create())
                {
                    do
                    {
                        buffer = new byte[4096];
                        bytesRead = file.Read(buffer, 0, buffer.Length);
                        totalBytesRead += bytesRead;
                        hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
                        backgroundWorker2.ReportProgress((int)((double)totalBytesRead / size * 100));
                    }
                    while (bytesRead != 0);

                    hasher.TransformFinalBlock(buffer, 0, 0);
                    e.Result = MakeHashString(hasher.Hash);
                }
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
                
                 label1.Text = (e.ProgressPercentage).ToString() + "%";
           

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            e3 = e.Result.ToString();
            //MessageBox.Show(e3);
            Console.WriteLine(e3);
            
            //progressBar2.Value = 100;
            if (e2 == e3)
            {
              //MessageBox.Show("equal");
                label1.Text = "EXACT SAME";
                label1.BackColor = Color.Green;
                //progressBar2.Value = 100;
            }
            else
            {
                //MessageBox.Show("not equal");
                label1.Text = "NOT COMPARABLE";
                
                label1.BackColor = Color.Red;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            backgroundWorker1.RunWorkerAsync(textBox1.Text);
            
            //Console.WriteLine(e2);
            //Console.WriteLine(e2);
            //
            //Console.WriteLine(e3);

            //if (e2==e3)
            //{
            //    MessageBox.Show("equal");
            //}
            //else
            //{
            //    MessageBox.Show("not equal");
            //}

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowser.FileName;
                textBox2.Text = folderPath;
                label4.Text = "OK";
                label4.BackColor = Color.Green;
                // ...
            }
            
            //backgroundWorker2.RunWorkerAsync(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

    }
}
