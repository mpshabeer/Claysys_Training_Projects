using Batch_32_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Repository
{
    public class ContactRepository
    {
        private SqlConnection connection;
        string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
        /// <summary>
        /// Inserts a new contact request into the database.
        /// </summary>
        /// <param name="contactus">The Contactus object containing contact details.</param>
        /// <returns>True if the insertion is successful, otherwise false.</returns>
        public bool InsertContact(Contactus contactus)
        {
            try
            {
                int i;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPI_Contactus", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", contactus.Name);
                        command.Parameters.AddWithValue("@Phone", contactus.Phone);
                        command.Parameters.AddWithValue("@Email", contactus.Email);
                        command.Parameters.AddWithValue("@Command", contactus.Command);
                        connection.Open();
                        i = command.ExecuteNonQuery();
                    }
                }

                if (i >= 1)
                {

                    return true;

                }
                else
                {

                    return false;
                }
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

    }
}