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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TECNOCLUBB
{
    public partial class Form2 : Form
    {
        SqlConnection baglanti2;
        SqlCommand komut2;
        SqlDataAdapter da2;

        public Form2()
        {
            InitializeComponent();
        }
        void kampanyagetir()
        {
            baglanti2 = new SqlConnection("server=LAPTOP-8CQIUM09;Initial Catalog=TECNOCLUB;Integrated Security=SSPI");
            baglanti2.Open();
            da2 = new SqlDataAdapter("SELECT *FROM [KAMPANYALAR TABLOSU]", baglanti2);
            DataTable tablo = new DataTable();
            da2.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti2.Close();
        }

     

       private void label5_Click(object sender, EventArgs e)
        {//label 5

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            kampanyagetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtıd.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtmik.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO [KAMPANYALAR TABLOSU](kampanya_adi,indirimmiktari,baslangic_tarihi,bitis_tarihi) VALUES (@kampanya_adi,@indirimmiktari,@baslangic_tarihi,@bitis_tarihi) ";
            DateTime baslangic_tarihi = dateTimePicker1.Value;
            DateTime bitis_tarihi= dateTimePicker2.Value;
            komut2 = new SqlCommand(sorgu, baglanti2);
            komut2.Parameters.AddWithValue("@kampanya_adi",txtad.Text);
            komut2.Parameters.AddWithValue("@indirimmiktari", txtmik.Text);
            komut2.Parameters.AddWithValue("@baslangic_tarihi", baslangic_tarihi);
            komut2.Parameters.AddWithValue("@bitis_tarihi", bitis_tarihi);
            baglanti2.Open();
            komut2.ExecuteNonQuery();
            baglanti2.Close();
            kampanyagetir();

        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM [KAMPANYALAR TABLOSU] WHERE kampanya_id = @kampanya_id";
            komut2 = new SqlCommand(sorgu, baglanti2);
            komut2.Parameters.AddWithValue("@kampanya_id", Convert.ToInt32(txtıd.Text));
            baglanti2.Open() ;
            komut2.ExecuteNonQuery();
            baglanti2.Close();
            kampanyagetir();
             
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 gecis = new Form1();
            gecis.Show();
            this.Hide();
        }
    }
}
