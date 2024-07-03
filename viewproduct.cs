using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practicle_cw
{
    public partial class viewproduct : Form
    {
        public viewproduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM additem", conn);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                this.dataGridView1.DataSource = dt;
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                //Difine a Command
                string sql = "SELECT * FROM additem WHERE pname =@additem ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@additem", this.txtsearch.Text);

                //Access Data using DataReader Methord
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    //Bind Data with Control
                    this.textBox4.Text = reader.GetValue(0).ToString();
                    this.textBox3.Text = reader.GetValue(1).ToString();
                    this.textBox2.Text = reader.GetValue(2).ToString();


                    if (!reader.IsDBNull(reader.GetOrdinal("photo")))
                    {
                        byte[] imgByte = (byte[])reader["photo"];
                        using (MemoryStream ms = new MemoryStream(imgByte))
                        {
                            this.txtimage.Image = Image.FromStream(ms);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Records Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            this.textBox4.Text = row.Cells[0].Value.ToString();
            this.textBox3.Text = row.Cells[1].Value.ToString();
            this.textBox2.Text = row.Cells[2].Value.ToString();
            if (row.Cells["photo"].Value != null && !(row.Cells["photo"].Value is DBNull))
            {
                byte[] imgByte = (byte[])row.Cells["photo"].Value;
                using (MemoryStream ms = new MemoryStream(imgByte))
                {
                    this.txtimage.Image = Image.FromStream(ms);
                }
            }
        }

        private void viewproduct_Load(object sender, EventArgs e)
        {

        }
    }
}
