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
using System.Xml;

namespace StokTakipUygulaması
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void AzalanStoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ProductName AS 'Ürün Adı',SUM(ProductPiece) AS 'Adet' from ProductTbl group by ProductName having SUM(ProductPiece) <= 20 order by SUM(ProductPiece)", bgl.baglanti());
            da.Fill(dt);
            GridControlAzalanStoklar.DataSource = dt;

        }

        void AjandaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select top 10 NoteDate AS 'Tarih',NoteClock AS 'Saat',NoteTitle AS 'Başlık' from NoteTbl order by NoteID desc", bgl.baglanti());
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;
        }

        void firmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHarketler2", bgl.baglanti());
            da.Fill(dt);
            GridControlSonOnHareket.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select CompanyName,CompanyPhone,CompanyPhoneOne from CompanyTbl", bgl.baglanti());
            da.Fill(dt);
            GridControlEnCokIsYapilanFirma.DataSource = dt;
        }

        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name== "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
                
            }
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            AzalanStoklar();
            AjandaListele();
            firmaHareket();
            fihrist();
            haberler();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}
