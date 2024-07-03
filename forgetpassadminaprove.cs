using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practicle_cw
{
    public partial class forgetpassadminaprove : Form
    {
        public forgetpassadminaprove()
        {
            InitializeComponent();
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
        private void button1_Click(object sender, EventArgs e)
        {
            string password = this.txtsinginpassword.Text;
            string hashedPassword = HashPassword(password);

            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                string login = "SELECT * FROM users WHERE user_name = '" + txtsinginemail.Text + "' AND password = '" + hashedPassword + "'";

                SqlCommand cmd = new SqlCommand(login, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader[8].ToString() == "Admin")
                    {
                        new Forgetpassword().Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Only Admin Can", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else
                {
                    MessageBox.Show("Invalid User name or Password", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtsinginemail.Clear();
                    txtsinginpassword.Clear();
                    txtsinginemail.Focus();
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void checkBoxsinginshowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxsinginshowpass.Checked)
            {
                txtsinginpassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtsinginpassword.UseSystemPasswordChar = false;
            }
        }
    }
}
