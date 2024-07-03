using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Practicle_cw
{
    public partial class Forgetpassword : Form
    {
        
        public Forgetpassword()
        {
            InitializeComponent();
        }

        private void Forgetpassword_Load(object sender, EventArgs e)
        {
            this.panel1.Hide();
            this.panel2.Hide();
            this.panel4.Hide();
            this.panel5.Hide();
            this.label4.Hide();
            this.label5.Hide();
            this.label1.Hide();
            this.label2.Hide();
            this.txtconf.Hide();
            this.txtnew.Hide();
            this.txtemail.Hide();
            this.txtcode.Hide();
            this.btnsend.Hide();
            this.btnverify.Hide();
            this.btnset.Hide();
            this.checkBox.Hide();
            this.linkLabel1.Hide();

        }

        private void btndone_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                string login = "SELECT * FROM users WHERE user_name = '" + txtname.Text + "'";

                SqlCommand cmd = new SqlCommand(login, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    this.panel1.Show();
                    this.panel2.Show();
                    this.label1.Show();
                    this.label2.Show();
                    this.txtemail.Show();
                    this.txtcode.Show();
                    this.btnsend.Show();
                    this.btnverify.Show();
                    this.linkLabel1.Show();
                }
                else
                {
                    MessageBox.Show("Invalid User name", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtname.Clear();
                    txtname.Focus();
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        string randomCode;
        public static string to;
        private void btnsend_Click(object sender, EventArgs e)
        {
            string from, pass, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = txtemail.Text;
            from = "onlineshoppingcenter365@gmail.com";
            pass = "xagseewvrummsljz";
            messageBody = "Hi "+ txtname.Text+ ",\n"+ "\nWe received a request to access your account " + txtemail.Text + ". Through your email address. \n\nYour verification code is :" + "\n" + randomCode + "\n\nThank You!";
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Password Reseting Code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                MessageBox.Show("Code sent successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending code: " + ex.Message);
            }
        }

        private void btnverify_Click(object sender, EventArgs e)
        {
            if (randomCode == (txtcode.Text).ToString())
            {
                to = txtcode.Text;
                this.txtnew.Show();
                this.txtconf.Show();
                this.panel4.Show();
                this.panel5.Show();
                this.label4.Show();
                this.label5.Show();
                this.btnset.Show();
                this.checkBox.Show();
            }
            else
            {
                MessageBox.Show("Wrong Code");
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnset_Click(object sender, EventArgs e)
        {
            if (this.txtnew.Text == this.txtconf.Text)
            {
                string password = this.txtnew.Text;
                string hashedPassword = HashPassword(password);

                DbConnection dbConnection = new DbConnection();
                SqlConnection conn = dbConnection.EstablishConnection();
                if (conn != null)
                {

                    //Difine a Command
                    string sql = "UPDATE users SET password =@password WHERE user_name = '" + txtname.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    //Excute Command to insert Data
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Your Update successfully");
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Password doesn't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtconf.Clear();
                txtconf.Focus();
            }
            this.Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                txtnew.UseSystemPasswordChar = true;
                txtconf.UseSystemPasswordChar = true;
            }
            else
            {
                txtnew.UseSystemPasswordChar = false;
                txtconf.UseSystemPasswordChar = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string from, pass, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = txtemail.Text;
            from = "onlineshoppingcenter365@gmail.com";
            pass = "xagseewvrummsljz";
            messageBody = "Your reset code is " + randomCode;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Password Reseting Code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                MessageBox.Show("Code sent successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending code: " + ex.Message);
            }
        }
    }
}
