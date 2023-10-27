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
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);
        }
        public bool checkemail(Registration registration)
        {
            connections();
            SqlCommand command = new SqlCommand("Emailexists", connection);
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
            return true;
        }
    }
}