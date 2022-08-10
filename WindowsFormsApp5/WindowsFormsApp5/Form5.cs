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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        bool durum;
        private void kategoriengelle()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kategoriler", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox1.Text==read["Kategori"].ToString() || textBox1.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kategoriengelle();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Kategoriler(Kategori) values('" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                textBox1.Text = "";
                MessageBox.Show("Kategori başarıyla eklendi.");
            }
            else
            {
                MessageBox.Show("Kategori zaten mevcut", "Uyarı!!");
            }
            textBox1.Text = "";
        }
    }
}
