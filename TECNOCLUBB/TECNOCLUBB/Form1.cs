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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TECNOCLUBB
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        
        public Form1()
        {
            InitializeComponent();
        }
        void bilgigetir()
        {
            baglanti = new SqlConnection("server=LAPTOP-8CQIUM09;Initial Catalog=TECNOCLUB;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT satisid ,urun_adi ,musteriad+' '+musterisoyad AS 'MUSTERI' ,telefon,personelad+' '+personelsoyad AS 'PERSONEL' ,adet ,fiyat ,puan  FROM [SATIS TABLOSU] INNER JOIN [URUNLER TABLOSU] ON [SATIS TABLOSU].urun=[URUNLER TABLOSU].urun_id INNER JOIN [MUSTERİLER TABLOSU] ON [SATIS TABLOSU].musteri=[MUSTERİLER TABLOSU].musteri_id  INNER JOIN [PERSONEL TABLOSU] ON [PERSONEL TABLOSU].personel_id=[SATIS TABLOSU].personel INNER JOIN [PUANLAMA TBALOSU] ON [PUANLAMA TBALOSU].puan_id=[MUSTERİLER TABLOSU].musteri_id", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bilgigetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textıd.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textmus.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            texttel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textper.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textsayi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textfiyat.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();    
            textpuan.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {//ekle
            
                string sorgu = @"
                       INSERT INTO [SATIS TABLOSU] (urun, musteri, personel, adet, topfiyat)
                     VALUES (
                     (SELECT TOP 1 urun_id FROM [URUNLER TABLOSU] WHERE urun_adi = @urun_adi ),
                      (SELECT musteri_id FROM [MUSTERİLER TABLOSU] WHERE musteriad+' '+musterisoyad = @MUSTERI),
                       (SELECT personel_id FROM [PERSONEL TABLOSU] WHERE personelad+' '+personelsoyad = @PERSONEL),
                          @adet, @fiyat)";

                baglanti.Open();
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@urun_adi", textad.Text);
                    komut.Parameters.AddWithValue("@MUSTERI", textmus.Text);
                    komut.Parameters.AddWithValue("@telefon", texttel.Text); 
                    komut.Parameters.AddWithValue("@PERSONEL", textper.Text);
                    komut.Parameters.AddWithValue("@adet", textsayi.Text);
                    komut.Parameters.AddWithValue("@fiyat", textfiyat.Text);
                   ///komut.Parameters.AddWithValue("@puan", textpuan.Text);
                    komut.ExecuteNonQuery();
                    
                    
                }
                  baglanti.Close();
                  bilgigetir();
                
        }
        private void btnsil_Click(object sender, EventArgs e)
        {//sil
            string sorgu = @"
        DELETE FROM [SATIS TABLOSU]
        WHERE satisid = @satisid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@satisid", Convert.ToInt32(textıd.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            bilgigetir();

        }

        private void btnup_Click(object sender, EventArgs e)
        {//güncelle
            string sorgu = @"
        UPDATE [SATIS TABLOSU]
        SET 
            urun = (SELECT TOP 1 urun_id FROM [URUNLER TABLOSU] WHERE urun_adi = @urun_adi),
            musteri = (SELECT musteri_id FROM [MUSTERİLER TABLOSU] WHERE musteriad+' '+musterisoyad = @MUSTERI),
            personel = (SELECT personel_id FROM [PERSONEL TABLOSU] WHERE personelad+' '+personelsoyad = @PERSONEL),
            adet = @adet,
            topfiyat = @topfiyat
        WHERE satisid = @satisid";

            baglanti.Open();
            using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@urun_adi", textad.Text);
                komut.Parameters.AddWithValue("@MUSTERI", textmus.Text);
                komut.Parameters.AddWithValue("@telefon", texttel.Text);
                komut.Parameters.AddWithValue("@PERSONEL", textper.Text);
                komut.Parameters.AddWithValue("@adet", textsayi.Text);
                komut.Parameters.AddWithValue("@topfiyat", textfiyat.Text); 
                komut.Parameters.AddWithValue("@satisid", Convert.ToInt32(textıd.Text));
                komut.ExecuteNonQuery();
            }

            bilgigetir();
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {//KAMPANYA EKLE

            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
    }
}
