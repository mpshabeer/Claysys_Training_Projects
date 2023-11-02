using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Batch_32_Final_Project.Models;
namespace Batch_32_Final_Project.Repository
{
    public class RegistrationRepository
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
        public string Encrypt(string clearText)
        {
            string ciphertext;
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    ciphertext = Convert.ToBase64String(ms.ToArray());
                }
            }
            return ciphertext;
        }
      
           
           
        
        public bool Insertregistration(Registration registration)
        {
            int i;
            var encryptedpassword = Encrypt(registration.Password);
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand("InsertToRegistration", connection))
                {

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
                    command.Parameters.AddWithValue("@Password", encryptedpassword);
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

       


        public Userdetails GetDetailsofUser(int rid)
        {
            Userdetails userdetails = null;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
               
                SqlCommand commandd = new SqlCommand("SPD_Registration", connection);
                commandd.CommandType = CommandType.StoredProcedure;
                commandd.Parameters.AddWithValue("@rid", rid);

                connection.Open();
                SqlDataReader dataReader = commandd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                         userdetails = new Userdetails
                        {
                            rid=rid,
                            Firstname = dataReader["Firstname"].ToString(),
                            Lastname = dataReader["Lastname"].ToString(),
                            Dateofbirth = dataReader["Dateofbirth"].ToString(),
                            Gender = dataReader["Gender"].ToString(),
                            Phone = dataReader["Phone"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Address = dataReader["Address"].ToString(),
                            State = dataReader["State"].ToString(),
                            City = dataReader["City"].ToString(),
                            Pincode = dataReader["Pincode"].ToString(),
                        };
                    }
                }
            }
            return userdetails; 
        }
        public bool Updateuserdetails(Userdetails userdetails)
        {

            int i = 0;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SPU_Registration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@rid", userdetails.rid);
                    command.Parameters.AddWithValue("@Firstname", userdetails.Firstname);
                    command.Parameters.AddWithValue("@Lastname", userdetails.Lastname);
                    command.Parameters.AddWithValue("@Dateofbirth", userdetails.Dateofbirth);
                    command.Parameters.AddWithValue("@Gender", userdetails.Gender);
                    command.Parameters.AddWithValue("@Email", userdetails.Email);
                    command.Parameters.AddWithValue("@Phone", userdetails.Phone);
                    command.Parameters.AddWithValue("@Address", userdetails.Address);
                    command.Parameters.AddWithValue("@State", userdetails.State);
                    command.Parameters.AddWithValue("@City", userdetails.City);
                    command.Parameters.AddWithValue("@Pincode", userdetails.Pincode);
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
        public bool ChangeUserPassword( string encryptedpassword,int rid)
        {

            int i = 0;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SPU_UserPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@rid", rid);
                    command.Parameters.AddWithValue("@Newpassword", encryptedpassword);
                 
      
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
    }
}
