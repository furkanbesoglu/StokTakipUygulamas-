using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StokTakipUygulaması
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunID;

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunID.Text = urunID;

            SqlCommand komut = new SqlCommand("select * from invoiceDetailTbl where invoiceDetailID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", urunID);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtTutar.Text = dr[4].ToString();
                txtUrunAd.Text = dr[1].ToString();
                bgl.baglanti().Close();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            double miktar, fiyat, tutar;
            miktar = Convert.ToDouble(txtMiktar.Text);
            fiyat = Convert.ToDouble(txtFiyat.Text);
            tutar = miktar * fiyat;
            txtTutar.Text = tutar.ToString();

            SqlCommand komut = new SqlCommand("update invoiceDetailTbl set ProductName=@p1,Piece=@p2,Price=@p3,Amount=@p4 where invoiceID=@p5 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMiktar.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@p5", urunID);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();

            if (txtUrunID.Text == "")
            {
                MessageBox.Show("Bu Alanı Boş Bırakamazsınız", "Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                dialog = MessageBox.Show("Ürün Silinsin Mi?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SqlCommand komutSil = new SqlCommand("Delete from invoiceTbl Where invoiceID=@p1", bgl.baglanti());
                    komutSil.Parameters.AddWithValue("@p1", txtUrunID.Text);
                    komutSil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Ürün Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
