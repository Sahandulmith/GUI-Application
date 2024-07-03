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
    public partial class Income : Form
    {
        public Income()
        {
            InitializeComponent();
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                string sql = "SELECT * FROM income WHERE date = @date ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", this.dateTimePicker1.Text);


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                //set the data source of the report
                CrystalReport1 crystalReport1 = new CrystalReport1();

                crystalReport1.Load("D:\\NIBM\\2ND SEM\\GUI\\Practicle cw\\Practicle cw\\CrystalReport1.rpt");
                crystalReport1.SetDataSource(ds.Tables[0]);


                //set the report sorce of the created"crystalreportviewer"
                this.crystalReportViewer1.ReportSource = crystalReport1;

                conn.Close();
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.Refresh();
            crystalReportViewer1.ReportSource = "D:\\NIBM\\2ND SEM\\GUI\\Practicle cw\\Practicle cw\\CrystalReport1.rpt";
        }

        private void Income_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = "D:\\NIBM\\2ND SEM\\GUI\\Practicle cw\\Practicle cw\\CrystalReport1.rpt";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                string deleteQuery = "DELETE FROM income ";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);


                // Execute the command
                int rowsAffected = cmd.ExecuteNonQuery();

                DialogResult ms = MessageBox.Show("Are you Sure Remove History", "Remove History", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ms == DialogResult.OK)
                {
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully deleted.");
                        new Income().Close();
                    }

                }
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
