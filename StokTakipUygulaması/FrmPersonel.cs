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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from StaffTbl", bgl.baglanti());
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
            txtAd.Text = "";
            txtID.Text = "";
            txtGorev.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            mskTxtTelefon.Text = "";
            mskTxtTCKN.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchTxtAdres.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            temizle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ilce from District where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into StaffTbl (StaffName,StaffSurname,StaffPhone,StaffTCKN,StaffMail,City,District,StaffAdress,StaffStatu) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTCKN.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", rchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

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
                    SqlCommand komutSil = new SqlCommand("Delete from StaffTbl Where StaffID=@p1", bgl.baglanti());
                    komutSil.Parameters.AddWithValue("@p1", txtID.Text);
                    komutSil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Ürün Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["StaffID"].ToString();
                txtAd.Text = dr["StaffName"].ToString();
                txtSoyad.Text = dr["StaffSurname"].ToString();
                mskTxtTelefon.Text = dr["StaffPhone"].ToString();
                mskTxtTCKN.Text = dr["StaffTCKN"].ToString();
                txtMail.Text = dr["StaffMail"].ToString();
                cmbil.Text = dr["City"].ToString();
                cmbilce.Text = dr["District"].ToString();
                rchTxtAdres.Text = dr["StaffAdress"].ToString();
                txtGorev.Text = dr["StaffStatu"].ToString();
            }

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update StaffTbl set StaffName=@p1,StaffSurname=@p2,StaffPhone=@p3,StaffTCKN=@p4,StaffMail=@p5,City=@p6,District=@p7,StaffAdress=@p8,StaffStatu=@p9 where StaffID=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTCKN.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", rchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtGorev.Text);
            komut.Parameters.AddWithValue("@p10", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }
    }
}
