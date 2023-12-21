using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TECNOCLUBB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {//kullanıcı adı

        }
             int sayac = 0;
        private void button1_Click(object sender, EventArgs e)
        {
           

           String kullaniciadi = "admin";
            String sifre = "admin123";
            if (textBox1.Text == kullaniciadi && textBox2.Text == sifre)
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
            }
            else
            {
                sayac++;
                MessageBox.Show("Kullanıcı Adı ve/veya Şifreniz yanlış.Kalan deneme hakkınız:" + (3 - sayac), "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(sayac == 3)
                {
                    MessageBox.Show("başka deneme hakkınız kalmadı.Program sonlandırılıyor :(");
                    Application.Exit();
                }
            }

        }  
    }
}
