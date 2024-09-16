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

namespace Test_Trigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // "

        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-BC3LOP2\SQLEXPRESS01;Initial Catalog=yedeklemetest;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
           
            listele();
            sayac();

        }

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKITAPLAR",bgl); 
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void sayac()
        {
            bgl.Open();
            SqlCommand cmd = new SqlCommand("Select*From TBLSAYAC", bgl);
            ;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblKitapAdet.Text = dr[0].ToString();
            }
            bgl.Close();

        }


        private void Btn_ekle_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand kmt = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVİ,TUR) values (@p1,@p2,@p3,@p4,@p5)",bgl);
            kmt.Parameters.AddWithValue("@p1", Txt_Ad.Text);
            kmt.Parameters.AddWithValue("@p2",Txt_Yazar.Text);
            kmt.Parameters.AddWithValue("@p3", Txt_Sayfa.Text);
            kmt.Parameters.AddWithValue("@p4",Txt_Yayınevi.Text);
            kmt.Parameters.AddWithValue("@p5", Txt_Tür.Text);
            kmt.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kitap Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            sayac();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            Txt_Id.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txt_Ad .Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            Txt_Yazar .Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            Txt_Sayfa .Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            Txt_Yayınevi .Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            Txt_Tür .Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();




        }

        private void Btn_sil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand kmt = new SqlCommand("DELETE FROM TBLKITAPLAR WHERE ID=@p1", bgl);
            ;
            kmt.Parameters.AddWithValue("@p1",Txt_Id.Text);
            kmt.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kitap sistemden silindi");
            listele();
            sayac();
        }
    }
}
