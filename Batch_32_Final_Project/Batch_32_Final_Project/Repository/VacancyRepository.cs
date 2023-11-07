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
        public bool InsertVacancy(Vacancy vacancy, string mail)
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
        public List<Vacancy> GetallVacancytocandidate()
        {
            List<Vacancy> VacancyList = new List<Vacancy>();
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
        public List<Vacancy> GetallVacancy()
        {
            List<Vacancy> VacancyList = new List<Vacancy>();

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

        public List<Vacancy> GetVacancyDetails(int vid)
        {

           
            List<Vacancy> VacancyList = new List<Vacancy>();
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
                                Createdby= Convert.ToString(datarow["Createdby"]),
                                InterviewRounds = Convert.ToString(datarow["InterviewRounds"]),
                                SelectionProcess = Convert.ToString(datarow["SelectionProcess"]),
                            }
                            );
                    }
                }
            }
            return VacancyList;
        }
        public bool UpdateTOVacancy(Vacancy vacancy)
        {

            int i = 0;

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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        public bool ImageUpload(HttpPostedFileBase Imagefile)
        {
            int i = 0;
            byte[] imgsource = new byte[Imagefile.ContentLength];
            Imagefile.InputStream.Read(imgsource, 0, Imagefile.ContentLength);
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SP_I", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Image", imgsource);
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

        public bool Applytojob(HttpPostedFileBase Resume, HttpPostedFileBase Photo, int vid,int rid,Apply applyforjob)
        {
            
            byte[] resumeSource = new byte[Resume.ContentLength];
            Resume.InputStream.Read(resumeSource, 0, Resume.ContentLength);
            string resumeBase64 = Convert.ToBase64String(resumeSource);

            byte[] imagesource = new byte[Photo.ContentLength];
            Photo.InputStream.Read(imagesource, 0, Photo.ContentLength);
            string imageBase64 = Convert.ToBase64String(imagesource);
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

        public List<Alldetailsofapplication> Getallapplieddetails(int id)
        {
            List<Alldetailsofapplication> AppliedList = new List<Alldetailsofapplication>();
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

        public bool Selection(Statusofapplication statusofapplication,int aid)
        {
            int i;
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
        public List<Alldetailsofapplication> Myapplications(int rid)
        {

            connections();
            List<Alldetailsofapplication> AppliedList = new List<Alldetailsofapplication>();
            SqlCommand command = new SqlCommand("SPD_Myapplications", connection);
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
            return AppliedList;
        }
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        public bool IsApplied(int vid, int rid)
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
        public List<Contactus> Getallcontactrequest()
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        
        public Application Getapplication(int id)
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
                            Qualification = Convert.ToString(dataReader["Qualification"]),
                            Experiance = Convert.ToString(dataReader["Experiance"]),
                            Description = Convert.ToString(dataReader["Description"]),
                        };
                    }
                }
            }
            return apply;
        }


        public Alldetailsofapplication Getapplications(int id)
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
                            Department= Convert.ToString(dataReader["Department"]),
                            Qualifications = Convert.ToString(dataReader["Qualifications"]),
                            Responsibilities = Convert.ToString(dataReader["ResponsibilitiesAndDuties"]),
                            JobTitle = Convert.ToString(dataReader["JobTitle"]),
                            JobDescription = Convert.ToString(dataReader["JobDescription"]),
                            Firstname = Convert.ToString(dataReader["Firstname"]),
                            Lastname = Convert.ToString(dataReader["Lastname"]),
                            Email = Convert.ToString(dataReader["Email"]),
                            AppliedDate= Convert.ToString(dataReader["AppliedDate"]),
                            Phone = Convert.ToString(dataReader["Phone"]),
                            Resume = Convert.ToString(dataReader["Resumes"]),
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

        public bool Updateapplications(Application apply, HttpPostedFileBase Resume, HttpPostedFileBase Photo)
        {
            int i = 0;
            byte[] resumeSource = new byte[Resume.ContentLength];
            Resume.InputStream.Read(resumeSource, 0, Resume.ContentLength);
            string resumeBase64 = Convert.ToBase64String(resumeSource);

            byte[] imagesource = new byte[Photo.ContentLength];
            Photo.InputStream.Read(imagesource, 0, Photo.ContentLength);
            string imageBase64 = Convert.ToBase64String(imagesource);

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SPU_Application", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@aid", apply.aid);
                    command.Parameters.AddWithValue("@Resume", resumeBase64);
                    command.Parameters.AddWithValue("@Photo", imageBase64);
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

        public bool Selections(Alldetailsofapplication alldetailsofapplication, int aid)
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
    }
}