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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from NoteTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtID.Text = "";
            txtOlusturan.Text = "";
            rchTxtDetay.Text = "";
            txtHitap.Text = "";
            txtBaslık.Text = "";
            mskTarih.Text = "";
            msktxtSaat.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["NoteID"].ToString();
                mskTarih.Text = dr["NoteDate"].ToString();
                msktxtSaat.Text = dr["NoteClock"].ToString();
                txtBaslık.Text = dr["NoteTitle"].ToString();
                rchTxtDetay.Text = dr["NoteDetail"].ToString();
                txtOlusturan.Text = dr["NoteConstituent"].ToString();
                txtHitap.Text = dr["NoteHitap"].ToString(); 
            }
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
                dialog = MessageBox.Show("Not Silinsin Mi?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SqlCommand komutSil = new SqlCommand("Delete from NoteTbl Where NoteID=@p1", bgl.baglanti());
                    komutSil.Parameters.AddWithValue("@p1", txtID.Text);
                    komutSil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Not Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into NoteTbl (NoteDate,NoteClock,NoteTitle,NoteDetail,NoteConstituent,NoteHitap) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", msktxtSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslık.Text);
            komut.Parameters.AddWithValue("@p4", rchTxtDetay.Text);
            komut.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p6", txtHitap.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update NoteTbl set NoteDate=@p1,NoteClock=@p2,NoteTitle=@p3,NoteDetail=@p4,NoteConstituent=@p5,NoteHitap=@p6 where NoteID=@p7", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", msktxtSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslık.Text);
            komut.Parameters.AddWithValue("@p4", rchTxtDetay.Text);
            komut.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p6", txtHitap.Text);
            komut.Parameters.AddWithValue("@p7", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Başarılı Şekilde Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNoteDetay frmd = new FrmNoteDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frmd.metin = dr["NoteDetail"].ToString();
            }
            frmd.Show();
        }
    }
}
