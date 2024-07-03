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
    public partial class pharmacy : UserControl
    {
        DataSet dataset;
        int f;
        public pharmacy()
        {
            InitializeComponent();
        }
        public pharmacy(int q)
        {
            InitializeComponent();
            f = q;
        }

        private void pharmacy_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM additem WHERE category = 'pharmacy'", conn);
                dataset = new DataSet();

                adapter.Fill(dataset);

                this.label1.Text = dataset.Tables[0].Rows[f][0].ToString();
                this.label3.Text = dataset.Tables[0].Rows[f][1].ToString();
                this.label2.Text = dataset.Tables[0].Rows[f][2].ToString();

                DataRow row = dataset.Tables[0].Rows[f];
                if (row["photo"] != DBNull.Value)
                {
                    byte[] imgByte = (byte[])row["photo"];
                    using (MemoryStream ms = new MemoryStream(imgByte))
                    {
                        this.pictureBox1.Image = Image.FromStream(ms);
                    }
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double price, total = 0;
            double qty;
            // Create connection with SQL
            string connectionString = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {


                    conn.Open();

                    // Define command with parameterized query
                    string sql = "INSERT INTO item (Item_Name, qty, unit, price) VALUES (@ItemName, @Qty, @Unit, @Price)";
                    string sql2 = "INSERT INTO income (Item_Name, qty, unit, price,date) VALUES (@ItemName, @Qty, @Unit, @Price,@date)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlCommand cmd1 = new SqlCommand(sql2, conn);

                    // Set parameter values
                    cmd.Parameters.AddWithValue("@ItemName", this.label1.Text);
                    cmd.Parameters.AddWithValue("@Qty", this.textBox1.Text);
                    cmd.Parameters.AddWithValue("@Unit", "-");

                    //calculate
                    price = Convert.ToDouble(this.label2.Text);
                    qty = Convert.ToDouble(this.textBox1.Text);



                    total = price * qty;




                    cmd.Parameters.AddWithValue("@Price", total);

                    // Execute command to insert data
                    int rowsAffected = cmd.ExecuteNonQuery();

                    cmd1.Parameters.AddWithValue("@ItemName", this.label1.Text);
                    cmd1.Parameters.AddWithValue("@Qty", this.textBox1.Text);
                    cmd1.Parameters.AddWithValue("@Unit", "-");
                    cmd1.Parameters.AddWithValue("@Price", total);
                    cmd1.Parameters.AddWithValue("@date", DateTime.Now);
                    // Execute command to insert data
                    int rowsAffected1 = cmd1.ExecuteNonQuery();

                    MessageBox.Show(rowsAffected + " item(s) added.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
