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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Müşteriler where tc_no='"+dataGridView1.CurrentRow.Cells["tc_no"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Müşteriler"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kayıt başarıyla silindi.");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from Müşteriler", baglanti);
            adtr.Fill(daset, "Müşteriler");
            dataGridView1.DataSource = daset.Tables["Müşteriler"];
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["tc_no"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["ad_soyad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Müşteriler set ad_soyad=@ad_soyad,telefon=@telefon,adres=@adres,email=@email where tc_no=@tc_no", baglanti);
            komut.Parameters.AddWithValue("@tc_no", textBox1.Text);
            komut.Parameters.AddWithValue("@ad_soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@telefon", textBox3.Text);
            komut.Parameters.AddWithValue("@adres", textBox4.Text);
            komut.Parameters.AddWithValue("@email", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Müşteriler"].Clear();
            Kayıt_Göster(); 
            MessageBox.Show("Müşteri kaydı başarıyla güncellendi.");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
               
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Müşteriler where tc_no like'%"+textBox6.Text+"%'",baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
