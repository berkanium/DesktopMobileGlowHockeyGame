using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Glow_Hockey_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int tabanSinusu = 5, tabanCosinusu = 5, skor = 0, CanHakkım = 3, saniye = 90, level = 1;
        private void TemasAlanı()
        {
            KarşıOyuncu.Location = new Point(OyunTopu.Location.X, KarşıOyuncu.Location.Y);
            if (OyunTopu.Top <= KarşıOyuncu.Bottom)
            {
                tabanCosinusu = tabanCosinusu * -1;
                skor = skor + 10;
                lblPuan.Text = skor.ToString();
            }
            if (OyunTopu.Bottom >= BenimOyuncum.Top && OyunTopu.Left >= BenimOyuncum.Left && OyunTopu.Right <= BenimOyuncum.Right) tabanCosinusu = tabanCosinusu * -1;
            else if (OyunTopu.Right >= label5.Left) tabanSinusu = tabanSinusu * -1;
            else if (OyunTopu.Left <= label6.Right) tabanSinusu = tabanSinusu * -1;
            else if (OyunTopu.Bottom >= label8.Top && OyunTopu.Left <= label8.Right) tabanCosinusu = tabanCosinusu * -1;
            else if (OyunTopu.Bottom >= label3.Top && OyunTopu.Right >= label3.Left) tabanCosinusu = tabanCosinusu * -1;

        }
        private void KaybetmeAnı(object sender,EventArgs e)
        {
            if (OyunTopu.Top >= label3.Bottom)
            {
                if (CanHakkım>0)
                {
                    tmrHareket.Stop();
                    CanHakkım--;
                    tmrSaniye.Stop();
                    MessageBox.Show("Yandınız...Kaybettiniz Kalan Can = " + CanHakkım.ToString());
                    Form1_Load(sender, e);   
                }
                if (CanHakkım==0)
                {
                    tmrHareket.Stop();
                    tmrSaniye.Stop();
                    MessageBox.Show("Oyunu Kaybettiniz", "Kaybetme Protokolü", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    skorSave();

                }
                lblKalanCan.Text = "X" + CanHakkım;
            }
        }
        private void TopHareketMerkezi()
        {
            OyunTopu.Location = new Point(248 , 245);
            if (tabanCosinusu > 0) tabanCosinusu = tabanCosinusu * -1;
        }
        private void lvlYukselmesi()
        {
            saniye--;
            if (saniye==0)
            {
                tmrHareket.Stop();
                tmrSaniye.Stop();
                level++;
                TopHareketMerkezi();
                if (tabanSinusu < 0) tabanSinusu = tabanSinusu - 1;
                else tabanSinusu = tabanSinusu + 1;
                if (tabanCosinusu < 0) tabanCosinusu = tabanCosinusu - 1;
                else tabanCosinusu = tabanCosinusu + 1;
                lblLevelHaberi.Text ="Helal Sana "+" "+level+ " " + "Levele Yükseldin Bravo";
                haberVerme();
                saniye = 60;
            }
            lblKalanSaniyem.Text = saniye.ToString();
            lblLevelSayısı.Text = level.ToString();
        }
        private void haberVerme()
        {
            pnlHaberi.Visible = true;
            pnlHaberi.Location=new Point(115 , 205);
        }
        private void skorSave()
        {
            if (skor > GlowHockey.Default.skor)
            {
                GlowHockey.Default.skor = skor;
                GlowHockey.Default.Save();
            }
            lblSkor.Text = GlowHockey.Default.skor.ToString();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > 34 && e.X <= 432)
            BenimOyuncum.Left = e.X;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            lblSkor.Text = GlowHockey.Default.skor.ToString() ;
            TopHareketMerkezi();
            tmrHareket.Enabled = true;
            tmrSaniye.Enabled = true;
            KarşıOyuncu.Text = "Ö0ZCAN :)";
            BenimOyuncum.Text = "Berkan";

        }

        private void tmrHareket_Tick(object sender, EventArgs e)
        {
            TemasAlanı();
            KaybetmeAnı(sender, e);
            OyunTopu.Location = new Point(OyunTopu.Location.X+tabanSinusu,OyunTopu.Location.Y+tabanCosinusu);

        }

        private void tmrSaniye_Tick(object sender, EventArgs e)
        {
            lvlYukselmesi();
        }

        private void btnOynatma_Click(object sender, EventArgs e)
        {
            tmrSaniye.Enabled = true;
            tmrHareket.Enabled = true;
            pnlHaberi.Visible = false;
        }
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            lblKalanCan.Text = "X" + CanHakkım;
            CanHakkım = 3;
            tmrSaniye.Enabled = true;
            tmrHareket.Enabled = true;
            saniye = 55;
            
        }
        DialogResult cikis = new DialogResult();
        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            cikis = MessageBox.Show("GlowHockey Oyunu Kapatılsın Mı ?", "GlowHockey", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (cikis==DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("GlowHockey Oyununu Kapatılmadı", "GlowHockey", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
    }
}
