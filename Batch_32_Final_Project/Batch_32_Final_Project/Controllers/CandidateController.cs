using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Batch_32_Final_Project.Models;
using System.Configuration;
using Batch_32_Final_Project.Repository;
using System.Web.Services.Description;

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

        private SqlConnection connection;
        
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
                ViewBag.Message=ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Signin", "Home");
        }

       

       
        
       
    }
}
