﻿using System;
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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from AdminTbl", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtKullaniciadi.Text = "";
            txtSifre.Text = "";
            txtID.Text = "";
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void Btnİslem_Click(object sender, EventArgs e)
        {
            if (Btnİslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into AdminTbl values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtKullaniciadi.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if (Btnİslem.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update AdminTbl set Sifre=@p2,KullaniciAdi=@p1 where AdminID=@p3", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtKullaniciadi.Text);
                komut1.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut1.Parameters.AddWithValue("@p3", txtID.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["AdminID"].ToString();
                txtKullaniciadi.Text = dr["KullaniciAdi"].ToString();
                txtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                Btnİslem.Text = "Güncelle";
                Btnİslem.BackColor = Color.LightYellow;
            }
            else
            {
                Btnİslem.Text = "Kaydet";
                Btnİslem.BackColor = Color.Yellow;
            }
        }
    }
}
