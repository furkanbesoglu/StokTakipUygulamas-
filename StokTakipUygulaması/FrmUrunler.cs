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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from ProductTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        void temizle()
        {
            txtAd.Text = "";
            txtID.Text = "";
            txtAlisFiyat.Text = "";
            txtSatisFiyat.Text = "";
            txtModel.Text = "";
            rchTxtDetay.Text = "";
            mskTxtYil.Text = "";
            txtMarka.Text = "";
            nudAdet.Value = 0;
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Verileri Kaydetme
            SqlCommand komut = new SqlCommand("insert into ProductTbl (ProductName,ProductBrand,ProductModel,ProductYear,ProductPiece,ProductPurchsePrice,ProductSellingPrice,ProductContent) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchTxtDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    SqlCommand komutSil = new SqlCommand("Delete from ProductTbl Where ProductID=@p1", bgl.baglanti());
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
            txtID.Text = dr["ProductID"].ToString();
            txtAd.Text = dr["ProductName"].ToString();
            txtMarka.Text = dr["ProductBrand"].ToString();
            txtModel.Text = dr["ProductModel"].ToString();
            mskTxtYil.Text = dr["ProductYear"].ToString();
            nudAdet.Value = int.Parse(dr["ProductPiece"].ToString());
            txtAlisFiyat.Text = dr["ProductPurchsePrice"].ToString();
            txtSatisFiyat.Text = dr["ProductSellingPrice"].ToString();
            rchTxtDetay.Text = dr["ProductContent"].ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update ProductTbl set ProductName=@p1,ProductBrand=@p2,ProductModel=@p3,ProductYear=@p4,ProductPiece=@p5,ProductPurchsePrice=@p6,ProductSellingPrice=@p7,ProductContent=@p8 where ProductID=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskTxtYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchTxtDetay.Text);
            komut.Parameters.AddWithValue("@p9", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Başarılı Şekilde Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
