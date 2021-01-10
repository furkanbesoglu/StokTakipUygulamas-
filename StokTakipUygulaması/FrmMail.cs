using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace StokTakipUygulaması
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMailAdres.Text = mail;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("furkababali@gmail.com","Kralkel.3478");
            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(txtMailAdres.Text);
            mesajim.From = new MailAddress("furkababali@gmail.com");
            mesajim.Subject = txtKonu.Text;
            mesajim.Body = rchMesaj.Text;
            istemci.Send(mesajim);
            MessageBox.Show("Bilgilendirme", "Mail Gönderme İşlemi Başarılı Şekilde Gerçekleştirildi.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
