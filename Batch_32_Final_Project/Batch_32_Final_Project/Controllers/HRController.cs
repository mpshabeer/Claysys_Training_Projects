using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Batch_32_Final_Project.Models;
using Batch_32_Final_Project.Repository;

namespace Batch_32_Final_Project.Controllers
{
   
    public class HRController : Controller
    {
        private SqlConnection connection;
        // GET: HR
        EmailexistRepository emailexist = new EmailexistRepository();
        RegistrationRepository registrationRepository = new RegistrationRepository();
        VacancyRepository vacancyRepository = new VacancyRepository();
        public ActionResult HRDashbord()
        {
            return View();
        }

        public ActionResult Addvacancy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Addvacancy(Vacancy vacancy)
        {
            bool isInserted = false;
            string mail = (string)Session["Email"];


            try
            {

                if (ModelState.IsValid)
                {
                    isInserted = vacancyRepository.InsertVacancy(vacancy, mail);
                    if (isInserted)
                    {
                        ViewBag.Message = "New Vacancy Added";

                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Unable to save";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = " Error";
                    return View();
                }

            }

            catch (Exception)
            {
                ViewBag.Messags = "exception error";
                return View();
            }
        }

        public ActionResult Viewvacancy()
        {
          
            ModelState.Clear();
            return View(vacancyRepository.GetallVacancy());
        }

        public ActionResult UpdateVacancy(int id)
        {
            
            return View(vacancyRepository.GetVacancyDetails(id).Find(Vacancy => Vacancy.vid == id));
        }

        [HttpPost]
        public ActionResult UpdateVacancy(Vacancy vacancy)
        {

            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = vacancyRepository.UpdateTOVacancy(vacancy);
                    if (isInserted)
                    {
                        ViewBag.Message = "Vacancy Updated";
                        return RedirectToAction("Viewvacancy");
                    }
                    else
                    {
                        ViewBag.Message = "Unable to save";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = " Error";
                    return View();
                }

            }

            catch (Exception)
            {
                ViewBag.Messags = "exception error";
                return View();
            }

        }

        public ActionResult DeleteVacancy(int id)
        {
            try
            {
                bool isDeleted = vacancyRepository.DeleteTHEvacancy(id);
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
            return RedirectToAction("Viewvacancy");
        }

        public ActionResult ViewAppliedDetails(int id)
        {
            
            ModelState.Clear();
            return View(vacancyRepository.Getallapplieddetails(id));
        }
        public ActionResult Actionstatus(int id)
        {
            ViewBag.aid = id;
            return View();
        }
        [HttpPost]
        public ActionResult Actionstatus(Statusofapplication statusofapplication, int aid)
        {
            
            try
            {
                bool IsSelected = vacancyRepository.Selection(statusofapplication, aid);
                if (IsSelected)
                {
                    ViewBag.Message = "Staus Updated";
                }
                else
                {
                    ViewBag.Message = "Unable to Update the status";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
            }
            return RedirectToAction("Viewappliedlist");
        }
    
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Viewappliedlist()
        {
            
            ModelState.Clear();
            return View(vacancyRepository.GetallVacancy());
        }

        public ActionResult CeatenewHR()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CeatenewHR(Registration registration)
        {
            bool isInserted = false;
            bool isvalidmail = false;
            try
            {

                if (ModelState.IsValid)
                {
                    isvalidmail = emailexist.checkemail(registration);
                    if (isvalidmail)
                    {
                        isInserted = registrationRepository.InserttoHR(registration);
                        if (isInserted)
                        {
                            TempData["Successmessage"] = "Registration of HR Successfull";

                            return View();
                        }
                        else
                        {
                            ViewBag.Message = "Unable to save";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Email Already  Exisist";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception)
            {
                ViewBag.Messags = "exception error";
                return View();
            }
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
                            bool isupdated = false;
                            isupdated = registrationRepository.ChangeUserPassword(encryptedpassword, rid);
                            if (isupdated)
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
                ViewBag.Message = "Exception " + ex;
                return View();
            }

        }
    }
}

