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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //Müşteri Bilgileri
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select CustomerName,CustomerSurname,CustomerPhone,CustomerPhoneTwo,CustomerMail from CustomerTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            //Firma Bilgileri
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select CompanyName,CompanyAuthorizedNameSurname,CompanyPhone,CompanyPhoneOne,CompanyPhoneTwo,CompanyMail,CompanyFax from CompanyTbl", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmm = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr !=null)
            {
                frmm.mail = dr["CustomerMail"].ToString();
            }
            frmm.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmm = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frmm.mail = dr["CompanyMail"].ToString();
            }
            frmm.Show();
        }
    }
}
