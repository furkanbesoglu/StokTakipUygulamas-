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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            lookUpEdit1.Text = "";
            txtHesapNo.Text = "";
            txtHesapTuru.Text = "";
            txtIban.Text = "TR";
            txtSube.Text = "";
            txtyetkili.Text = "";
            mskTxtTelefon.Text = "";
            mskTxtTarih.Text = "";
            cmbil.Text = "";
            cmbIlce.Text = "";
        }

        void firmalistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select CompanyID,CompanyName from CompanyTbl", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "CompanyID";
            lookUpEdit1.Properties.DisplayMember = "CompanyName";
            lookUpEdit1.Properties.DataSource = dt;
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

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            firmalistele();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["BankID"].ToString();
                txtAd.Text = dr["BankName"].ToString();
                lookUpEdit1.Text = dr["CompanyName"].ToString();
                mskTxtTelefon.Text = dr["Phone"].ToString();
                mskTxtTarih.Text = dr["Date"].ToString();
                txtHesapNo.Text = dr["BankHesapNo"].ToString();
                cmbil.Text = dr["City"].ToString();
                cmbIlce.Text = dr["District"].ToString();
                txtHesapTuru.Text = dr["AccountType"].ToString();
                txtIban.Text = dr["BankIBAN"].ToString();
                txtSube.Text = dr["BankBranch"].ToString();
                txtyetkili.Text = dr["BankAuthorized"].ToString();
            }

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
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
                    SqlCommand komutSil = new SqlCommand("Delete from BankTbl Where BankID=@p1", bgl.baglanti());
                    komutSil.Parameters.AddWithValue("@p1", txtID.Text);
                    komutSil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Ürün Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into BankTbl (BankName,City,District,BankBranch,BankIBAN,BankHesapNo,BankAuthorized,Phone,Date,AccountType,CompanID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtIban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p9", DateTime.Parse(mskTxtTarih.Text));
            komut.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Sisteme Banka Bilgisi Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update BankTbl set BankName=@p1,City=@p2,District=@p3,BankBranch=@p4,BankIBAN=@p5,BankHesapNo=@p6,BankAuthorized=@p7,Phone=@p8,Date=@p9,AccountType=@p10,CompanID=@p11 where BankID=@p12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtIban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p9", DateTime.Parse(mskTxtTarih.Text));
            komut.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p12", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Başarı İle Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }
    }
}
