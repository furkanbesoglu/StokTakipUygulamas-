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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from ExpensesTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtID.Text = "";
            rchTxtNotlar.Text = "";
            txtSu.Text = "0";
            txtMaas.Text="0";
            txtInternet.Text = "0";
            txtElektrik.Text = "0";
            txtEkstra.Text = "0";
            txtDogalgaz.Text = "0";
            cmbAy.Text = "";
            cmbYil.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ExpensesID"].ToString();
                cmbAy.Text = dr["Ay"].ToString();
                cmbYil.Text = dr["Yil"].ToString();
                txtElektrik.Text = dr["ExpensesElectricity"].ToString();
                txtSu.Text = dr["ExpensesWater"].ToString();
                txtDogalgaz.Text = dr["ExpensesNaturalGas"].ToString();
                txtInternet.Text = dr["ExpensesInternet"].ToString();
                txtMaas.Text = dr["ExpensesSalary"].ToString();
                txtEkstra.Text = dr["ExpensesExtra"].ToString();
                rchTxtNotlar.Text = dr["ExpensesDetail"].ToString();
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
                    SqlCommand komutSil = new SqlCommand("Delete from ExpensesTbl Where ExpensesID=@p1", bgl.baglanti());
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
            SqlCommand komut = new SqlCommand("insert into ExpensesTbl (Ay,Yil,ExpensesElectricity,ExpensesWater,ExpensesNaturalGas,ExpensesInternet,ExpensesSalary,ExpensesExtra,ExpensesDetail) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYil.Text);
            komut.Parameters.AddWithValue("@p3",decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4",decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5",decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6",decimal.Parse(txtInternet.Text));
            komut.Parameters.AddWithValue("@p7",decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8",decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchTxtNotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update ExpensesTbl set Ay=@p1,Yil=@p2,ExpensesElectricity=@p3,ExpensesWater=@p4,ExpensesNaturalGas=@p5,ExpensesInternet=@p6,ExpensesSalary=@p7,ExpensesExtra=@p8,ExpensesDetail=@p9 where ExpensesID=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchTxtNotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Başarı ile Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
