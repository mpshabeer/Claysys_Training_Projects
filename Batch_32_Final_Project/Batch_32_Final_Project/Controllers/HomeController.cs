using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Batch_32_Final_Project.Repository;
using Batch_32_Final_Project.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace Batch_32_Final_Project.Controllers
{
    public class HomeController : Controller
    {

        RegistrationRepository _RegistrationRepository = new RegistrationRepository();
        ContactRepository _ContactRepository = new ContactRepository();
        Decryptpassword decryptpassword = new Decryptpassword();
        EmailexistRepository emailexist = new EmailexistRepository();
        private SqlConnection connection;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Registration registration)
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
                        isInserted = _RegistrationRepository.Insertregistration(registration);
                        if (isInserted)
                        {
                            TempData["Successmessage"] = "Registration  Successfull";

                            return RedirectToAction("Signin");
                        }
                        else
                        {
                            ViewBag.Message = "Unable to save";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Email Exisist";
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Signin()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Signin(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
                    connection = new SqlConnection(connectionstring);
                    SqlCommand command = new SqlCommand("LoginValidation", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", login.Email);
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.Read())
                    {
                        string encryptedpasswords = sdr["Password"].ToString();
                        string decryptedpassword = decryptpassword.Decrypt(encryptedpasswords);
                        if (login.Password == decryptedpassword)
                        {
                            FormsAuthentication.SetAuthCookie(login.Email, false);
                            Session["Email"] = login.Email.ToString();
                            string userType = sdr["Type"].ToString();
                            try
                            {
                                if (userType == "Candidate")
                                {
                                    return RedirectToAction("CandidateDashbord", "Candidate");
                                }
                                else if (userType == "HR")
                                {

                                    return RedirectToAction("HRDashbord", "HR");
                                }
                            }
                            catch (Exception ex)
                            {

                                ViewBag.Message = ex;
                                return View("Error");
                            }
                           
                        }
                        else
                        {
                            ViewBag.Message = "Password doesn't Match Please recheck it";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Email not found Please recheck it ";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Messag = ex.Message;
                return View();
            }
        }

        public ActionResult Contact()
        {
   
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contactus contactus)
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = _ContactRepository.InsertContact(contactus);

                    if (isInserted)
                    {
                        TempData["Successmessage"] = "Your request saved";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Successmessage"] = "Unable to save";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Messags = ex.Message;
                return View();
            }
        }
    }
}