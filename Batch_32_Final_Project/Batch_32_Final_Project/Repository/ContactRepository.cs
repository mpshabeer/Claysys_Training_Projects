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
        //To Handle connection related activities    
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);

        }

        public bool InsertContact(Contactus contactus)
        {

            connections();


            SqlCommand command = new SqlCommand("Inserttocontact", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", contactus.Name);
            command.Parameters.AddWithValue("@Phone", contactus.Phone);
            command.Parameters.AddWithValue("@Email", contactus.Email);
            command.Parameters.AddWithValue("@Command", contactus.Command);
        

            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }



        }

    }
}