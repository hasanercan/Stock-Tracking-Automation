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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void Form9_Load(object sender, EventArgs e)
        {
            sepetlistele();
            hesapla();
        }
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Sepet", baglanti);
            adtr.Fill(daset, "Sepet");
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
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyat) from Sepet", baglanti);
                textBox9.Text = komut.ExecuteScalar() + "TL";
                baglanti.Close();
            }
            catch (Exception)
            {

                ;
            }
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

        private void button11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
