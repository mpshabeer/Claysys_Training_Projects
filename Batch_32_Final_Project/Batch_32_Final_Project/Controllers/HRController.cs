using System;
using System.Collections.Generic;
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
    }
}

