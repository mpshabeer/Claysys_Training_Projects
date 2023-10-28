using Batch_32_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

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
        public bool InsertVacancy(Vacancy vacancy,string mail)
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
        }
        public List<Vacancy> GetallVacancy()
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);
            List<Vacancy> VacancyList = new List<Vacancy>();
            SqlCommand command = new SqlCommand("GetVacancylist", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();

            connection.Open();
            dataadapter.Fill(datatable);
            connection.Close();
            foreach (DataRow datarow in datatable.Rows)
            {

                VacancyList.Add(

                    new Vacancy
                    {
                        vid = Convert.ToInt32(datarow["vid"]),
                        JobTitle = Convert.ToString(datarow["JobTitle"]),
                        JobDescription = Convert.ToString(datarow["JobDescription"]),
                        PostedDate = Convert.ToString(datarow["PostedDate"]),
                        LastDateToApply = Convert.ToString(datarow["LastDateToApply"]),
                        Createdby = Convert.ToString(datarow["Createdby"])
                    }
                    );
            }

            return VacancyList;
        }

        public List<Vacancy> GetVacancyDetails(int vid)
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);
            List<Vacancy> VacancyList = new List<Vacancy>();
            SqlCommand command = new SqlCommand("DetailsofVacancy", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@vid", vid);
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();

            connection.Open();
            dataadapter.Fill(datatable);
            connection.Close();
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

            return VacancyList;
        }


    }
}