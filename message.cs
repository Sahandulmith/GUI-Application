using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practicle_cw
{
    public partial class message : Form
    {
        public message()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand adapter = new SqlCommand("SELECT * FROM customer", conn);
            SqlDataReader reader = adapter.ExecuteReader();



            while (reader.Read())
            {
                string phoneNumber = reader["mobile"].ToString();

                using (SerialPort sp = new SerialPort())
                {
                    if (comboBox1.Items.Count > 0)
                    {
                        sp.PortName = "COM8";
                        sp.Open();
                        sp.WriteLine("AT" + Environment.NewLine);
                        Thread.Sleep(100);
                        sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
                        Thread.Sleep(100);
                        sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
                        Thread.Sleep(100);
                        sp.WriteLine("AT+CMGS=\"" + phoneNumber + "\"" + Environment.NewLine);
                        Thread.Sleep(100);
                        sp.WriteLine("**ONLINE SHOPPING CENTER**\n"+ textBox3.Text + Environment.NewLine);
                        Thread.Sleep(100);
                        sp.Write(new byte[] { 26 }, 0, 1);
                        Thread.Sleep(100);

                        string response = sp.ReadExisting();
                        if (response.Contains("ERROR"))
                        {
                            MessageBox.Show("Sending failed to " + phoneNumber);
                        }
                        else
                        {
                            //MessageBox.Show("Message sent to " + phoneNumber);
                        }
                    }
                }
            }

            reader.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void message_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

            SqlConnection conn = new SqlConnection(connectionstring);

            conn.Open();

            string login = "SELECT mobile FROM customer ";

            SqlCommand cmd = new SqlCommand(login, conn);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                this.comboBox1.Items.Add(reader.GetValue(0));
            }

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand adapter = new SqlCommand("SELECT * FROM customer", conn);
            SqlDataReader reader = adapter.ExecuteReader();
            reader.Read();

            SerialPort sp = new SerialPort();
            sp.PortName = "COM8";
            sp.Open();
            sp.WriteLine("AT" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
            Thread.Sleep(100);

            var phoneNumber = reader["mobile"].ToString();

            sp.WriteLine("AT+CMGS=\"" + comboBox1.Text + "\"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("**ONLINE SHOPPING CENTER**\n"+textBox3.Text + Environment.NewLine);
            Thread.Sleep(100);
            sp.Write(new byte[] { 26 }, 0, 1);
            Thread.Sleep(100);

            var response = sp.ReadExisting();
            if (response.Contains("ERROR"))
            {
                MessageBox.Show("Sending faild "+ comboBox1.Text);
            }
            else
            {
                MessageBox.Show("Message sent "+ comboBox1.Text);
            }
            sp.Close();
        }
    }
}
