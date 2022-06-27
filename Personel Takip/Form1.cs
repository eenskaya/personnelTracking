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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
        public static string tcno, ad, soyad, yetki;

        private void button1_Click(object sender, EventArgs e)
        {
            if(hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString
                            () == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            ad = kayitokuma.GetValue(1).ToString();
                            soyad = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;
                        }

                    }


                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString
                            () == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            ad = kayitokuma.GetValue(1).ToString();
                            soyad = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }

                    }

                }
                if (durum == false)
                    hak--;
                baglantim.Close();
            }
            label5.Text = Convert.ToString(hak);
            if(hak==0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı", "Sky Personel Takip Programı ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        int hak = 3;bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi... ";
            this.AcceptButton = button1; this.CancelButton = button2;
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                }
    }
}
