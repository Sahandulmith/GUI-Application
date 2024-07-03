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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practicle_cw
{
    public partial class cart : Form
    {
        public cart()
        {
            InitializeComponent();
        }

        private void btncurrentorder_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM item", conn);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                this.dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
