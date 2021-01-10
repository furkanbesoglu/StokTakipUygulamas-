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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from CompanyTbl", bgl.baglanti());
            DataTable dt = new DataTable();
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

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("select FirmaOzelKod1 from FirmaOzelKodlar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                rchTxtOzelkod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            rchTxtOzelkod1.Text = "";
            rchTxtOzelkod2.Text = "";
            rchTxtOzelkod3.Text = "";
            txtYetkiliStatü.Text = "";
            txtAdSoyadYetkil.Text = "";
            mskTxtTCKNYetkili.Text = "";
            txtSektor.Text = "";
            mskTxtTelefon.Text = "";
            mskTxtTelefon2.Text = "";
            mskTXTPhone3.Text = "";
            txtMail.Text = "";
            mskTxtFaks.Text = "";
            cmbil.Text = "";
            cmbIlce.Text = "";
            txtVergiDairesi.Text = "";
            rchTxtAdres.Text = "";
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

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            carikodaciklamalar();
            temizle();
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["CompanyID"].ToString();
                txtAd.Text = dr["CompanyName"].ToString();
                txtYetkiliStatü.Text = dr["CompanyAuthorizedStatu"].ToString();
                txtAdSoyadYetkil.Text = dr["CompanyAuthorizedNameSurname"].ToString();
                mskTxtTCKNYetkili.Text = dr["CompanyAuthorizedTCKN"].ToString();
                txtSektor.Text = dr["Sektor"].ToString();
                mskTxtTelefon.Text = dr["CompanyPhone"].ToString();
                mskTxtTelefon2.Text = dr["CompanyPhoneOne"].ToString();
                mskTXTPhone3.Text = dr["CompanyPhoneTwo"].ToString();
                txtMail.Text = dr["CompanyMail"].ToString();
                mskTxtFaks.Text = dr["CompanyFax"].ToString();
                cmbil.Text = dr["City"].ToString();
                cmbIlce.Text = dr["District"].ToString();
                txtVergiDairesi.Text = dr["CompanyTaxAdministration"].ToString();
                rchTxtAdres.Text = dr["CompanyAdress"].ToString();
                rchTxtOzelkod1.Text = dr["OzelKod1"].ToString();
                rchTxtOzelkod2.Text = dr["OzelKod2"].ToString();
                rchTxtOzelkod3.Text = dr["OzelKod3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into CompanyTbl (CompanyName,CompanyAuthorizedStatu,CompanyAuthorizedNameSurname,CompanyAuthorizedTCKN,Sektor,CompanyPhone,CompanyPhoneOne,CompanyPhoneTwo,CompanyMail,CompanyFax,City,District,CompanyTaxAdministration,CompanyAdress,OzelKod1,OzelKod2,OzelKod3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYetkiliStatü.Text);
            komut.Parameters.AddWithValue("@p3", txtAdSoyadYetkil.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTCKNYetkili.Text);
            komut.Parameters.AddWithValue("@p5", txtSektor.Text);
            komut.Parameters.AddWithValue("@p6", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p7", mskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", mskTXTPhone3.Text);
            komut.Parameters.AddWithValue("@p9", txtMail.Text);
            komut.Parameters.AddWithValue("@p10", mskTxtFaks.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p14", rchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p15", rchTxtOzelkod1.Text);
            komut.Parameters.AddWithValue("@p16", rchTxtOzelkod2.Text);
            komut.Parameters.AddWithValue("@p17", rchTxtOzelkod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    SqlCommand komutSil = new SqlCommand("Delete from CompanyTbl Where CompanyID=@p1", bgl.baglanti());
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
            SqlCommand komut = new SqlCommand("update CompanyTbl set CompanyName=@p1,CompanyAuthorizedStatu=@p2,CompanyAuthorizedNameSurname=@p3,CompanyAuthorizedTCKN=@p4,Sektor=@p5,CompanyPhone=@p6,CompanyPhoneOne=@p7,CompanyPhoneTwo=@p8,CompanyMail=@p9,CompanyFax=@p10,City=@p11,District=@p12,CompanyTaxAdministration=@p13,CompanyAdress=@p14,OzelKod1=@p15,OzelKod2=@p16,OzelKod3=@p17 where CompanyID=@p18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYetkiliStatü.Text);
            komut.Parameters.AddWithValue("@p3", txtAdSoyadYetkil.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtTCKNYetkili.Text);
            komut.Parameters.AddWithValue("@p5", txtSektor.Text);
            komut.Parameters.AddWithValue("@p6", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p7", mskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", mskTXTPhone3.Text);
            komut.Parameters.AddWithValue("@p9", txtMail.Text);
            komut.Parameters.AddWithValue("@p10", mskTxtFaks.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p14", rchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p15", rchTxtOzelkod1.Text);
            komut.Parameters.AddWithValue("@p16", rchTxtOzelkod2.Text);
            komut.Parameters.AddWithValue("@p17", rchTxtOzelkod3.Text);
            komut.Parameters.AddWithValue("@p18", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
