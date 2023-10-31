using Batch_32_Final_Project.Models;
using Batch_32_Final_Project.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                    VacancyRepository vacancyRepository=new VacancyRepository();
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
                return View("Error"+ex);
            }
        }

        public ActionResult ViewApplications()
        {
            VacancyRepository vacancyRepository = new VacancyRepository();
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
        public ActionResult Applyforvacancy(Apply applytojob,int vid)
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

    }
}
