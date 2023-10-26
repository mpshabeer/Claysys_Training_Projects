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

            try
            {
                if (ModelState.IsValid)
                { 
                    isInserted = _RegistrationRepository.Insertregistration(registration);

                    if (isInserted)
                    {
                        TempData["Successmessage"] = "Registration  Successfull";

                        return RedirectToAction("Signin");
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
                    command.Parameters.AddWithValue("@Password", login.Password);

                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.Read())
                    {
                        FormsAuthentication.SetAuthCookie(login.Email, false);
                        Session["Email"] = login.Email.ToString();
                        string userType = sdr["Type"].ToString();
                        if (userType == "Candidate")
                        {
                            return RedirectToAction("CandidateDashbord", "Candidate");
                        }
                        else if (userType == "HR")
                        {

                            return RedirectToAction("HRDashbord", "HR");
                        }
                        else
                        {
                            ViewBag.Message = "Unknown user type.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No user found please recheck your user name and Password";
                    }
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Messag = ex.Message;
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}