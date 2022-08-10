using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Sepet", baglanti);
            adtr.Fill(daset,"Sepet");
            dataGridView1.DataSource = daset.Tables["Sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }
        private void hesapla()
        {
            try
            {
               
            }
            catch (Exception)
            {

                ;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sepetlistele();
            hesapla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 ekle = new Form2();
            ekle.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 ekle = new Form3();
            ekle.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 ekle = new Form4();
            ekle.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form5 ekle = new Form5();
            ekle.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form6 ekle = new Form6();
            ekle.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form7 ekle = new Form7();
            ekle.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                textBox2.Text = "";
                textBox3.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Müşteriler where tc_no like '"+textBox1.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["ad_soyad"].ToString();
                textBox3.Text = read["telefon"].ToString();
               
            }
            baglanti.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Ürünler where barkod_no like '" + textBox4.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox5.Text = read["Ürünadı"].ToString();
                textBox7.Text = read["Satısfiyatı"].ToString();


            }
            baglanti.Close();
        }

        private void Temizle()
        {
            if (textBox4.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != textBox6)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Sepet",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox4.Text==read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Sepet(tc,adsoyad,telefon,barkodno,ürünadı,stoksayısı,satışfiyatı,toplamfiyat,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@ürünadı,@stoksayısı,@satışfiyatı,@toplamfiyat,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", textBox1.Text);
                komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
                komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                komut.Parameters.AddWithValue("@barkodno", textBox4.Text);
                komut.Parameters.AddWithValue("@ürünadı", textBox5.Text);
                komut.Parameters.AddWithValue("stoksayısı", int.Parse(textBox6.Text));
                komut.Parameters.AddWithValue("satışfiyatı", double.Parse(textBox7.Text));
                komut.Parameters.AddWithValue("toplamfiyat", double.Parse(textBox8.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update Sepet set stoksayısı=stoksayısı+'"+int.Parse(textBox6.Text)+ "'where barkodno='" + textBox4.Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                SqlCommand komut3 = new SqlCommand("update Sepet set toplamfiyat=stoksayısı*satışfiyatı where barkodno='"+textBox4.Text+"'", baglanti);               
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            textBox6.Text = "1";
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
            MessageBox.Show("Ürün başarıyla sepete eklendi");
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != textBox6)
                    {
                        item.Text = "";
                    }
                }

            }

            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    if (item != textBox6)
                    {
                        item.Text = "";
                    }
                }

            }

                }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox8.Text = "";
            }
            try
            {
                textBox8.Text=(double.Parse(textBox6.Text)* double.Parse(textBox7.Text)).ToString() ;
            }
            catch (Exception)
            {

                ;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox8.Text = (double.Parse(textBox6.Text) * double.Parse(textBox7.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Sepet where barkodno ='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün sepettten çıkarıldı.");
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Sepet ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün sepettten çıkarıldı.");
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form8 ekle = new Form8();
            ekle.ShowDialog();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Satışlar(tc,adsoyad,telefon,barkodno,ürünadı,stoksayısı,satışfiyatı,toplamfiyat,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@ürünadı,@stoksayısı,@satışfiyatı,@toplamfiyat,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", dataGridView1.Rows[i].Cells["tc"].Value.ToString());
                komut.Parameters.AddWithValue("@adsoyad", dataGridView1.Rows[i].Cells["adsoyad"].Value.ToString());
                komut.Parameters.AddWithValue("@telefon", dataGridView1.Rows[i].Cells["telefon"].Value.ToString());
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@ürünadı", dataGridView1.Rows[i].Cells["ürünadı"].Value.ToString());
                komut.Parameters.AddWithValue("stoksayısı", int.Parse(dataGridView1.Rows[i].Cells["stoksayısı"].Value.ToString()));
                komut.Parameters.AddWithValue("satışfiyatı", double.Parse(dataGridView1.Rows[i].Cells["satışfiyatı"].Value.ToString()));
                komut.Parameters.AddWithValue("toplamfiyat", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyat"].Value.ToString()));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                SqlCommand komut2 = new SqlCommand("update Ürünler set StokSayısı=StokSayısı-'" + int.Parse(dataGridView1.Rows[i].Cells["stoksayısı"].Value.ToString()) + "' where barkod_no='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış başarıyla gerçekleşti");


               
            }
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("delete from Sepet ", baglanti);
            komut5.ExecuteNonQuery();
            baglanti.Close();

            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form9 ekle = new Form9();
            ekle.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
