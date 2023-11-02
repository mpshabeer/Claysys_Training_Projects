﻿using Batch_32_Final_Project.Models;
using Batch_32_Final_Project.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Batch_32_Final_Project.Controllers
{

    public class CandidateController : Controller
    {
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        RegistrationRepository registrationRepository = new RegistrationRepository();
        VacancyRepository vacancyRepository = new VacancyRepository();
        private SqlConnection connection;
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);

        }

        public ActionResult CandidateDashbord()
        {
            return View();
        }

        public ActionResult Viewvacancy()
        {
            VacancyRepository vacancyRepository = new VacancyRepository();
            ModelState.Clear();
            return View(vacancyRepository.GetallVacancy());

        }


        public ActionResult ViewVacancyDetails(int id)
        {
            try
            {
                VacancyRepository vacancyRepository = new VacancyRepository();
                ModelState.Clear();
                return View(vacancyRepository.GetVacancyDetails(id));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpGet]

        public ActionResult ImageInsertintodb()
        {
            return View();
        }


        public ActionResult ImageInsertintodb(HttpPostedFileBase Imagefile)
        {
            bool isinserted;
            try
            {
                if (Imagefile != null && Imagefile.ContentLength > 0)
                {

                    isinserted = vacancyRepository.ImageUpload(Imagefile);
                    if (isinserted)
                    {
                        return RedirectToAction("Viewvacancy");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error" + ex);
            }
        }

        public ActionResult ViewApplications()
        {

            List<Applications> applicationsList = vacancyRepository.GetApplied();
            return View(applicationsList);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Signin", "Home");
        }

        public ActionResult Applyforvacancy(int vid)
        {
            Apply applyModel = new Apply();
            ViewBag.Vid = vid;
            return View(applyModel);
        }

        /*public ActionResult Applyforvacancy(HttpPostedFileBase Resume, HttpPostedFileBase Photo,int vid)*/
        [HttpPost]
        public ActionResult Applyforvacancy(Apply applytojob, int vid)
        {
            if (ModelState.IsValid)
            {
                bool isinserted;
                try
                {
                    HttpPostedFileBase Resume = Request.Files["Resume"];
                    HttpPostedFileBase Photo = Request.Files["Photo"];
                    if (Resume != null && Photo != null)
                    {
                        VacancyRepository vacancyRepository = new VacancyRepository();
                        int rid = Convert.ToInt32(Session["rid"].ToString());
                        isinserted = vacancyRepository.Applytojob(Resume, Photo, vid, rid, applytojob);
                        if (isinserted)
                        {
                            return RedirectToAction("Viewvacancy");
                        }
                        else
                        {
                            ViewBag.Message = "Something Wrong";
                            return View();
                        }
                    }
                    ViewBag.Message = "NULL ";
                    return View();
                }
                catch (Exception ex)
                {
                    return View("Error" + ex);
                }
            }
            return View();
        }
        public ActionResult Viewmyapplication()
        {
            int rid = Convert.ToInt32(Session["rid"].ToString());
            List<Alldetailsofapplication> applicationsList = vacancyRepository.Myapplications(rid);
            return View(applicationsList);
        }

        public ActionResult Withdrawapplication(int aid)
        {

            try
            {
                bool isDeleted = vacancyRepository.Withdrawapplication(aid);
                if (isDeleted)
                {
                    ViewBag.Message = "Vacancy Deleted Successfully";
                }
                else
                {
                    ViewBag.Message = "Unable to Delete the Vacancy";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
            }
            return RedirectToAction("Viewmyapplication");
        }
        public ActionResult Getuserdetails()
        {
            int rid = Convert.ToInt32(Session["rid"].ToString());
            Userdetails userdetails = new Userdetails();
            userdetails = registrationRepository.GetDetailsofUser(rid);
            return View(userdetails);

        }

        public ActionResult Updateuserdetails()
        {
            int rid = Convert.ToInt32(Session["rid"].ToString());
            Userdetails userdetails = new Userdetails();
            userdetails = registrationRepository.GetDetailsofUser(rid);
            return View(userdetails);

        }

        [HttpPost]
        public ActionResult Updateuserdetails(Userdetails userdetails)
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = registrationRepository.Updateuserdetails(userdetails);
                    if (isInserted)
                    {
                        ViewBag.Message = "User details Updated";
                        return RedirectToAction("Getuserdetails");
                    }
                    else
                    {
                        ViewBag.Message = "Unable to save: No records were updated.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Model not Valid";
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        // Log or print the specific validation errors
                        Console.WriteLine(error.ErrorMessage);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult ChangePassword()
        {

            return View();

        }
        [HttpPost]
        public ActionResult ChangePassword(Changepassword changepassword)
        {

            Decryptpassword decryptpassword = new Decryptpassword();
            int rid = Convert.ToInt32(Session["rid"].ToString());
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
                    connection = new SqlConnection(connectionstring);
                    SqlCommand command = new SqlCommand("SPD_Oldpassword", connection);
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
                            string newpassword = changepassword.NewPassword;
                            string encryptedpassword = registrationRepository.Encrypt(newpassword);
                            bool isupdated=false;
                            isupdated = registrationRepository.ChangeUserPassword(encryptedpassword, rid);
                            if(isupdated)
                            {
                                return RedirectToAction("Getuserdetails");
                            }
                            else
                            {
                                ViewBag.Message = "Model Not VALID!!!!!!! ";
                                return View();
                            }
                            
                        }
                    }
                    else
                    {
                        ViewBag.Message = "User not found Please recheck it ";
                        return View();
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Exception "+ex;
                return View();
            }
           
        }

} } 
