using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Personel_Takip
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0; Data Source=personel.accdb");
        private void personelleri_göster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personeller_listesi = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO], ad AS[ADI],soyad AS[SOYADI]" +
                    ",cinsiyet as[CİNSİYETİ], mezuniyet as[MEZUNİYETİ],dogumtarihi as[DOĞUM TARİHİ], görevi as[GÖREVİ], görevyeri as[GÖREV YERİ]," +
                    "maasi as[MAAŞI] from personeller Order by ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                personeller_listesi.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                throw;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            personelleri_göster();
            this.Text = "KULLANICI İŞLEMLERİ";
            label19.Text = Form1.ad + " " + Form1.soyad;
            pictureBox1.Height = 150;pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Height = 150;pictureBox2.Width = 150;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch (Exception)
            {

                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");

            }
            maskedTextBox1.Mask = "00000000000";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (maskedTextBox1.Text.Length==11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0) + ".jpg");

                    }
                    catch
                    {

                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");
                    }
                    label10.Text = kayitokuma.GetValue(1).ToString();
                    label11.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Bay")
                        label12.Text = "Bay  ";
                    else
                        label12.Text = "Bayan";
                    label13.Text = kayitokuma.GetValue(4).ToString();
                    label14.Text = kayitokuma.GetValue(5).ToString();
                    label15.Text = kayitokuma.GetValue(6).ToString();
                    label16.Text = kayitokuma.GetValue(7).ToString();
                    label17.Text = kayitokuma.GetValue(8).ToString();
                    break;
                }
                if (kayit_arama_durumu==false)
                
                    MessageBox.Show("Aranan Kayıt Bulunamadı !");
                    baglantim.Close();   
            }
            else

                MessageBox.Show("11 Haneli bir Tc kimlik No Giriniz! ");
        }
    }
}
