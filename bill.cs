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
using System.Speech.Synthesis;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Practicle_cw
{
    public partial class bill : Form
    {
        double total = 0.0;
        double balance = 0.0;
        public bill()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM item", conn);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                this.dataGridView1.DataSource = dt;


                conn.Close();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    double value;
                    if (row.Cells[3].Value != null && double.TryParse(row.Cells[3].Value.ToString(), out value))
                    {
                        total += value;
                    }
                }

                txtbilltotal.Text = total.ToString();
                this.button1.Enabled = false;
                

                if (card.Checked == true)
                {
                    this.button2.Hide();
                    this.label13.Hide();
                    this.label11.Hide();
                    this.textBox3.Hide();
                    this.txtchash.Hide();
                    this.label6.Hide();
                    this.textBox1.Hide();
                    this.label8.Hide();
                    this.label7.Hide();
                    this.label9.Hide();
                    this.label3.Show();
                    this.label4.Show();
                    this.txtbilltotal.Show();
                    this.label15.Hide();
                    this.label14.Hide();
                    this.textBox4.Hide();
                }
                if(cash.Checked == true)
                {
                    this.txtchash.Show();
                    this.label6.Show();
                    this.textBox1.Show();
                    this.label8.Show();
                    this.label7.Show();
                    this.label9.Show();
                    this.label3.Show();
                    this.label4.Show();
                    this.txtbilltotal.Show();
                    this.label13.Show();
                    this.label11.Show();
                    this.textBox3.Show();
                    this.label15.Show();
                    this.label14.Show();
                    this.textBox4.Show();
                }

            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            SqlConnection con = new SqlConnection(connectionstring);

            con.Open();
            string Sql = "UPDATE customer SET purches = purches + @purches WHERE mobile = '" + textBox2.Text + "'";
            SqlCommand cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@purches", this.txtbilltotal.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void bill_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("First Select payment Methord....", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.txtchash.Hide();
            this.label6.Hide();
            this.textBox1.Hide();
            this.label8.Hide();
            this.label7.Hide();
            this.label9.Hide();
            this.label13.Hide();
            this.label11.Hide();
            this.textBox3.Hide();
            this.label3.Hide();
            this.label4.Hide();
            this.txtbilltotal.Hide();
            this.label15.Hide();
            this.label14.Hide();
            this.textBox4.Hide();
            this.btnsee.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double cas = Convert.ToDouble(this.txtchash.Text);
            double save = Convert.ToDouble(this.textBox3.Text);
            double get = Convert.ToDouble(this.textBox4.Text);

            balance = cas - save + get - total;

            this.textBox1.Text = balance.ToString();
            button2.Enabled = false;

            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                string login = "SELECT * FROM customer WHERE mobile = '" + textBox2.Text + "'";

                SqlCommand cmd = new SqlCommand(login, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

                    SqlConnection con = new SqlConnection(connectionstring);

                    con.Open();
                    string sql = "UPDATE customer SET saved = saved + @saved WHERE mobile = '" + textBox2.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(sql, con);
                    
                    cmd1.Parameters.AddWithValue("@saved", this.textBox3.Text);

                    // Execute the command
                    cmd1.ExecuteNonQuery();
                }

                else
                {
                    MessageBox.Show("Invalid Mobile Number", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                    textBox2.Focus();
                }
                conn.Close();
                new billcustomersee().Close();
            }

            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";
            SqlConnection conn = new SqlConnection(connectionString);
            
                conn.Open();

                string updateSql = "UPDATE customer SET saved = saved - @save WHERE mobile = @mobile";
                SqlCommand updateCmd = new SqlCommand(updateSql, conn);

            updateCmd.Parameters.AddWithValue("@mobile", textBox2.Text);
            updateCmd.Parameters.AddWithValue("@save", textBox4.Text);
                    
            updateCmd.ExecuteNonQuery();

                string deleteSql = "DELETE FROM item";
                using (SqlCommand deleteCmd = new SqlCommand(deleteSql, conn))
                {
                    deleteCmd.ExecuteNonQuery();
                }

            billcustomersee b = new billcustomersee();

            string message = "Thank You.... Come Again...";
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Volume = 100;
            synth.Rate = 2;
            synth.Speak(message);

            SerialPort sp = new SerialPort();
            sp.PortName = "COM8";
            sp.Open();
            sp.WriteLine("AT" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CMGS=\"" + textBox2.Text + "\"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("Thank You!!!!! Come Again." +
                "\nyou bill amount Rs."+ txtbilltotal.Text+ "" +
                "\nYou Saving Rs. "+ textBox3 .Text+ "\n\n*SMS STOP ONLINE SHOPPING CENTER to "+94720840910+"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.Write(new byte[] { 26 }, 0, 1);
            Thread.Sleep(100);

            var response = sp.ReadExisting();
            if (response.Contains("ERROR"))
            {
                MessageBox.Show("Sending faild");
            }
            else
            {
                //MessageBox.Show("Message sent");
            }
            sp.Close();


            this.Close();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void card_CheckedChanged(object sender, EventArgs e)
        {
            this.button2.Hide();
            this.label13.Hide();
            this.label11.Hide();
            this.textBox3.Hide();
            this.txtchash.Hide();
            this.label6.Hide();
            this.textBox1.Hide();
            this.label8.Hide();
            this.label7.Hide();
            this.label9.Hide();
            this.label3.Show();
            this.label4.Show();
            this.txtbilltotal.Show();
            this.label15.Hide();
            this.label14.Hide();
            this.textBox4.Hide();
        }

        private void cash_CheckedChanged(object sender, EventArgs e)
        {
            this.btnsee.Show();
            this.txtchash.Show();
            this.label6.Show();
            this.textBox1.Show();
            this.label8.Show();
            this.label7.Show();
            this.label9.Show();
            this.label3.Show();
            this.label4.Show();
            this.txtbilltotal.Show();
            this.label13.Show();
            this.label11.Show();
            this.textBox3.Show();
            this.label15.Show();
            this.label14.Show();
            this.textBox4.Show();
        }

        private void btnsee_Click(object sender, EventArgs e)
        {
            
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                string sql = "SELECT * FROM customer WHERE mobile =@mobile ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                billcustomersee b = new billcustomersee();
                cmd.Parameters.AddWithValue("@mobile", textBox2.Text);


                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    
                    //Bind Data with Control
                    b.txtname.Text = reader.GetValue(0).ToString();
                    b.txtnic.Text = reader.GetValue(1).ToString();
                    b.txtmobile.Text = reader.GetValue(2).ToString();
                    b.txtpuchesprice.Text = reader.GetValue(3).ToString();
                    b.textBox1.Text = reader.GetValue(4).ToString();

                    if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        byte[] imgByte = (byte[])reader["picture"];
                        using (MemoryStream ms = new MemoryStream(imgByte))
                        {
                            b.pictureBox3.Image = Image.FromStream(ms);
                        }
                    }
                    b.Show();
                    this.btnsee.Enabled = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
