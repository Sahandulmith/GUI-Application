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
    public partial class sampleadd2 : Form
    {
        DataSet dataSet;
        public sampleadd2()
        {
            InitializeComponent();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtimage_Click(object sender, EventArgs e)
        {
            
        }
        public Image image
        {
            get { return txtimage.Image; }
            set { txtimage.Image = value; }
        }
        private byte[] getphoto()
        {
            MemoryStream stream = new MemoryStream();
            txtimage.Image.Save(stream, txtimage.Image.RawFormat);

            return stream.GetBuffer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) ;
            {
                txtimage.Image = new Bitmap(ofd.FileName);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                string deleteQuery = "DELETE FROM additem WHERE pname = '" + textBox4.Text + "'";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);

                // Execute the command
                int rowsAffected = cmd.ExecuteNonQuery();

                DialogResult ms = MessageBox.Show("Are you Sure Remove Item", "Remove Item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ms == DialogResult.OK)
                {
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                try
                {

                    //Difine a Command
                    string sql = "UPDATE additem SET pname =@additem,price=@price,sell_price= @sell_price,photo =@photo WHERE pname =@additem";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@additem", this.textBox4.Text);
                    cmd.Parameters.AddWithValue("@price", this.textBox3.Text);
                    cmd.Parameters.AddWithValue("@sell_price", this.textBox2.Text);
                    cmd.Parameters.AddWithValue("@photo", getphoto());

                    //Excute Command to insert Data
                    int retur = cmd.ExecuteNonQuery();

                    MessageBox.Show("Your account Update successfully." + retur);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //Disconnect from server
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
            this.textBox1.Text = row.Cells[4].Value.ToString();
            if (row.Cells["photo"].Value != null && !(row.Cells["photo"].Value is DBNull))
            {
                byte[] imgByte = (byte[])row.Cells["photo"].Value;
                using (MemoryStream ms = new MemoryStream(imgByte))
                {
                    this.txtimage.Image = Image.FromStream(ms);
                }
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                //Difine command
                string sql = "INSERT INTO additem VALUES(@additem, @price, @sell_price,@photo,@category)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@additem", this.textBox4.Text);
                cmd.Parameters.AddWithValue("@price", this.textBox3.Text);
                cmd.Parameters.AddWithValue("@sell_price", this.textBox2.Text);
                cmd.Parameters.AddWithValue("@photo", getphoto());
                cmd.Parameters.AddWithValue("@category", this.textBox1.Text);

                //excute command to insert data
                int retur = cmd.ExecuteNonQuery();

                MessageBox.Show("Item Added" + retur, "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
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
    }
}
