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
using Batch_32_Final_Project.Models;
namespace Batch_32_Final_Project.Repository
{
    public class RegistrationRepository
    {
        private SqlConnection connection;
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
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);
        }
        public bool Insertregistration(Registration registration)
        {
            connections();
            var encryptedpassword = Encrypt(registration.Password);
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
            command.Parameters.AddWithValue("@Password",encryptedpassword);
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
