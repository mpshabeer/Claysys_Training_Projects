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
        Decryptpassword decryptpassword = new Decryptpassword();
        EmailexistRepository emailexist = new EmailexistRepository();
        RegistrationRepository registrationRepository = new RegistrationRepository();
        VacancyRepository vacancyRepository = new VacancyRepository();

        /// <summary>
        ///  Displays the HRDashbord view for the HR.
        /// </summary>
        /// <returns>Returns the default view for the controller.</returns>
        public ActionResult HRDashbord()
        {

            return View();
        }
        /// <summary>
        /// Display Addvacancy form for creating new Job poster
        /// </summary>
        /// <returns>Returns the Addvacancy view for the controller</returns>
        public ActionResult Addvacancy()
        {
            return View();
        }
        /// <summary>
        ///  Handles the HTTP POST request for Addvacancy .
        /// </summary>
        /// <param name="vacancy">The vacancy object containing Jobposter data.</param>
        /// <returns>>Returns an ActionResult based on the outcome of the Addvacancy process.</returns>
        [HttpPost]
        public ActionResult Addvacancy(Vacancy vacancy)
        {
            // Flag to track whether the update operation was successful.
            bool isInserted = false;
            // Retrieving the email from the session.
            string mail = (string)Session["Email"];
            try
            {
                if (ModelState.IsValid)
                {
                    // Inserting the new vacancy into the repository if the ModelState is valid.
                    isInserted = vacancyRepository.InsertVacancy(vacancy, mail);
                    if (isInserted)
                    {
                        // Setting a success message if the vacancy update is successful and redirecting to the Viewvacancy action.
                        TempData["SuccessMessage"] = "New Jobposter Added";
                        return RedirectToAction("Addvacancy");
                    }
                    else
                    {
                        // Setting a error message if the update operation fails and returning the current view.
                        ViewBag.Message = "Unable to save";
                        return View();
                    }
                }
                else
                {
                    // Setting a error message if the ModelState is invalid  and returning the current view.
                    ViewBag.Message = "Please fill requird fields ";
                    return View();
                }
            }

            catch (Exception)
            {
                ViewBag.Messags = "exception error";
                return View();
            }
        }
        /// <summary>
        /// Displays the vacancies on the view.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view with all the vacancies.</returns>
        public ActionResult Viewvacancy()
        {
            // Clearing the model state to prevent invalid or unwanted data from persisting.
            ModelState.Clear();
            // Returning the view with all the vacancies fetched from the VacancyRepository.
            return View(vacancyRepository.GetallVacancy());
        }
        /// <summary>
        /// Displays the form to update a specific vacancy.
        /// </summary>
        /// <param name="id">The unique identifier of the vacancy to be updated.</param>
        /// <returns>Returns the ActionResult containing the view with the details of the specific vacancy to be updated.</returns>
        public ActionResult UpdateVacancy(int id)
        {
            // Retrieving the specific vacancy details from the vacancy repository by its unique identifier.
            // The Find method is used to locate the specific vacancy based on the provided id.
            return View(vacancyRepository.GetVacancyDetails(id).Find(Vacancy => Vacancy.vid == id));
        }
        /// <summary>
        /// Handles the HTTP POST request for updating a vacancy.
        /// </summary>
        /// <param name="vacancy">The Vacancy object containing the updated details of the vacancy.</param>
        /// <returns>Returns an ActionResult based on the result of the update operation. If successful, redirects to the Viewvacancy action; otherwise, returns the View with appropriate error messages.</returns>
        [HttpPost]
        public ActionResult UpdateVacancy(Vacancy vacancy)
        {
            // Flag to track whether the update operation was successful.
            bool isInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    // Updating the vacancy in the repository if the ModelState is valid.
                    isInserted = vacancyRepository.UpdateTOVacancy(vacancy);
                    if (isInserted)
                    {
                        // Setting a success message if the vacancy update is successful and redirecting to the Viewvacancy action.
                        TempData["SuccessMessage"] = "Job Poster Updated Successfully ";
                        return RedirectToAction("Viewvacancy");
                    }
                    else
                    {
                        // Setting a error message if the update operation fails and returning the current view.
                        ViewBag.Message = "Unable to save";
                        return View();
                    }
                }
                else
                {
                    // Setting a error message if the ModelState is invalid and returning the current view.
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
        /// <summary>
        /// Deletes a specific vacancy based on the provided id.
        /// </summary>
        /// <param name="id">The unique identifier of the vacancy to be deleted.</param>
        /// <returns>Returns an ActionResult after attempting to delete the vacancy. 
        /// If successful, displays a success message; otherwise, displays an error message and redirects to the Viewvacancy action.</returns>
        public ActionResult DeleteVacancy(int id)
        {
            try
            {
                bool isDeleted = vacancyRepository.DeleteTHEvacancy(id);
                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Jobposter Deleted Successfully";
                }
                else
                {
                    ViewBag.Message = "Unable to Delete the Jobposter";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
            }
            return RedirectToAction("Viewvacancy");
        }
        /// <summary>
        /// Displays the details of the applicants who have applied for a specific vacancy.
        /// </summary>
        /// <param name="id">The unique identifier of the vacancy for which the applicant details are to be displayed.</param>
        /// <returns>Returns the ActionResult containing the view with the details of the applicants who have applied for the specified vacancy.</returns>
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
        /// Displays the list of all vacancies.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view with the list of all vacancies.</returns>
        public ActionResult Viewappliedlist()
        {
            ModelState.Clear();
            return View(vacancyRepository.GetallVacancy());
        }
        /// <summary>
        /// Displays the view for creating a new HR (Human Resources) user.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view for creating a new HR user.</returns>
        public ActionResult CeatenewHR()
        {
            return View();
        }
        /// <summary>
        /// Handles the HTTP POST request for creating a new HR (Human Resources) user.
        /// </summary>
        /// <param name="registration">The Registration object containing the details of the new HR user.</param>
        /// <returns>Returns an ActionResult based on the result of the creation operation. 
        /// If successful, returns the view with a success message; otherwise, returns the view with an appropriate error message.</returns>
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
                            TempData["SuccessMessage"] = "Registration of HR Successfull";
                            return RedirectToAction("CeatenewHR");
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
        /// <summary>
        /// Displays the view for updating the details of the currently logged-in HR (Human Resources) user.
        /// </summary>
        /// <returns>Returns the ActionResult containing the view for updating the details of the currently logged-in HR user.</returns>
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
        /// <returns>Returns an ActionResult based on the result of the update operation. 
        ///  If successful,redirects to the Getuserdetails action; otherwise, returns the view with an appropriate error message.</returns>
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
                        TempData["SuccessMessage"]  = "Details Updated Successfully !!";
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
        /// <summary>
        /// Displays the view for Changing the password to currently logged-in HR.
        /// </summary>
        ///<returns>Returns the ActionResult containing the view for updating Password of the currently logged-in HR user.</returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        ///  Handles the HTTP POST request for updating the password of the currently logged-in user.
        /// </summary>
        /// <param name="changepassword">The Changepassword object containing the updated password of the user</param>
        /// <returns>Returns an ActionResult based on the result of the update operation. 
        ///  If successful,redirects to the Getuserdetails action; otherwise, returns the view with an appropriate error message.</returns>
        [HttpPost]
        public ActionResult ChangePassword(Changepassword changepassword)
        {
            bool isvalidpasswords;
            bool isupdated ;
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
                            TempData["SuccessMessage"] = "Password has been changed successfully.";
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
        /// Displays the view containing all the contact request
        ///<returns>Returns the ActionResult containing the view for all contact request.</returns>
        public ActionResult Viewcontact()
        {
            return View(vacancyRepository.Getallcontactrequest());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteContact(int id)
        {
            try
            {
                bool isDeleted = vacancyRepository.DeleteContactus(id);
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
            return RedirectToAction("Viewcontact");
        }
        /// <summary>
        /// Displays the view for updating a specific application status.
        /// </summary>
        /// <param name="id">The unique identifier of the application to be updated.</param>
        /// <returns>Returns the ActionResult containing the view for updating the status of the specific application.</returns>
        public ActionResult Updateapplication(int id)
        {

            Alldetailsofapplication alldetailsofapplication = new Alldetailsofapplication();
            alldetailsofapplication = vacancyRepository.Getapplications(id);
            ViewBag.aid = id;
            return View(alldetailsofapplication);
        }
        /// <summary>
        /// Handles the HTTP POST request for updating the application status.
        /// </summary>
        /// <param name="application">The Alldetailsofapplication object containing the updated details of the application.</param>
        /// <returns>Returns an ActionResult based on the result of the status update operation.
        /// If successful, redirects to the Updateapplication action with the updated application details;
        /// otherwise, returns the view with an appropriate error message.</returns>
        [HttpPost]
        public ActionResult Updateapplication(Alldetailsofapplication application)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool IsSelected = vacancyRepository.Selections(application,application.aid);
                    if (IsSelected)
                    {
                        ViewBag.Message = "Staus Updated";
                        return RedirectToAction("Updateapplication", new { id = application.aid });
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
            }
            else
            {
                ViewBag.Message = "Model Not Valid";
                return View();
            }
            ViewBag.Message = "Updated";
            return View();
        }
        /// <summary>
        /// Display the details of vacancy with curresponding id
        /// </summary>
        /// <param name="id">The ID of the vacancy</param>
        /// <returns>Returns a view displaying the details of the specified vacancy</returns>
        public ActionResult ViewVacancyDetails(int id)
        {
            try
            {
                ModelState.Clear();
                // Retrieve vacancy details from the repository based on the provided ID
                return View(vacancyRepository.GetVacancyDetails(id));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}

