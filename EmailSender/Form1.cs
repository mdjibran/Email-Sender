using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;


namespace EmailSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] fileList = new string[20];
        float attachSize=0, totAttach=0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (_tbto.Text != "" || _tbtoList.Items.Count > 0)
                sendMail();
            else
                MessageBox.Show("Please enter a receivers email address or list of receivers to send mail","Email Sender");
        }


        private void sendMail()
        {
            if (radioButton1.Checked == true)
                sendToSingle();
            if (radioButton2.Checked == true)
                SendToMany();
        }

        private void SendToMany()
        {
            progressBar1.Value = 0;
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(_tbfrom.Text, "EasyNotes.Co.In");
                //              msg.To.Add(new MailAddress(_tbto.Text));
                try
                {
                    msg.Attachments.Add(new Attachment(_attchmentBox.Text));
                }
                catch { }
                msg.Subject = _tbsubject.Text;
                msg.Body = _tbMessage.Text;
                msg.IsBodyHtml = false;



                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential(_tbfrom.Text, "9960065517");
                smtp.EnableSsl = true;

                int listLength, seriel = 1;
                decimal barValue = 0;
                listLength = _tbtoList.Items.Count;
                barValue = (100 / listLength);


                progressBar1.Value = 0;
                smtp.Send(msg);

                MessageBox.Show("All emails have been sent", "Easynotes Emailer");
            }

            catch
            {
                MessageBox.Show("Some Unknown Error occured, Please try after some time!!!", "Email Sender");
                return;
            }          
        }

        private void sendToSingle()
        {
            progressBar1.Value = 0;
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("studentsitehelp@gmail.com", "EasyNotes.Co.In");
                msg.To.Add(new MailAddress(_tbto.Text));
                try
                {
                    msg.Attachments.Add(new Attachment(_attchmentBox.Text));
                }
                catch { }
                msg.Subject = _tbsubject.Text;
                msg.Body = _tbMessage.Text;
                msg.IsBodyHtml = false;



                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential("studentsitehelp@gmail.com", "9960065517");
                smtp.EnableSsl = true;

                smtp.Send(msg);

                MessageBox.Show("Email has been sent", "Easynotes Emailer");
            }

            catch
            {
                MessageBox.Show("Some Unknown Error occured, Please try after some time!!!", "Email Sender");
                return;
            }          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _tbtoList.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {               

                StreamReader sr = new StreamReader(ofd.FileName);
                string text = File.ReadAllText(ofd.FileName).ToString();
                string pureText = text.Replace("\r\n", "");

                string[] spltredText = pureText.Split(',');
                foreach (string s in spltredText)
                {                                         
                  _tbtoList.Items.Add(s);
                  button2.Enabled = true;
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            button1.Enabled = false;
            _tbto.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            float div;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName.ToString();
                FileInfo finfo = new FileInfo(path);
                attachSize = Convert.ToInt32(finfo.Length);

                div = attachSize / 1048576;
                totAttach += div;
                size.Text = div.ToString() + " MB";
                _attchmentBox.Text = path;
                //listBox1.Items.Add(path);
                //int i = 0;
                //foreach (string file in listBox1.Items)
                //{
                //    fileList[i] = file;
                //    i++;
                //}
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _tbto.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            _tbto.Text = "";
            _tbto.Enabled = false;
        }        
    }
}
