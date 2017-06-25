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

namespace ProductManagement
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBoxStatus.SelectedIndex = 0;
            LoadData();
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emirm\Desktop\C#\GitHub\ProductManagement\ProductManagement\ProductDB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            bool status = false;
            if (comboBoxStatus.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var sqlQuery = "";
            if (IfProductExists(con, textBox1.Text))
            {
                sqlQuery= @"UPDATE [Products] SET [ProductName] = '"+ textBox2.Text +"',[ProductStatus] = '"+ status + "' WHERE [ProductCode] = '"+ textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [Products]
                            ([ProductCode],[ProductName],[ProductStatus])
                            VALUES
                            ('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "', '" + status + "')";
            }

            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
        }

        private bool IfProductExists(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [Products] WHERE [ProductCode] = '"+ productCode +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emirm\Desktop\C#\GitHub\ProductManagement\ProductManagement\ProductDB.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Not Active";
                }
            }
            textBox1.Clear();
            textBox2.Clear();
            comboBoxStatus.SelectedIndex = 0;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBoxStatus.SelectedIndex = 0;
            }
            else
            {
                comboBoxStatus.SelectedIndex = 1;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emirm\Desktop\C#\GitHub\ProductManagement\ProductManagement\ProductDB.mdf;Integrated Security=True;Connect Timeout=30");
            var sqlQuery = "";
            if (IfProductExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Product not found");
            }

            LoadData();
        }
    }
}
