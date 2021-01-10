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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from invoiceTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            mskTxtTarih.Text = "";
            txtAlici.Text = "";
            txtFaturaID.Text = "";
            txtFiyat.Text = "";
            txtID.Text = "";
            txtMiktar.Text = "";
            txtSeri.Text = "";
            txtSiraNo.Text = "";
            txtTeslimEden.Text = "";
            txtTeslimAlan.Text = "";
            txtTutar.Text = "";
            txtUrunAd.Text = "";
            txtUrunID.Text = "";
            mktxtSaat.Text = "";
            txtVegiDairesi.Text = "";

        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFaturaID.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into invoiceTbl (invoiceSherry,invoiceSherryNo,invoiceDate,invoiceClock,invoiceTaxAdministration,invoiceReceiver,invoiceDelivering,invoiceDeliveringReceiver) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtSeri.Text);
                komut.Parameters.AddWithValue("@p2", txtSiraNo.Text);
                komut.Parameters.AddWithValue("@p3", mskTxtTarih.Text);
                komut.Parameters.AddWithValue("@p4", mktxtSaat.Text);
                komut.Parameters.AddWithValue("@p5", txtVegiDairesi.Text);
                komut.Parameters.AddWithValue("@p6", txtAlici.Text);
                komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            //Firma Carisi
            if (txtFaturaID.Text != "" && comboBox1.Text == "Firma")
            {
                double tutar, miktar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();


                SqlCommand komut2 = new SqlCommand("insert into invoiceDetailTbl (ProductName,Piece,Price,Amount,invoiceID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtFaturaID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Hareket Tablosuna Veri Ekleme
                SqlCommand komut3 = new SqlCommand("insert into FirmaHareketlerTbl (UrunID,Adet,PersonelID,FirmaID,Fiyat,Toplam,FaturaID,Tarih) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@p1", txtUrunID.Text);
                komut3.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@p3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@p4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@p5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@p6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@p7", txtFaturaID.Text);
                komut3.Parameters.AddWithValue("@p8", mskTxtTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Stok Sayısı Azaltma
                SqlCommand komut4 = new SqlCommand("update ProductTbl set ProductPiece=ProductPiece-@s1 where ProductID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", txtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", txtUrunID.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Fatura Ait Ürün Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Müşteri Carisi
            if (txtFaturaID.Text != "" && comboBox1.Text == "Müşteri")
            {
                double tutar, miktar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();


                SqlCommand komut2 = new SqlCommand("insert into invoiceDetailTbl (ProductName,Piece,Price,Amount,invoiceID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtFaturaID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Hareket Tablosuna Veri Ekleme
                SqlCommand komut3 = new SqlCommand("insert into MusteriHareketlerTbl (UrunID,Adet,PersonelID,MusteriID,Fiyat,Toplam,FaturaID,Tarih) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@p1", txtUrunID.Text);
                komut3.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@p3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@p4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@p5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@p6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@p7", txtFaturaID.Text);
                komut3.Parameters.AddWithValue("@p8", mskTxtTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Stok Sayısı Azaltma
                SqlCommand komut4 = new SqlCommand("update ProductTbl set ProductPiece=ProductPiece-@s1 where ProductID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", txtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", txtUrunID.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Fatura Ait Ürün Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["invoiceID"].ToString();
                txtSeri.Text = dr["invoiceSherry"].ToString();
                txtSiraNo.Text = dr["invoiceSherryNo"].ToString();
                mskTxtTarih.Text = dr["invoiceDate"].ToString();
                mktxtSaat.Text = dr["invoiceClock"].ToString();
                txtVegiDairesi.Text = dr["invoiceTaxAdministration"].ToString();
                txtAlici.Text = dr["invoiceReceiver"].ToString();
                txtTeslimEden.Text = dr["invoiceDelivering"].ToString();
                txtTeslimAlan.Text = dr["invoiceDeliveringReceiver"].ToString();
            }
        }

        private void BtnTemizle_Click_1(object sender, EventArgs e)
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
                dialog = MessageBox.Show("Fatura Silinsin Mi?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SqlCommand komutSil = new SqlCommand("Delete from invoiceTbl Where invoiceID=@p1", bgl.baglanti());
                    komutSil.Parameters.AddWithValue("@p1", txtID.Text);
                    komutSil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Fatura Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update invoiceTbl set invoiceSherry=@p1,invoiceSherryNo=@p2,invoiceDate=@p3,invoiceClock=@p4,invoiceTaxAdministration=@p5,invoiceReceiver=@p6,invoiceDelivering=@p7,invoiceDeliveringReceiver=@p8 where invoiceID=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtSeri.Text);
            komut.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTarih.Text);
            komut.Parameters.AddWithValue("@p4", mktxtSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtVegiDairesi.Text);
            komut.Parameters.AddWithValue("@p6", txtAlici.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@p9", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Başarılı Şeklide Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay frmfut = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frmfut.id = dr["invoiceID"].ToString();
            }
            frmfut.Show();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select ProductName,ProductSellingPrice from ProductTbl where ProductID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtUrunAd.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
