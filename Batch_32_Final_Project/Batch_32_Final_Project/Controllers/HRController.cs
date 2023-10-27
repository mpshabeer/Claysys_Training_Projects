using System;
using System.Collections.Generic;
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
        // GET: HR
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
        

            try
            {
                if (ModelState.IsValid)
                {
                    if (isInserted)
                    {
                        TempData["Successmessage"] = "New vacancy Added";

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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}

