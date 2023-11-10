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
        private SqlConnection connection;
        string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
        Decryptpassword decryptpassword = new Decryptpassword();
        /// <summary>
        /// Encrypts a string using the AES algorithm.
        /// </summary>
        /// <param name="clearText">The string to encrypt.</param>
        /// <returns>The encrypted string.</returns>
        public string Encrypt(string clearText)
        {
            try
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
             finally
            {
             
            }
        }
        /// <summary>
        /// Inserts a new registration record into the database.
        /// </summary>
        /// <param name="registration">The Registration object containing user registration information.</param>
        /// <returns>True if the insertion was successful; otherwise, false.</returns>
        public bool Insertregistration(Registration registration)
        {
            try
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
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// Retrieves user details from the database based on the provided registration ID.
        /// </summary>
        /// <param name="rid">The registration ID of the user.</param>
        /// <returns>A Userdetails object containing user information.</returns>
        public Userdetails GetDetailsofUser(int rid)
        {
            try
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
                                rid = rid,
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
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool Updateuserdetails(Userdetails userdetails)
        {
            int i = 0;
            try
            {
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
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// Changes the password for a specific user identified by the provided registration ID.
        /// </summary>
        /// <param name="encryptedpassword">The new encrypted password for the user.</param>
        /// <param name="rid">The registration ID of the user.</param>
        /// <returns>True if the password change was successful; otherwise, false.</returns>
        public bool ChangeUserPassword( string encryptedpassword,int rid)
        {
            try
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
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// Inserts a new HR record into the database.
        /// </summary>
        /// <param name="registration">The Registration object containing HR information to be inserted.</param>
        /// <returns>True if the insertion was successful; otherwise, false.</returns>
        public bool InserttoHR(Registration registration)
        {
            try
            {
                int i;
                var encryptedpassword = Encrypt(registration.Password);
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPI_HR", connection))
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
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// Checks if the provided old password matches the password stored in the database for a specific user.
        /// </summary>
        /// <param name="changepassword">The Changepassword object containing old password information.</param>
        /// <param name="rid">The user's registration ID.</param>
        /// <returns>True if the provided old password matches the password stored in the database; otherwise, false.</returns>
        public bool ChangePassword(Changepassword changepassword,int rid)
        {
            try
            {
                Decryptpassword decryptpassword = new Decryptpassword();
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_Oldpassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@rid", rid);
                        connection.Open();
                        SqlDataReader sdr = command.ExecuteReader();
                        if (sdr.Read())
                        {
                            string encryptedpasswords = sdr["Password"].ToString();
                            string decryptedpassword = decryptpassword.Decrypt(encryptedpasswords);
                            if (changepassword.OldPassword == decryptedpassword)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
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
        /// <summary>
        /// Validates user credentials by checking the provided email and password against the database.
        /// </summary>
        /// <param name="login">The Login object containing user login information.</param>
        /// <param name="userType">Output parameter that receives the user type if validation is successful; otherwise, null.</param>
        /// <param name="rid">Output parameter that receives the user's registration ID if validation is successful; otherwise, null.</param>
        /// <param name="username">Output parameter that receives the user's first name if validation is successful; otherwise, null.</param>
        /// <returns>True if the provided credentials are valid; otherwise, false.</returns>
        public bool ValidateUser(Login login, out string userType, out string rid,out string username)
        {
            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    SqlCommand command = new SqlCommand("SP_Login", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", login.Email);
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.Read())
                    {
                        rid = sdr["rid"].ToString();
                        string encryptedpasswords = sdr["Password"].ToString();
                        string decryptedpassword = decryptpassword.Decrypt(encryptedpasswords);
                        if (login.Password == decryptedpassword)
                        {
                            userType = sdr["Type"].ToString();
                            username = sdr["Firstname"].ToString(); 
                            return true;
                        }
                    }
                }
            }
            catch (Exception )
            {
                userType = null;
                rid = null;
                username = null;
                return false;
            }

            userType = null;
            rid = null;
            username = null;
            return false;
    }
}
}
