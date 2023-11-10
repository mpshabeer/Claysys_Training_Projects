using Batch_32_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Batch_32_Final_Project.Repository
{
   
    public class VacancyRepository
    {
        private SqlConnection connection;
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);

        }
        string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
        /// <summary>
        /// Inserts a vacancy into the database.
        /// </summary>
        /// <param name="vacancy">The Vacancy object to be inserted.</param>
        /// <param name="mail">The email address of the user inserting the vacancy.</param>
        /// <returns>Returns true if the vacancy was successfully inserted; otherwise, false.</returns>
        public bool InsertVacancy(Vacancy vacancy, string mail)
        {
            try
            {
                string todaydate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                int i;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPI_Vacancy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JobTitle", vacancy.JobTitle);
                        command.Parameters.AddWithValue("@JobDescription", vacancy.JobDescription);
                        command.Parameters.AddWithValue("@Department", vacancy.Department);
                        command.Parameters.AddWithValue("@Location", vacancy.Location);
                        command.Parameters.AddWithValue("@VacancyStatus", vacancy.VacancyStatus);
                        command.Parameters.AddWithValue("@NumberOfOpenings", Convert.ToInt32(vacancy.NumberOfOpenings));
                        command.Parameters.AddWithValue("@Qualification", vacancy.Qualifications);
                        command.Parameters.AddWithValue("@ResponsibilitiesAndDuties", vacancy.ResponsibilitiesAndDuties);
                        command.Parameters.AddWithValue("@SalaryRange", vacancy.SalaryRange);
                        command.Parameters.AddWithValue("@PostedDate", todaydate);
                        command.Parameters.AddWithValue("@LastDateToApply", vacancy.LastDateToApply);
                        command.Parameters.AddWithValue("@RecruiterInCharge", vacancy.RecruiterInCharge);
                        command.Parameters.AddWithValue("@InterviewRounds", Convert.ToInt32(vacancy.InterviewRounds));
                        command.Parameters.AddWithValue("@SelectionProcess", vacancy.SelectionProcess);
                        command.Parameters.AddWithValue("@Createdby", mail);
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

        /* public bool InsertVacancy(Vacancy vacancy,string mail)
         {

             connections(); 
             string todaydate = DateTime.UtcNow.ToString("yyyy-MM-dd");

             SqlCommand command = new SqlCommand("Inserttovacancy", connection);
             command.CommandType = CommandType.StoredProcedure;
             command.Parameters.AddWithValue("@JobTitle", vacancy.JobTitle);
             command.Parameters.AddWithValue("@JobDescription", vacancy.JobDescription);
             command.Parameters.AddWithValue("@Department", vacancy.Department);
             command.Parameters.AddWithValue("@Location", vacancy.Location);
             command.Parameters.AddWithValue("@VacancyStatus", vacancy.VacancyStatus);
             command.Parameters.AddWithValue("@NumberOfOpenings", Convert.ToInt32(vacancy.NumberOfOpenings));
             command.Parameters.AddWithValue("@Qualification", vacancy.Qualification);
             command.Parameters.AddWithValue("@ResponsibilitiesAndDuties", vacancy.ResponsibilitiesAndDuties);
             command.Parameters.AddWithValue("@SalaryRange", vacancy.SalaryRange);
             command.Parameters.AddWithValue("@PostedDate", todaydate);
             command.Parameters.AddWithValue("@LastDateToApply", vacancy.LastDateToApply);
             command.Parameters.AddWithValue("@RecruiterInCharge", vacancy.RecruiterInCharge);
             command.Parameters.AddWithValue("@InterviewRounds", Convert.ToInt32(vacancy.InterviewRounds));
             command.Parameters.AddWithValue("@SelectionProcess", vacancy.SelectionProcess);
             command.Parameters.AddWithValue("@Createdby", mail);

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
         }*/

        /// <summary>
        /// Retrieves all open job poster for candidates.
        /// </summary>
        /// <returns>A list of Vacancy objects representing all open job posters.</returns>
        public List<Vacancy> GetallVacancytocandidate()
        {
            List<Vacancy> VacancyList = new List<Vacancy>();
            try
            {
                string todaydate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    SqlCommand command = new SqlCommand("SP_GetOpenvacancy", connection);
                    command.Parameters.AddWithValue("@Date", todaydate);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    connection.Open();
                    dataadapter.Fill(datatable);
                    foreach (DataRow datarow in datatable.Rows)
                    {
                        VacancyList.Add(new Vacancy
                        {
                            vid = Convert.ToInt32(datarow["vid"]),
                            JobTitle = Convert.ToString(datarow["JobTitle"]),
                            JobDescription = Convert.ToString(datarow["JobDescription"]),
                            Department = Convert.ToString(datarow["Department"]),
                            NumberOfOpenings = Convert.ToString(datarow["NumberOfOpenings"]),
                            PostedDate = Convert.ToString(datarow["PostedDate"]),
                            LastDateToApply = Convert.ToString(datarow["LastDateToApply"]),
                            Createdby = Convert.ToString(datarow["Createdby"])
                        });
                    }
                }
                return VacancyList;
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
        /// Retrieves all job posters.
        /// </summary>
        /// <returns>A list of Vacancy objects representing all job posters.</returns>

       
        public List<Vacancy> GetallVacancy()
        {
            List<Vacancy> VacancyList = new List<Vacancy>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_Vacancy", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                        DataTable datatable = new DataTable();

                        connection.Open();
                        dataadapter.Fill(datatable);

                        foreach (DataRow datarow in datatable.Rows)
                        {
                            VacancyList.Add(new Vacancy
                            {
                                vid = Convert.ToInt32(datarow["vid"]),
                                JobTitle = Convert.ToString(datarow["JobTitle"]),
                                JobDescription = Convert.ToString(datarow["JobDescription"]),
                                Department = Convert.ToString(datarow["Department"]),
                                NumberOfOpenings = Convert.ToString(datarow["NumberOfOpenings"]),
                                PostedDate = Convert.ToString(datarow["PostedDate"]),
                                LastDateToApply = Convert.ToString(datarow["LastDateToApply"]),
                                Createdby = Convert.ToString(datarow["Createdby"])
                            });
                        }
                    }
                }
                return VacancyList;
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
        /// Retrieves details of a specific jobposter from the database.
        /// </summary>
        /// <param name="vid">The ID of the job poster to retrieve.</param>
        /// <returns>A list of Vacancy objects containing details of the specified job poster.</returns>

        public List<Vacancy> GetVacancyDetails(int vid)
        {
            List<Vacancy> VacancyList = new List<Vacancy>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_Job", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@vid", vid);
                        SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                        DataTable datatable = new DataTable();
                        connection.Open();
                        dataadapter.Fill(datatable);
                        foreach (DataRow datarow in datatable.Rows)
                        {
                            VacancyList.Add(

                                new Vacancy
                                {
                                    vid = Convert.ToInt32(datarow["vid"]),
                                    JobTitle = Convert.ToString(datarow["JobTitle"]),
                                    JobDescription = Convert.ToString(datarow["JobDescription"]),
                                    Department = Convert.ToString(datarow["Department"]),
                                    Location = Convert.ToString(datarow["Location"]),
                                    VacancyStatus = Convert.ToString(datarow["VacancyStatus"]),
                                    NumberOfOpenings = Convert.ToString(datarow["NumberOfOpenings"]),
                                    Qualifications = Convert.ToString(datarow["Qualifications"]),
                                    ResponsibilitiesAndDuties = Convert.ToString(datarow["ResponsibilitiesAndDuties"]),
                                    SalaryRange = Convert.ToString(datarow["SalaryRange"]),
                                    PostedDate = Convert.ToString(datarow["PostedDate"]),
                                    LastDateToApply = Convert.ToString(datarow["LastDateToApply"]),
                                    RecruiterInCharge = Convert.ToString(datarow["RecruiterInCharge"]),
                                    Createdby = Convert.ToString(datarow["Createdby"]),
                                    InterviewRounds = Convert.ToString(datarow["InterviewRounds"]),
                                    SelectionProcess = Convert.ToString(datarow["SelectionProcess"]),
                                }
                                );
                        }
                    }
                }
                return VacancyList;
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
        /// Updates the details of a vacancy in the database.
        /// </summary>
        /// <param name="vacancy">The Vacancy object containing updated details.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool UpdateTOVacancy(Vacancy vacancy)
        {
            int i = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPU_Vacancy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", vacancy.vid);
                        command.Parameters.AddWithValue("@JobTitle", vacancy.JobTitle);
                        command.Parameters.AddWithValue("@JobDescription", vacancy.JobDescription);
                        command.Parameters.AddWithValue("@Department", vacancy.Department);
                        command.Parameters.AddWithValue("@Location", vacancy.Location);
                        command.Parameters.AddWithValue("@VacancyStatus", vacancy.VacancyStatus);
                        command.Parameters.AddWithValue("@NumberOfOpenings", Convert.ToInt32(vacancy.NumberOfOpenings));
                        command.Parameters.AddWithValue("@Qualification", vacancy.Qualifications);
                        command.Parameters.AddWithValue("@ResponsibilitiesAndDuties", vacancy.ResponsibilitiesAndDuties);
                        command.Parameters.AddWithValue("@SalaryRange", vacancy.SalaryRange);
                        command.Parameters.AddWithValue("@LastDateToApply", vacancy.LastDateToApply);
                        command.Parameters.AddWithValue("@RecruiterInCharge", vacancy.RecruiterInCharge);
                        command.Parameters.AddWithValue("@InterviewRounds", Convert.ToInt32(vacancy.InterviewRounds));
                        command.Parameters.AddWithValue("@SelectionProcess", vacancy.SelectionProcess);
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
        /// Deletes a vacancy from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the vacancy to be deleted.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public bool DeleteTHEvacancy(int id)
        {
            int i = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPDel_Vacancy", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@vid", id);
                        connection.Open();
                        i = command.ExecuteNonQuery();
                        connection.Close();
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
        /// Applies for a job by submitting the necessary files and information.
        /// </summary>
        /// <param name="Resume">The resume file uploaded by the applicant.</param>
        /// <param name="Photo">The photo file uploaded by the applicant.</param>
        /// <param name="Acadamiccertificate">The academic certificate file uploaded by the applicant.</param>
        /// <param name="Experiancecertificate">The experience certificate file uploaded by the applicant.</param>
        /// <param name="vid">The ID of the vacancy to apply for.</param>
        /// <param name="rid">The ID of the applicant.</param>
        /// <param name="applyforjob">Additional information related to the application.</param>
        /// <returns>True if the application was successfully submitted; otherwise, false.</returns>
        public bool Applytojob(HttpPostedFileBase Resume, HttpPostedFileBase Photo, HttpPostedFileBase Acadamiccertificate, HttpPostedFileBase Experiancecertificate, int vid,int rid,Apply applyforjob)
        {

            try
            {
                byte[] resumeSource = new byte[Resume.ContentLength];
                Resume.InputStream.Read(resumeSource, 0, Resume.ContentLength);
                string resumeBase64 = Convert.ToBase64String(resumeSource);

                byte[] imagesource = new byte[Photo.ContentLength];
                Photo.InputStream.Read(imagesource, 0, Photo.ContentLength);
                string imageBase64 = Convert.ToBase64String(imagesource);

                byte[] Acadamicsource = new byte[Acadamiccertificate.ContentLength];
                Acadamiccertificate.InputStream.Read(Acadamicsource, 0, Acadamiccertificate.ContentLength);
                string acadamicBase64 = Convert.ToBase64String(Acadamicsource);
                string expBase64 = null;
                if (Experiancecertificate != null)
                {
                    byte[] experiancesource = new byte[Experiancecertificate.ContentLength];
                    Experiancecertificate.InputStream.Read(experiancesource, 0, Experiancecertificate.ContentLength);
                    expBase64 = Convert.ToBase64String(experiancesource);
                }
                string todaydate = DateTime.UtcNow.ToString("yyyy-MM-dd");

                int i;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPI_job", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@vid", vid);
                        command.Parameters.AddWithValue("@rid", rid);
                        command.Parameters.AddWithValue("@Resume", resumeBase64);
                        command.Parameters.AddWithValue("@Photo", imageBase64);
                        command.Parameters.AddWithValue("@Acadamic", acadamicBase64);
                        command.Parameters.AddWithValue("@Expcertificate", expBase64);
                        command.Parameters.AddWithValue("@Experiance", applyforjob.Experiance);
                        command.Parameters.AddWithValue("@Qualification", applyforjob.Qualification);
                        command.Parameters.AddWithValue("@Description", applyforjob.Description);
                        command.Parameters.AddWithValue("@AppliedDate", todaydate);
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
        /// Retrieves all applied details for a specific job vacancy from the database.
        /// </summary>
        /// <param name="id">The ID of the job vacancy.</param>
        /// <returns>A list of Alldetailsofapplication objects containing applied details.</returns>
        public List<Alldetailsofapplication> Getallapplieddetails(int id)
        {
            List<Alldetailsofapplication> AppliedList = new List<Alldetailsofapplication>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_Allapplications", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@vid", id);
                        SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                        DataTable datatable = new DataTable();

                        connection.Open();
                        dataadapter.Fill(datatable);
                        connection.Close();
                        foreach (DataRow datarow in datatable.Rows)
                        {

                            AppliedList.Add(

                                new Alldetailsofapplication
                                {
                                    vid = Convert.ToInt32(datarow["vid"]),
                                    aid = Convert.ToInt32(datarow["aid"]),
                                    rid = Convert.ToInt32(datarow["rid"]),
                                    JobTitle = Convert.ToString(datarow["JobTitle"]),
                                    Firstname = Convert.ToString(datarow["Firstname"]),
                                    Lastname = Convert.ToString(datarow["Lastname"]),
                                    Email = Convert.ToString(datarow["Email"]),
                                    Resume = Convert.ToString(datarow["Resumes"]),
                                    Photo = Convert.ToString(datarow["Photo"]),
                                    Experiance = Convert.ToString(datarow["Experiance"]),
                                    Description = Convert.ToString(datarow["Description"]),
                                    Qualification = Convert.ToString(datarow["Qualification"]),
                                    Status = Convert.ToString(datarow["Status"]),
                                    Interviewdate = Convert.ToString(datarow["Interviewdate"]),

                                }
                                );
                        }
                    }
                }
                return AppliedList;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            
        }

        public bool Selection(Statusofapplication statusofapplication,int aid)
        {
            int i;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPU_Applicationsstatus", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@aid", aid);
                        command.Parameters.AddWithValue("@Status", statusofapplication.Status);
                        command.Parameters.AddWithValue("@Interviewdate", statusofapplication.Interviewdate);
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
        /// Retrieves all applications for a specific user from the database.
        /// </summary>
        /// <param name="rid">The ID of the user.</param>
        /// <returns>A list of Alldetailsofapplication objects containing the user's applications.</returns>
        public List<Alldetailsofapplication> Myapplications(int rid)
        {
            try
            {
                List<Alldetailsofapplication> AppliedList = new List<Alldetailsofapplication>();
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_Myapplications", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@rid", rid);
                        SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                        DataTable datatable = new DataTable();
                        connection.Open();
                        dataadapter.Fill(datatable);
                        connection.Close();
                        foreach (DataRow datarow in datatable.Rows)
                        {

                            AppliedList.Add(

                                new Alldetailsofapplication
                                {
                                    vid = Convert.ToInt32(datarow["vid"]),
                                    aid = Convert.ToInt32(datarow["aid"]),
                                    rid = Convert.ToInt32(datarow["rid"]),
                                    JobTitle = Convert.ToString(datarow["JobTitle"]),
                                    JobDescription = Convert.ToString(datarow["JobDescription"]),
                                    Firstname = Convert.ToString(datarow["Firstname"]),
                                    Lastname = Convert.ToString(datarow["Lastname"]),
                                    Email = Convert.ToString(datarow["Email"]),
                                    Resume = Convert.ToString(datarow["Resumes"]),
                                    Photo = Convert.ToString(datarow["Photo"]),
                                    AppliedDate = Convert.ToString(datarow["AppliedDate"]),
                                    Experiance = Convert.ToString(datarow["Experiance"]),
                                    Description = Convert.ToString(datarow["Description"]),
                                    Qualification = Convert.ToString(datarow["Qualification"]),
                                    Status = Convert.ToString(datarow["Status"]),
                                    Interviewdate = Convert.ToString(datarow["Interviewdate"]),

                                }
                                );
                        }
                    }
                }
                return AppliedList;
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
        /// Withdraws an application from the database based on the provided application ID.
        /// </summary>
        /// <param name="id">The ID of the application to withdraw.</param>
        /// <returns>True if the withdrawal was successful; otherwise, false.</returns>
        public bool Withdrawapplication(int id)
        {
            int i = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SP_WithdrawMyapplication", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@aid", id);
                        connection.Open();
                        i = command.ExecuteNonQuery();
                        connection.Close();
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
        /// Checks whether a user has applied for a particular job vacancy.
        /// </summary>
        /// <param name="vid">The ID of the job vacancy.</param>
        /// <param name="rid">The ID of the user.</param>
        /// <returns>True if the user has applied for the job vacancy; otherwise, false.</returns>
        public bool IsApplied(int vid, int rid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPD_isUserApplied", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@vid", vid);
                        command.Parameters.AddWithValue("@rid", rid);
                        connection.Open();
                        SqlDataReader sdr = command.ExecuteReader();
                        if (sdr.HasRows)
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
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// Retrieves all contact requests from the database.
        /// </summary>
        /// <returns>A list of Contactus objects containing all contact requests.</returns>
        public List<Contactus> Getallcontactrequest()
        {
            try
            {
                List<Contactus> ContactusList = new List<Contactus>();

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    SqlCommand command = new SqlCommand("SPD_Contactus", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    connection.Open();
                    dataadapter.Fill(datatable);
                    foreach (DataRow datarow in datatable.Rows)
                    {
                        ContactusList.Add(new Contactus
                        {
                            cid = Convert.ToInt32(datarow["id"]),
                            Name = Convert.ToString(datarow["Name"]),
                            Email = Convert.ToString(datarow["Email"]),
                            Phone = Convert.ToString(datarow["Phone"]),
                            Command = Convert.ToString(datarow["Command"])
                        });
                    }
                }

                return ContactusList;
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
        /// Deletes a contact request from the database based on the provided contact ID.
        /// </summary>
        /// <param name="cid">The ID of the contact request to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public bool DeleteContactus(int cid)
        {
            int i = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPDel_Contact", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cid", cid);
                        connection.Open();
                        i = command.ExecuteNonQuery();
                        connection.Close();
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
        /// Retrieves a specific application from the database based on the provided application ID.
        /// </summary>
        /// <param name="id">The ID of the application to retrieve.</param>
        /// <returns>An Application object containing the details of the application.</returns>
        public Application Getapplication(int id)
        {
            try
            {
                Application apply = null;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    SqlCommand command = new SqlCommand("SPD_Application", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@aid", id);
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            apply = new Application
                            {
                                aid = Convert.ToInt32(dataReader["aid"]),
                                Resume = Convert.ToString(dataReader["Resumes"]),
                                Photo = Convert.ToString(dataReader["Photo"]),
                                Acadamiccertificate = Convert.ToString(dataReader["Acadmiccertificate"]),
                                Experiancecertificate = Convert.ToString(dataReader["ExperienceCertificate"]),
                                Qualification = Convert.ToString(dataReader["Qualification"]),
                                Experiance = Convert.ToString(dataReader["Experiance"]),
                                Description = Convert.ToString(dataReader["Description"]),
                            };
                        }
                    }
                }
                return apply;
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
        /// Retrieves detailed information about a specific application from the database based on the provided application ID.
        /// </summary>
        /// <param name="id">The ID of the application to retrieve.</param>
        /// <returns>An Alldetailsofapplication object containing detailed information about the application.</returns>
        public Alldetailsofapplication Getapplications(int id)
        {
            try
            {
                Alldetailsofapplication apply = null;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    SqlCommand commandd = new SqlCommand("SPD_Applicant", connection);
                    commandd.CommandType = CommandType.StoredProcedure;
                    commandd.Parameters.AddWithValue("@aid", id);
                    connection.Open();
                    SqlDataReader dataReader = commandd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            apply = new Alldetailsofapplication
                            {
                                vid = Convert.ToInt32(dataReader["vid"]),
                                aid = id,
                                rid = Convert.ToInt32(dataReader["rid"]),
                                Department = Convert.ToString(dataReader["Department"]),
                                Qualifications = Convert.ToString(dataReader["Qualifications"]),
                                Responsibilities = Convert.ToString(dataReader["ResponsibilitiesAndDuties"]),
                                JobTitle = Convert.ToString(dataReader["JobTitle"]),
                                JobDescription = Convert.ToString(dataReader["JobDescription"]),
                                Firstname = Convert.ToString(dataReader["Firstname"]),
                                Lastname = Convert.ToString(dataReader["Lastname"]),
                                Email = Convert.ToString(dataReader["Email"]),
                                AppliedDate = Convert.ToString(dataReader["AppliedDate"]),
                                Phone = Convert.ToString(dataReader["Phone"]),
                                Resume = Convert.ToString(dataReader["Resumes"]),
                                Acadamiccertificate = Convert.ToString(dataReader["Acadmiccertificate"]),
                                Experiancecertificate = Convert.ToString(dataReader["ExperienceCertificate"]),
                                Photo = Convert.ToString(dataReader["Photo"]),
                                Experiance = Convert.ToString(dataReader["Experiance"]),
                                Description = Convert.ToString(dataReader["Description"]),
                                Qualification = Convert.ToString(dataReader["Qualification"]),
                                Status = Convert.ToString(dataReader["Status"]),
                                Interviewdate = Convert.ToString(dataReader["Interviewdate"]),

                            };
                        }
                    }
                }
                return apply;
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
        /// Updates an application in the database with new information.
        /// </summary>
        /// <param name="apply">The updated Application object.</param>
        /// <param name="Resume">The updated resume file.</param>
        /// <param name="Photo">The updated photo file.</param>
        /// <param name="Acadamic">The updated academic file.</param>
        /// <param name="Exp">The updated experience file.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool Updateapplications(Application apply, HttpPostedFileBase Resume, HttpPostedFileBase Photo, HttpPostedFileBase Acadamic, HttpPostedFileBase Exp)
        {
            try
            {
                int i = 0;
                byte[] resumeSource = new byte[Resume.ContentLength];
                Resume.InputStream.Read(resumeSource, 0, Resume.ContentLength);
                string resumeBase64 = Convert.ToBase64String(resumeSource);

                byte[] imagesource = new byte[Photo.ContentLength];
                Photo.InputStream.Read(imagesource, 0, Photo.ContentLength);
                string imageBase64 = Convert.ToBase64String(imagesource);

                byte[] acadamicsource = new byte[Acadamic.ContentLength];
                Acadamic.InputStream.Read(acadamicsource, 0, Acadamic.ContentLength);
                string acadamicBase64 = Convert.ToBase64String(acadamicsource);
                string expBase64 = null;
                if (Exp != null)
                {
                    byte[] expsource = new byte[Exp.ContentLength];
                    Exp.InputStream.Read(expsource, 0, Exp.ContentLength);
                    expBase64 = Convert.ToBase64String(expsource);
                }
             

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPU_Application", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@aid", apply.aid);
                        command.Parameters.AddWithValue("@Resume", resumeBase64);
                        command.Parameters.AddWithValue("@Photo", imageBase64);
                        command.Parameters.AddWithValue("@Acadamiccertificate", acadamicBase64);
                        command.Parameters.AddWithValue("@experiancecertificate", expBase64);
                        command.Parameters.AddWithValue("@Qualification", apply.Qualification);
                        command.Parameters.AddWithValue("@Experiance", apply.Experiance);
                        command.Parameters.AddWithValue("@Description", apply.Description);
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
        /// Updates the selection status of an application in the database.
        /// </summary>
        /// <param name="alldetailsofapplication">The Alldetailsofapplication object containing the updated status and interview date.</param>
        /// <param name="aid">The ID of the application to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool Selections(Alldetailsofapplication alldetailsofapplication, int aid)
        {
            try
            {
                int i;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand("SPU_Applicationsstatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@aid", aid);
                        command.Parameters.AddWithValue("@Status", alldetailsofapplication.Status);
                        command.Parameters.AddWithValue("@Interviewdate", alldetailsofapplication.Interviewdate);
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