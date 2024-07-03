using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practicle_cw
{
    internal class Main
    {

       
        /*public static bool IsValidUser (string user)
        {
            bool isvalid = false;

            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated Security = True";//create connection string
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string login = "SELECT * FROM users WHERE user_name = '" + user + "'";//methana table eke nama
            SqlCommand cmd = new SqlCommand(login, conn);

            //Access Data using Data Adapter Methord
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                isvalid = true;
                USER = dt.Rows[0]["user_name"].ToString();
            }
            return isvalid;
        }
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }*/

        public static int SQL(string qry,Hashtable ht)
        {
            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated Security = True";//create connection string
            SqlConnection conn = new SqlConnection(connectionstring);
            int res = 0;
            try
            {
                
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if(conn.State == ConnectionState.Closed) { conn.Open(); }
                {
                    res = cmd.ExecuteNonQuery();
                }
                if (conn.State == ConnectionState.Open) { conn.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
            return res;
        }

        public static void LoadData(string qry,DataGridView gv,ListBox lb)
        {
            string connectionstring = "Data Source = SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog = Practiclecw;Integrated Security = True";//create connection string
            SqlConnection conn = new SqlConnection(connectionstring);
            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                da.Fill(dt);
                for(int i = 0; i < lb.Items.Count; i++)
                {
                    string colonal = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colonal].DataPropertyName = dt.Columns[i].ToString();
                }
                gv.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
        }
    }
}
