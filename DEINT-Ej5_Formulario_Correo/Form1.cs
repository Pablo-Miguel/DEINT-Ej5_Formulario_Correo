using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEINT_Ej5_Formulario_Correo
{
    public partial class Form1 : Form
    {

        Attachment data;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try {
                MailMessage _Correo = new MailMessage();
                _Correo.From = new MailAddress(txtFrom.Text);

                _Correo.To.Add(txtTo.Text);
                _Correo.Subject = txtAsunto.Text;
                _Correo.Body = txtContenido.Text;
                if (data != null)
                {
                    _Correo.Attachments.Add(data);
                }
                _Correo.IsBodyHtml = false;
                _Correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(txtFrom.Text, txtPassword.Text);

                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                smtp.Send(_Correo);

                MessageBox.Show("Correo enviado");

                txtFrom.Clear();
                txtPassword.Clear();
                txtTo.Clear();
                data = null;
                txtAsunto.Clear();
                txtContenido.Clear();
            }
            catch (Exception ex) {
                MessageBox.Show("Algo ha fallado al enviar el correo");
            }
            

        }

        private void btnAdjunto_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            MessageBox.Show("Se ha adjuntado el archivo: " + filePath);

            data = new Attachment(filePath, MediaTypeNames.Application.Octet);

        }
    }
}
