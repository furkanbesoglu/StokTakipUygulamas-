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
using DevExpress.Charts;

namespace StokTakipUygulaması
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute sp_FirmaHareketleri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void MusteriHareketleri()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("execute sp_MusteriHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }

        void Giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from ExpensesTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;

        }
        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            FirmaHareketleri();
            MusteriHareketleri();
            Giderler();

            lblAktifKullanici.Text = ad;

            //Toplam Tutat Sorgusu
            SqlCommand komut1 = new SqlCommand("select SUM(Amount) from invoiceDetailTbl", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamTutar.Text = dr1[0].ToString() + " ₺";
            }
            bgl.baglanti().Close();

            //Son Ayın Faturaları Sorgusu
            SqlCommand komut2 = new SqlCommand("select (ExpensesElectricity+ExpensesInternet+ExpensesNaturalGas+ExpensesWater+ExpensesExtra) from ExpensesTbl order by ExpensesID asc ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString() + " ₺";
            }
            bgl.baglanti().Close();

            //Son Ayın PErsonel Maaşları
            SqlCommand komut3 = new SqlCommand("select ExpensesSalary from ExpensesTbl order by ExpensesID asc ", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaasi.Text = dr3[0].ToString() + " ₺";
            }
            bgl.baglanti().Close();

            //Müşteri Sayısı
            SqlCommand komut4 = new SqlCommand("select COUNT(*) from CustomerTbl", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma Sayısı
            SqlCommand komut5 = new SqlCommand("select COUNT(*) from CompanyTbl", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Şehir Firma Sayısı
            SqlCommand komut6 = new SqlCommand("select COUNT(distinct(City)) from CompanyTbl", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Personel Sayısı
            SqlCommand komut7 = new SqlCommand("select COUNT(*) from StaffTbl", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblPersonleSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Stok Sayısı
            SqlCommand komut8 = new SqlCommand("select SUM(ProductPiece) from ProductTbl", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblStokSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac > 0 && sayac < 5)
            {
                //Elektrik
                groupControl10.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut9 = new SqlCommand("select top 4 Ay,ExpensesElectricity from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr9 = komut9.ExecuteReader();
                while (dr9.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr9[0], dr9[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 5 && sayac < 10)
            {

                //Su
                groupControl10.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesWater from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 10 && sayac < 15)
            {

                //İnternet
                groupControl10.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesInternet from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 15 && sayac < 20)
            {

                //doğalgaz
                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesNaturalGas from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 20 && sayac < 25)
            {

                //doğalgaz
                groupControl10.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesExtra from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 25 && sayac < 30)
            {

                //doğalgaz
                groupControl10.Text = "Maaşlar";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesSalary from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 31)
            {
                sayac = 0;
            }
        }
        int sayac2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 < 5)
            {
                //Elektrik
                groupControl11.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut9 = new SqlCommand("select top 4 Ay,ExpensesElectricity from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr9 = komut9.ExecuteReader();
                while (dr9.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr9[0], dr9[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 5 && sayac2 < 10)
            {

                //Su
                groupControl11.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesWater from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 10 && sayac2 < 15)
            {

                //İnternet
                groupControl11.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesInternet from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 15 && sayac2 < 20)
            {

                //doğalgaz
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesNaturalGas from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 20 && sayac2 < 25)
            {

                //doğalgaz
                groupControl11.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesExtra from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 25 && sayac2 < 30)
            {

                //doğalgaz
                groupControl11.Text = "Maaşlar";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 Ay,ExpensesSalary from ExpensesTbl order by ExpensesID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 31)
            {
                sayac2 = 0;
            }
        }
    }
}
