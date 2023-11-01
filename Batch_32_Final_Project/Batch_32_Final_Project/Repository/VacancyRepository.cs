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
                using (SqlCommand command = new SqlCommand("Inserttovacancy", connection))
                {
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
        public List<Vacancy> GetallVacancy()
        {
            List<Vacancy> VacancyList = new List<Vacancy>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand("GetVacancylist", connection);
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
                        PostedDate = Convert.ToString(datarow["PostedDate"]),
                        LastDateToApply = Convert.ToString(datarow["LastDateToApply"]),
                        Createdby = Convert.ToString(datarow["Createdby"])
                    });
                }
            }

            return VacancyList;
        }

        public List<Vacancy> GetVacancyDetails(int vid)
        {

           
            List<Vacancy> VacancyList = new List<Vacancy>();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand("DetailsofVacancy", connection);
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
                            Qualification = Convert.ToString(datarow["Qualification"]),
                            ResponsibilitiesAndDuties = Convert.ToString(datarow["ResponsibilitiesAndDuties"]),
                            SalaryRange = Convert.ToString(datarow["SalaryRange"]),
                            PostedDate = Convert.ToString(datarow["PostedDate"]),
                            LastDateToApply = Convert.ToString(datarow["LastDateToApply"]),
                            RecruiterInCharge = Convert.ToString(datarow["RecruiterInCharge"]),
                            InterviewRounds = Convert.ToString(datarow["InterviewRounds"]),
                            SelectionProcess = Convert.ToString(datarow["SelectionProcess"]),
                        }
                        );
                }
            }
            return VacancyList;
        }
        public bool UpdateTOVacancy(Vacancy vacancy)
        {

            connections();
            

            SqlCommand command = new SqlCommand("UpdateVacancy", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", vacancy.vid);
            command.Parameters.AddWithValue("@JobTitle", vacancy.JobTitle);
            command.Parameters.AddWithValue("@JobDescription", vacancy.JobDescription);
            command.Parameters.AddWithValue("@Department", vacancy.Department);
            command.Parameters.AddWithValue("@Location", vacancy.Location);
            command.Parameters.AddWithValue("@VacancyStatus", vacancy.VacancyStatus);
            command.Parameters.AddWithValue("@NumberOfOpenings", Convert.ToInt32(vacancy.NumberOfOpenings));
            command.Parameters.AddWithValue("@Qualification", vacancy.Qualification);
            command.Parameters.AddWithValue("@ResponsibilitiesAndDuties", vacancy.ResponsibilitiesAndDuties);
            command.Parameters.AddWithValue("@SalaryRange", vacancy.SalaryRange);
            command.Parameters.AddWithValue("@LastDateToApply", vacancy.LastDateToApply);
            command.Parameters.AddWithValue("@RecruiterInCharge", vacancy.RecruiterInCharge);
            command.Parameters.AddWithValue("@InterviewRounds", Convert.ToInt32(vacancy.InterviewRounds));
            command.Parameters.AddWithValue("@SelectionProcess", vacancy.SelectionProcess);
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

        public bool DeleteTHEvacancy(int id)
        {
            connections();

            try
            {
                SqlCommand command = new SqlCommand("DeleteVacancy", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vid", id);
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        public bool ImageUpload(HttpPostedFileBase Imagefile)
        {

            byte[] imgsource = new byte[Imagefile.ContentLength];
            Imagefile.InputStream.Read(imgsource, 0, Imagefile.ContentLength);
            connections();

            connection.Open();
            SqlCommand command = new SqlCommand("SP_I", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Image", imgsource);
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

        public List<Applications> GetApplied()
        {
             List<Applications> ApplicationsList = new List<Applications>();
                connections();

                SqlCommand command = new SqlCommand("SPD_Applications", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();


                connection.Open();
                dataAdapter.Fill(dataTable);
                connection.Close();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    byte[] imageData;
                    try
                    {
                        string base64String = dataRow["image"].ToString();
                        imageData = Convert.FromBase64String(base64String);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Error converting from Base64: " + ex.Message);
                        continue;
                    }

                    ApplicationsList.Add(new Applications
                    {
                        id = Convert.ToInt32(dataRow["id"]),
                        Imagefile = imageData
                    });
                }

                return ApplicationsList;
        }

        public bool Applytojob(HttpPostedFileBase Resume, HttpPostedFileBase Photo, int vid,int rid,Apply applyforjob)
        {
            
            byte[] resumeSource = new byte[Resume.ContentLength];
            Resume.InputStream.Read(resumeSource, 0, Resume.ContentLength);
            string resumeBase64 = Convert.ToBase64String(resumeSource);

            byte[] imagesource = new byte[Photo.ContentLength];
            Photo.InputStream.Read(imagesource, 0, Photo.ContentLength);
            string imageBase64 = Convert.ToBase64String(imagesource);


            connections();

            connection.Open();
            SqlCommand command = new SqlCommand("SPI_job", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@vid", vid);
            command.Parameters.AddWithValue("@rid", rid);
            command.Parameters.AddWithValue("@Resume", resumeBase64);
            command.Parameters.AddWithValue("@Photo", imageBase64);
            command.Parameters.AddWithValue("@Experiance", applyforjob.Experiance);
            command.Parameters.AddWithValue("@Qualification", applyforjob.Qualification);
            command.Parameters.AddWithValue("@Description", applyforjob.Description);
        
          
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

        public List<Alldetailsofapplication> Getallapplieddetails(int id)
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);
            List<Alldetailsofapplication> AppliedList = new List<Alldetailsofapplication>();
            SqlCommand command = new SqlCommand("SPD_Allapplications", connection);
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
                        aid= Convert.ToInt32(datarow["aid"]),
                        rid = Convert.ToInt32(datarow["rid"]),
                        JobTitle= Convert.ToString(datarow["JobTitle"]),
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

            return AppliedList;
        }

        public bool Selection(Statusofapplication statusofapplication,int aid)
        {

            connections();
            

            SqlCommand command = new SqlCommand("SPU_Applicationsstatus", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@aid", aid);
            command.Parameters.AddWithValue("@Status", statusofapplication.Status);
            command.Parameters.AddWithValue("@Interviewdate", statusofapplication.Interviewdate);
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