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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        bool durum;
        private void barkodengelle()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Ürünler", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox1.Text==read["barkod_no"].ToString() || textBox1.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void Kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kategoriler", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["Kategori"].ToString());
            }
            baglanti.Close();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            Kategorigetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Markalar where Kategori='"+comboBox1.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox2.Items.Add(read["Marka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            barkodengelle();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Ürünler(barkod_no,Katagori,Marka,Ürünadı,StokSayısı,Alısfiyatı,SAtısfiyatı,tarih) values(@barkod_no,@Katagori,@Marka,@Ürünadı,@StokSayısı,@Alısfiyatı,@SAtısfiyatı,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@barkod_no", textBox1.Text);
                komut.Parameters.AddWithValue("@Katagori", comboBox1.Text);
                komut.Parameters.AddWithValue("@Marka", comboBox2.Text);
                komut.Parameters.AddWithValue("@Ürünadı", textBox2.Text);
                komut.Parameters.AddWithValue("@StokSayısı", int.Parse(textBox3.Text));
                komut.Parameters.AddWithValue("@Alısfiyatı", double.Parse(textBox4.Text));
                komut.Parameters.AddWithValue("@SAtısfiyatı", double.Parse(textBox5.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Ürün başarıyla eklendi.");
            }
            else
            {
                MessageBox.Show("Barkod No zaten mevcut", "Uyarı!!");
            }
           
            comboBox2.Items.Clear();
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Ürünler set StokSayısı=StokSayısı+'"+int.Parse(textBox8.Text)+"' where barkod_no='"+textBox11.Text+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Mevcut ürün ekleme başarılı");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                label15.Text = "";
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Ürünler where barkod_no like'" + textBox11.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox4.Text = read["Katagori"].ToString();
                comboBox3.Text = read["Marka"].ToString();
                textBox9.Text = read["Ürünadı"].ToString();
                label15.Text = read["StokSayısı"].ToString();
                textBox7.Text = read["Alısfiyatı"].ToString();
                textBox6.Text = read["Satısfiyatı"].ToString();
            }
            baglanti.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
