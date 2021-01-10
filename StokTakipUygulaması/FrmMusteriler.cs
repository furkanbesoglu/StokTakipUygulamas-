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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from CustomerTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("select Sehir from City", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTxtTelefon.Text = "";
            mskTxtTelefon2.Text = "";
            mskTxtTCKN.Text = "";
            txtMail.Text = "";
            txtVergiDairesi.Text = "";
            rchTxtAdres.Text = "";
            cmbil.Text = "";
            cmbIlce.Text = "";
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            temizle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ilce from District where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into CustomerTbl (CustomerName,CustomerSurname,CustomerPhone,CustomerPhoneTwo,CustomerTCKN,CustomerMail,City,District,CustomerAdress,CustomerTaxAdministration) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTxtTCKN.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cmbil.Text);
            komut.Parameters.AddWithValue("@p8", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p9", rchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtVergiDairesi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["CustomerID"].ToString();
                txtAd.Text = dr["CustomerName"].ToString();
                txtSoyad.Text = dr["CustomerSurname"].ToString();
                mskTxtTelefon.Text = dr["CustomerPhone"].ToString();
                mskTxtTelefon2.Text = dr["CustomerPhoneTwo"].ToString();
                mskTxtTCKN.Text = dr["CustomerTCKN"].ToString();
                txtMail.Text = dr["CustomerMail"].ToString();
                cmbil.Text = dr["City"].ToString();
                cmbIlce.Text = dr["District"].ToString();
                rchTxtAdres.Text = dr["CustomerAdress"].ToString();
                txtVergiDairesi.Text = dr["CustomerTaxAdministration"].ToString();
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();

            if (txtID.Text == "")
            {
                MessageBox.Show("Bu Alanı Boş Bırakamazsınız", "Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                dialog = MessageBox.Show("Ürün Silinsin Mi?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SqlCommand komutSil = new SqlCommand("Delete from CustomerTbl Where CustomerID=@p1", bgl.baglanti());
                    komutSil.Parameters.AddWithValue("@p1", txtID.Text);
                    komutSil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Ürün Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update CustomerTbl set CustomerName=@p1,CustomerSurname=@p2,CustomerPhone=@p3,CustomerPhoneTwo=@p4,CustomerTCKN=@p5,CustomerMail=@p6,City=@p7,District=@p8,CustomerAdress=@p9,CustomerTaxAdministration=@p10 where CustomerID=@p11", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTxtTCKN.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cmbil.Text);
            komut.Parameters.AddWithValue("@p8", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p9", rchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p11", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Başarıyla Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
