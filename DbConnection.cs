using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practicle_cw
{
    internal class DbConnection
    {
        // Connection string should be a class member
        private string connectionString;

        // Constructor to initialize connection string
        public DbConnection()
        {
            this.connectionString = "Data Source=SAHAN-DULMITH\\SQLEXPRESS;Initial Catalog=Practiclecw;Integrated Security=True";
        }

        // Method to establish connection
        public SqlConnection EstablishConnection()
        {
            try
            {
                // Create a new SqlConnection object using the connection string
                SqlConnection connection = new SqlConnection(connectionString);

                // Open the connection
                connection.Open();

                // Return the connection object
                return connection;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during connection
                Console.WriteLine("Error establishing database connection: " + ex.Message);
                return null;
            }
        }

        

    }
    
}
