using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        VacancyRepository _vacancyRepository=new VacancyRepository();
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
                    isInserted = _vacancyRepository.InsertVacancy(vacancy, mail);
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
            VacancyRepository vacancyRepository = new VacancyRepository();
            ModelState.Clear();
            return View(vacancyRepository.GetallVacancy());
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}

