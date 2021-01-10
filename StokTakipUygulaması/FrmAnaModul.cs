using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipUygulaması
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }
        public string kullanici;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (frma == null || frma.IsDisposed)
            {
                frma = new FrmAnaSayfa();
                frma.MdiParent = this;
                frma.Show();
            }
        }
        FrmUrunler fru;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fru == null || fru.IsDisposed)
            {
                fru = new FrmUrunler();
                fru.MdiParent = this;
                fru.Show();
            }
        }
        FrmMusteriler frm;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmMusteriler();
                frm.MdiParent = this;
                frm.Show();
            }
        }
        FrmFirmalar frf;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frf == null || frf.IsDisposed)
            {
                frf = new FrmFirmalar();
                frf.MdiParent = this;
                frf.Show();
            }
        }
        FrmPersonel frp;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frp == null || frp.IsDisposed)
            {
                frp = new FrmPersonel();
                frp.MdiParent = this;
                frp.Show();
            }
        }
        FrmRehber frr;
        private void BtnRehberler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frr == null || frr.IsDisposed)
            {
                frr = new FrmRehber();
                frr.MdiParent = this;
                frr.Show();
            }
        }
        FrmGiderler frmg;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmg == null || frmg.IsDisposed)
            {
                frmg = new FrmGiderler();
                frmg.MdiParent = this;
                frmg.Show();
            }
        }
        FrmBankalar frmb;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmb == null || frmb.IsDisposed)
            {
                frmb = new FrmBankalar();
                frmb.MdiParent = this;
                frmb.Show();
            }
        }
        FrmFaturalar frmt;
        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmt == null || frmt.IsDisposed)
            {
                frmt = new FrmFaturalar();
                frmt.MdiParent = this;
                frmt.Show();
            }
        }
        FrmNotlar frmn;
        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmn == null || frmn.IsDisposed)
            {
                frmn = new FrmNotlar();
                frmn.MdiParent = this;
                frmn.Show();
            }
        }
        FrmHareketler frmh;
        private void BtnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmh == null || frmh.IsDisposed)
            {
                frmh = new FrmHareketler();
                frmh.MdiParent = this;
                frmh.Show();
            }
        }
        FrmStoklar frms;
        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frms == null || frms.IsDisposed)
            {
                frms = new FrmStoklar();
                frms.MdiParent = this;
                frms.Show();
            }
        }
        FrmAyarlar frmay;
        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmay == null || frmay.IsDisposed)
            {
                frmay = new FrmAyarlar();
                frmay.Show();
            }
        }
        FrmKasa frmks;
        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmks == null || frmks.IsDisposed)
            {
                frmks = new FrmKasa();
                frmks.ad = kullanici;
                frmks.MdiParent = this;
                frmks.Show();
            }
        }
        FrmAnaSayfa frma;
        private void BtnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frma==null || frma.IsDisposed)
            {
                frma = new FrmAnaSayfa();
                frma.MdiParent = this;
                frma.Show();
            }
        }
    }
}
