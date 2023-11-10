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
        /// <summary>
        ///  Displays the default view for the user.
        /// </summary>
        /// <returns>Returns the default view for the controller.</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Displays the signup view for user registration.
        /// </summary>
        /// <returns>Returns the signup view for user registration.</returns>
        public ActionResult Signup()
        {
            return View();
        }
        /// <summary>
        /// Handles the HTTP POST request for user registration.
        /// </summary>
        /// <param name="registration">The Registration object containing user registration data.</param>
        /// <returns>Returns an ActionResult based on the outcome of the user Signup process.</returns>
        
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
                            TempData["Successmessage"] = "Registration successfull";

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
                        ViewBag.Message = "Email already exisist";
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
        /// <summary>
        /// Display the Aboutus page for user
        /// </summary>
        /// <returns>Returns the About view for user</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        /// <summary>
        /// Display the Signin page for user
        /// </summary>
        /// <returns>Returns the Signin view for user</returns>
        public ActionResult Signin()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }
        /// <summary>
        /// Handle HTTP POST request for user Signin
        /// </summary>
        /// <param name="login">The lgin object containing user Signin data</param>
        /// <returns>Return to an action based on user Signin</returns>
        [HttpPost]
     
        public ActionResult Signin(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
                    RegistrationRepository registrationRepository= new RegistrationRepository();
                    if (registrationRepository.ValidateUser(login, out string userType, out string rid,out string username))
                    {
                        FormsAuthentication.SetAuthCookie(login.Email, false);
                        Session["Email"] = login.Email.ToString();
                        Session["rid"] = Convert.ToInt32(rid);
                        Session["username"] = username;
                        Session["userType"] = userType;
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
                        ViewBag.Message = "Invalid email or password. please try again.";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        /// <summary>
        /// Display the contactus page for user
        /// </summary>
        /// <returns>Return Contact View for user contactus page</returns>
        public ActionResult Contact()
        {
            return View();
        }
        /// <summary>
        /// Handle HTTP POST request for user Contact request
        /// </summary>
        /// <param name="contactus">The contactus object containing user contactus request data</param>
        /// <returns>returns to the default view for the controller</returns>
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