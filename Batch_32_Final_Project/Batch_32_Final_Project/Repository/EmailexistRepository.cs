using Batch_32_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Batch_32_Final_Project.Repository
{
    public class EmailexistRepository

    {
       private SqlConnection connection;
       string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();

        /// <summary>
        /// Validates user credentials by checking the provided email and password against the database.
        /// </summary>
        /// <param name="login">The Login object containing user login information.</param>
        /// <param name="userType">Output parameter that receives the user type if validation is successful; otherwise, null.</param>
        /// <param name="rid">Output parameter that receives the user's registration ID if validation is successful; otherwise, null.</param>
        /// <param name="username">Output parameter that receives the user's first name if validation is successful; otherwise, null.</param>
        /// <returns>True if the provided credentials are valid; otherwise, false.</returns>
        public bool checkemail(Registration registration)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_Emailexist", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", registration.Email);
                        connection.Open();
                        SqlDataReader sdr = command.ExecuteReader();
                        if (sdr.Read())
                        {
                            string Emails = sdr["Email"].ToString();
                            if (Emails == registration.Email)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                return true;
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