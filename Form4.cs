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

namespace FoodApp
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            showData();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\amali\Documents\star.mdf;Integrated Security=True;Connect Timeout=30");
        private void showData()
        {
            con.Open();
            string q = "select * from AM";
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            string q = "select * from AM where name like '" + txtSearch.Text + "%' ";
            SqlDataAdapter laye = new SqlDataAdapter(q, con);
            var ds = new DataSet();
            laye.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
            txtDrinks.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
            txtPrice.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
           

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            int price = Convert.ToInt32(txtPrice.Text);
            con.Open();
            string q = "insert into GB values('" + id + "','" + txtDrinks.Text + "','" + price + "')";
            SqlCommand command = new SqlCommand(q, con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");
            showData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string qr = "Delete from AM where id = '" + txtID.Text + "' ";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted");
            showData();
        }
    }
}
