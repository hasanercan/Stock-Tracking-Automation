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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Sky;Initial Catalog=stok_takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void satışlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Satışlar", baglanti);
            adtr.Fill(daset, "Satışlar");
            dataGridView1.DataSource = daset.Tables["Satışlar"];
            
            baglanti.Close();
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            satışlistele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
