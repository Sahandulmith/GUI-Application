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

namespace Practicle_cw
{
    public partial class removeaccount : Form
    {
        public removeaccount()
        {
            InitializeComponent();
        }

        private void btnsingin_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                // Prepare the SQL command to delete all records from the item table
                string deleteQuery = "DELETE FROM users WHERE user_name = '" + txtsinginemail.Text + "' AND nic = '" + txtnic.Text + "'";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);

                // Execute the command
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Your account successfully deleted.");
                }
                else
                {
                    MessageBox.Show("Wrong user name or NIC.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}