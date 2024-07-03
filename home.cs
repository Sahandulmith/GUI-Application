using System;
using System.Collections;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practicle_cw
{
    public partial class home : Form
    {
        double total;
        double cost;
        DataSet dataset;
        private List<string> shoppingCartItems = new List<string>();
        public home()
        {
            InitializeComponent();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Create connection with SQL
            string connectionString = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Define command with parameterized query
                string sql = "SELECT * FROM item";
                SqlCommand cmd = new SqlCommand(sql, conn);

                //DATA REARDE METHORD
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();

                this.dataGridView1.DataSource = shoppingCartItems;
            }
        }

        private void item5_Load(object sender, EventArgs e)
        {

        }
        
        private void home_Load(object sender, EventArgs e)
        {
            if (Singin.user_type == "A")
            {
                button4.Visible = false;
                btnshippingmethord.Visible = false;
                button1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox4.Visible = false;
            }
            else
            {
                button4.Visible = true;
                btnshippingmethord.Visible = true;
                button1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox4.Visible = true;
            }

            
        }

        private void item1_Load(object sender, EventArgs e)
        {

        }


        private void btncurrentorder_Click_1(object sender, EventArgs e)
        {
            // Create connection with SQL
            string connectionString = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM item", conn);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                this.dataGridView1.DataSource = dt;
            }

            double total = 0.0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                double value;
                if (row.Cells[3].Value != null && double.TryParse(row.Cells[3].Value.ToString(), out value))
                {
                    total += value;
                }
            }

            textBox1.Text = total.ToString();

        }

        private void btnoderclear_Click(object sender, EventArgs e)
        {
            
            new clearapproveadmin().Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new cart().Show();

        }
        private void btnalcohol_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int m = 0; m < 100; m++)
                            {
                                flowLayoutPanel1.Controls.Add(new alcohol(m));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnfood_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem";

                   List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        
                       command.CommandType = CommandType.Text;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int j = 0; j < 100; j++)
                            {
                                flowLayoutPanel1.Controls.Add(new food(j));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnallitem_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem ";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int i = 0; i < 10000; i++)
                            {
                                flowLayoutPanel1.Controls.Add(new additem(i));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnsoftdrink_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem ";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int l = 0; l < 100; l++)
                            {
                                flowLayoutPanel1.Controls.Add(new softdrink(l));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnbillpayment_Click(object sender, EventArgs e)
        {
            new customer().Show();
        }

        private void soft11_Load(object sender, EventArgs e)
        {

        }
        private void flowLayoutPanel3soft_Paint(object sender, PaintEventArgs e)
        {
            //flowLayoutPanel3soft.Controls.Add(new additem());
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new Singin().Show();
        }

        private void btnhomesingup_Click(object sender, EventArgs e)
        {
            
        }

        private void btnshippingmethord_Click(object sender, EventArgs e)
        {
            
            new sampleadd2().Show();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

           
            ////create a connection with SQL server
            //string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated Security = True";//create connection string
            //SqlConnection conn = new SqlConnection(connectionstring);
            //conn.Open();

            ////Difine a Command
            //string sql = "SELECT * FROM additem WHERE pname =@additem ";//methana table eke nama
            //SqlCommand cmd = new SqlCommand(sql, conn);
            //cmd.Parameters.AddWithValue("@additem", this.txtsearch.Text);

            ////Access Data using DataReader Methord
            //SqlDataReader reader = cmd.ExecuteReader();
            //if (reader.Read() == true)
            //{
            //    //Bind Data with Control
            //     reader.GetValue(0).ToString();
            //   reader.GetValue(1).ToString();
            //     reader.GetValue(2).ToString();

            //}
            //else
            //{
            //    MessageBox.Show("No Records Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ////Disconnect from server
            //conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new bill().Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            row.Cells[0].Value.ToString();
            row.Cells[1].Value.ToString();
            row.Cells[2].Value.ToString();
        }

        private void btnoderclear_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void btnoderclear_MouseLeave(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Income().Show();
        }

        private void btnbakery_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem ";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int n = 0; n < 100; n++)
                            {
                                flowLayoutPanel1.Controls.Add(new bakery(n));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnelectric_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem ";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int p = 0; p < 100; p++)
                            {
                                flowLayoutPanel1.Controls.Add(new electric(p));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnpharmacy_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem ";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int q = 0; q < 100; q++)
                            {
                                flowLayoutPanel1.Controls.Add(new pharmacy(q));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void btnschool_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM additem ";
                    List<string> result = new List<string>();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            FlowLayoutPanel panel = new FlowLayoutPanel();
                            Form form = new Form();
                            panel.Dock = DockStyle.Fill;
                            form.Controls.Add(panel);

                            for (int o = 0; o < 100; o++)
                            {
                                flowLayoutPanel1.Controls.Add(new school(o));
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new message().Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
