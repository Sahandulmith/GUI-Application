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
    public partial class singup : Form
    {
        DataSet ds;
        public singup()
        {
            InitializeComponent();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncrateaccount_Click(object sender, EventArgs e)
        {

            if (this.textBox1.Text == this.textBox2.Text)
            {
                try
                {


                    string password = this.textBox1.Text;

                    // Hash the password using SHA256
                    string hashedPassword = HashPassword(password);

                    //create connection with sql
                    string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated security = True";

                    SqlConnection conn = new SqlConnection(connectionstring);

                    conn.Open();

                    //Difine command
                    string sql = "INSERT INTO users VALUES(@first_name, @last_name, @Address, @nic, @mobile, @email,  @user_name, @password ,@user_type)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@first_name", this.txtfirstname.Text);
                    cmd.Parameters.AddWithValue("@last_name", this.txtlastname.Text);
                    cmd.Parameters.AddWithValue("@Address", this.txtaddress.Text);
                    cmd.Parameters.AddWithValue("@nic", this.txtnic.Text);
                    cmd.Parameters.AddWithValue("@mobile", this.txtmobile.Text);
                    cmd.Parameters.AddWithValue("@email", this.txtemail.Text);
                    cmd.Parameters.AddWithValue("@user_name", this.textBox3.Text);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@user_type", "User");

                    //excute command to insert data
                    int retur = cmd.ExecuteNonQuery();

                    MessageBox.Show("Your Account Created" + retur, "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    conn.Close();

                    new Singin().Show();
                    this.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                
            }
            else
            {
                MessageBox.Show("Password doesn't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
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

        private void checkpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkpassword.Checked)
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;

            }
            else
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void singup_Load(object sender, EventArgs e)
        {

        }

        }
    }

