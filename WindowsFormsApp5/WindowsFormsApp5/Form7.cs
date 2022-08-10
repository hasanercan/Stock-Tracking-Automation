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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        DataSet daset = new DataSet();
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update Ürünler set Ürünadı=@Ürünadı,StokSayısı=@StokSayısı,Alısfiyatı=@Alısfiyatı,Satısfiyatı=@Satısfiyatı where barkod_no=@barkod_no", baglanti);
                komut.Parameters.AddWithValue("@barkod_no", textBox11.Text);
                komut.Parameters.AddWithValue("@Ürünadı", textBox9.Text);
                komut.Parameters.AddWithValue("@StokSayısı", int.Parse(textBox8.Text));
                komut.Parameters.AddWithValue("@Alısfiyatı", double.Parse(textBox7.Text));
                komut.Parameters.AddWithValue("@Satısfiyatı", double.Parse(textBox6.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["Ürünler"].Clear();
                ÜrünListele();
                MessageBox.Show("Ürün güncelleme başarılı."); 
                
            }
            else
            {
                MessageBox.Show("Lütfen ürün seçimi yapınız.", "Uyarı!!");
            }
            foreach (Control item in this.Controls )
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            ÜrünListele();
            Kategorigetir();
        }

        private void ÜrünListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Ürünler", baglanti);
            adtr.Fill(daset, "Ürünler");
            dataGridView1.DataSource = daset.Tables["Ürünler"];
            baglanti.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox11.Text = dataGridView1.CurrentRow.Cells["barkod_no"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Katagori"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Marka"].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells["Ürünadı"].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells["StokSayısı"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["Alısfiyatı"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["SAtısfiyatı"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox11.Text!="" && comboBox1.Text!="" && comboBox2.Text!="" )
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update Ürünler set Katagori=@Katagori,Marka=@Marka where barkod_no=@barkod_no", baglanti);
                komut.Parameters.AddWithValue("@barkod_no", textBox11.Text);
                komut.Parameters.AddWithValue("@Katagori", comboBox1.Text);
                komut.Parameters.AddWithValue("@Marka", comboBox2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["Ürünler"].Clear();
                ÜrünListele();
                MessageBox.Show("Kategori ve marka güncelleme başarılı.");
            }
            else
            {
                MessageBox.Show("Lütfen ürün seçimi ve kategori , marka saçimlerini yapınız.","Uyarı!!");
            }
           
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Markalar where Kategori='" + comboBox1.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox2.Items.Add(read["Marka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Ürünler where barkod_no='" + dataGridView1.CurrentRow.Cells["barkod_no"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Ürünler"].Clear();
            ÜrünListele();
            MessageBox.Show("Kayıt başarıyla silindi.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Ürünler where barkod_no like'%" + textBox1.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
