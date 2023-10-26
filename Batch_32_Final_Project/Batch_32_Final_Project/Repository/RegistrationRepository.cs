using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Batch_32_Final_Project.Models;
namespace Batch_32_Final_Project.Repository
{
    public class RegistrationRepository
    {

        private SqlConnection connection;
        //To Handle connection related activities    
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);

        }

        public bool Insertregistration(Registration registration)
        {

            connections();
            

            SqlCommand command = new SqlCommand("InsertToRegistration", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Firstnane", registration.Firstname);
            command.Parameters.AddWithValue("@Lastname", registration.Lastname);
            command.Parameters.AddWithValue("@Dateofbirth", registration.Dateofbirth);
            command.Parameters.AddWithValue("@Gender", registration.Gender);
            command.Parameters.AddWithValue("@Phone", registration.Phone);
            command.Parameters.AddWithValue("@Email", registration.Email);
            command.Parameters.AddWithValue("@Address", registration.Address);
            command.Parameters.AddWithValue("@State", registration.State);
            command.Parameters.AddWithValue("@City", registration.City);
            command.Parameters.AddWithValue("@Pincode", registration.Pincode);
            command.Parameters.AddWithValue("@Password", registration.Password);

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