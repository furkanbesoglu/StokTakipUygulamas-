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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 10);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 23);
            //chartControl1.Series["Series 1"].Points.AddPoint("Gaziantep", 8);

            SqlDataAdapter da = new SqlDataAdapter("select ProductName,SUM(ProductPiece) as 'Miktar' from ProductTbl Group By ProductName", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;

            //Charta Stok Miktarı Listeleme
            SqlCommand komut = new SqlCommand("select ProductName,SUM(ProductPiece) as 'Miktar' from ProductTbl Group By ProductName", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();

            //Charta Firma Şehir Sayısı Çekme
            SqlCommand komut2 = new SqlCommand("SELECT City AS 'Şehir',COUNT(*) as 'Miktar' FROM CompanyTbl GROUP BY City", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay frmfut = new FrmStokDetay();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frmfut.ad = dr["ProductName"].ToString();
            }
            frmfut.Show();
        }

    }
}
