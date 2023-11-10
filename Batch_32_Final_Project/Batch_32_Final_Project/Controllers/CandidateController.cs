using Batch_32_Final_Project.Models;
using Batch_32_Final_Project.Repository;
using Microsoft.Ajax.Utilities;
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
using System.Web.UI.WebControls;

namespace Batch_32_Final_Project.Controllers
{

    public class CandidateController : Controller
    {
        RegistrationRepository registrationRepository = new RegistrationRepository();
        VacancyRepository vacancyRepository = new VacancyRepository();
        private SqlConnection connection;
        private void connections()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["adoConnnectionstring"].ToString();
            connection = new SqlConnection(connectionstring);

        }
        /// <summary>
        /// Display the dashboard for candidate user
        /// </summary>
        /// <returns> Returns the ActionResult containing view for the candidate dashbord</returns>
        /// 
        public ActionResult CandidateDashbord()
        {
            return View();
        }
        /// <summary>
        /// Display the view that containing all the vacancies
        /// </summary>
        /// <returns>Returns the ActionResult containing the view with all the vacancies.</returns>
        public ActionResult Viewvacancy()
        {
            ModelState.Clear();
            return View(vacancyRepository.GetallVacancytocandidate());
        }
        /// <summary>
        /// Display details of specifiv vacancy based on provided id.
        /// </summary>
        /// <param name="id">The unique identifier of the vacancy for which the details are to be displayed</param>
        /// <returns>Return the Actionresult containing the with details of the specific vacancy. </returns>
        public ActionResult ViewVacancyDetails(int id)
        {
            try
            {
                ModelState.Clear();
                return View(vacancyRepository.GetVacancyDetails(id));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        /// <summary>
        /// Logs out the currently authenticated user, clears the forms authentication, and abandons the session.
        /// </summary>
        /// <returns>Returns an ActionResult that redirects the user to the Index action of the Home controller after logging out.</returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Signin", "Home");
        }
        /// <summary>
        /// Displays the view for applying to a specific vacancy.
        /// </summary>
        /// <param name="vid">The unique identifier of the vacancy for which the user is applying.</param>
        /// <returns>Returns the ActionResult containing the view for applying to the specified vacancy.</returns>
        public ActionResult Applyforvacancy(int vid)
        {
            Apply applyModel = new Apply();
            ViewBag.Vid = vid;
            return View(applyModel);
        }
        /// <summary>
        /// Handles the HTTP POST request for applying to a specific vacancy.
        /// </summary>
        /// <param name="applytojob">The Apply object containing the details of the application.</param>
        /// <param name="vid">The unique identifier of the vacancy for which the user is applying.</param>
        /// <returns>Returns an ActionResult based on the result of the application process. If successful, redirects to the Viewvacancy action; otherwise, returns the view with an appropriate error message.</returns>
        [HttpPost]
        public ActionResult Applyforvacancy(Apply applytojob, int vid)
        {
            if (ModelState.IsValid)
            {
                bool isinserted;
                bool isapplied;
                try
                {
                    HttpPostedFileBase Resume = Request.Files["Resume"];
                    HttpPostedFileBase Photo = Request.Files["Photo"];
                    HttpPostedFileBase Acadamiccertificate = Request.Files["Acadamiccertificate"];
                    HttpPostedFileBase Experiancecertificate = Request.Files["Experiancecertificate"];
                    if (Resume != null && Photo != null && Acadamiccertificate!=null)
                    {
                       
                        int rid = Convert.ToInt32(Session["rid"].ToString());
                        isapplied = vacancyRepository.IsApplied(vid, rid);
                        if (isapplied)
                        {
                            ViewBag.Message = "Your Application Allready saved in Database";
                            return View();
                        }
                        else
                        {
                            isinserted = vacancyRepository.Applytojob(Resume, Photo, Acadamiccertificate, Experiancecertificate, vid, rid, applytojob);
                            if (isinserted)
                            {
                                TempData["SuccessMessage"] = "Application saved";
                                return RedirectToAction("Viewvacancy");
                            }
                            else
                            {
                                ViewBag.Message = "Something Wrong";
                                return View();
                            }
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
        /// <summary>
        /// Displays the view containing all applications made by the currently logged-in user.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view with all the applications made by the currently logged-in user.</returns>
        public ActionResult Viewmyapplication()
        {
            int rid = Convert.ToInt32(Session["rid"].ToString());
            List<Alldetailsofapplication> applicationsList = vacancyRepository.Myapplications(rid);
            return View(applicationsList);
        }
        /// <summary>
        /// Withdraws a specific application based on the provided application id.
        /// </summary>
        /// <param name="aid">The unique identifier of the application to be withdrawn.</param>
        /// <returns>Returns an ActionResult after attempting to withdraw the application. 
        /// If successful, displays a success message; otherwise, displays an error message and redirects to the Viewmyapplication action.</returns>
        public ActionResult Withdrawapplication(int aid)
        {
            try
            {
                bool isDeleted = vacancyRepository.Withdrawapplication(aid);
                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Application withdraw Successfully";
                }
                else
                {
                    TempData["SuccessMessage"] = "Unable to withdraw the Application";
                }
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "An error occurred: " + ex.Message;
            }
            return RedirectToAction("Viewmyapplication");
        }
        // <summary>
        /// sisplay and displays the details of the currently logged-in user.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view with the details of the currently logged-in user.</returns>
        public ActionResult Getuserdetails()
        {
            int rid = Convert.ToInt32(Session["rid"].ToString());
            Userdetails userdetails = new Userdetails();
            userdetails = registrationRepository.GetDetailsofUser(rid);
            return View(userdetails);

        }
        /// <summary>
        /// Displays the view for updating the details of the currently logged-in user.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view for updating the details of the currently logged-in user.</returns>
        public ActionResult Updateuserdetails()
        {
            int rid = Convert.ToInt32(Session["rid"].ToString());
            Userdetails userdetails = new Userdetails();
            userdetails = registrationRepository.GetDetailsofUser(rid);
            return View(userdetails);

        }
        /// <summary>
        /// Handles the HTTP POST request for updating the details of the currently logged-in user.
        /// </summary>
        /// <param name="userdetails">The Userdetails object containing the updated details of the user.</param>
        /// <returns>Returns an ActionResult based on the result of the update operation. If successful, redirects to the Getuserdetails action; otherwise, returns the view with an appropriate error message.</returns>
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
                        TempData["SuccessMessage"] = "User details Updated";
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
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View();
            }
        }
        /// <summary>
        /// Display view to change currently logged-in user password
        /// </summary>
        /// <returns>Returns the ActionResult containing the view for changing the password of the currently logged-in user.</returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Changepassword changepassword)
        {
            bool isvalidpasswords;
            bool isupdated;
            int rid = Convert.ToInt32(Session["rid"].ToString());
            try
            {
                if (ModelState.IsValid)
                {
                    isvalidpasswords = registrationRepository.ChangePassword(changepassword, rid);
                    if (isvalidpasswords)
                    {
                        string newpassword = changepassword.NewPassword;
                        string encryptedpassword = registrationRepository.Encrypt(newpassword);
                        isupdated = registrationRepository.ChangeUserPassword(encryptedpassword, rid);
                        if (isupdated)
                        {
                            TempData["SuccessMessage"] = "Password Changed successfully";
                            return RedirectToAction("Getuserdetails");
                        }
                        else
                        {
                            ViewBag.Message = "Unable to save your password";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please Enter your password Correctly";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Please Enter the required fields";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Exception " + ex;
                return View();
            }
        }
        /// <summary>
        /// Displays the view for updating a specific application.
        /// </summary>
        /// <param name="id">The unique identifier of the application to be updated.</param>
        /// <returns>Returns the ActionResult containing the view for updating the specific application.</returns>
        public ActionResult Updateapplication(int id)
        {
            Application application = new Application();
            application = vacancyRepository.Getapplication(id);
            return View(application);
        }
        /// <summary>
        /// Displays the view for updating a specific application.
        /// </summary>
        /// <param name="id">The unique identifier of the application to be updated.</param>
        /// <returns>Returns the ActionResult containing the view for updating the specific application.</returns>
        [HttpPost]
    public ActionResult Updateapplication(Application application)
    {
            if (ModelState.IsValid)
        {
                HttpPostedFileBase Resume = Request.Files["Resume"];
                HttpPostedFileBase Photo = Request.Files["Photo"];
                HttpPostedFileBase Acadamic = Request.Files["Acadamiccertificate"];
                HttpPostedFileBase Exp = Request.Files["Experiancecertificate"];
                bool isinserted;
                if (Resume != null && Photo != null)
                {
                    try
                    {
                        isinserted = vacancyRepository.Updateapplications(application, Resume, Photo,Acadamic,Exp);
                        if (isinserted)
                        {
                            TempData["SuccessMessage"] = "Application Updated Successfully";
                            return RedirectToAction("Viewmyapplication");
                        }
                        else
                        {
                            ViewBag.Message = "Insertion error";
                            return View();
                        }
                    }
                    catch (Exception ex)
                    {
                        return View("Error" + ex);
                    }
                }
                else
                {
                    ViewBag.message = "Resume empty";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Model NOT VALID";
                return View();

            }
    }
        public ActionResult viewDetailsofapplication(int id)
        {

            Alldetailsofapplication alldetailsofapplication = new Alldetailsofapplication();
            alldetailsofapplication = vacancyRepository.Getapplications(id);
            ViewBag.aid = id;
            return View(alldetailsofapplication);
        }
    } } 
