using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project.DAL;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
  

    public class ProductController : Controller
    {
        // GET: Product


        Product_DAL _productDAL = new Product_DAL();
        public ActionResult Index()
        {

            var produclist=_productDAL.GetAllProducts();

            if (produclist.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products Not available";
            }
            return View(produclist);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel product )
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = _productDAL.InsertProduct(product);

                    if (isInserted)
                    {
                        TempData["Successmessage"] = "Product Saved Successfully";

                    }
                    else
                    {
                        TempData["Successmessage"] = "Unable to save";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["Successmessage"] = ex.Message;
                return View();
            }
           
            
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products=_productDAL.GetProductbyId(id).FirstOrDefault();
            if(products == null)
            {
                TempData["InfoMessage"] = "Product Not available with ID " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(ProductModel product)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.Updateproduct(product);
                    if (IsUpdated)
                    {
                        TempData["successmessage"] = "Product Updated successfully";
                    }
                    else
                    {
                        TempData["errormessage"] = "product is already";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["Errormessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {

            var product = _productDAL.GetProductbyId(id).FirstOrDefault();

            try
            {
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product is not avilable with id" + id.ToString();
                    return RedirectToAction("Index");

                }

                return View(product);
            }
            catch (Exception ex)
            {

                TempData["Errormessage"] = ex.Message;
                return View();

            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            string result=_productDAL.Deleteproducts(id);
            try
            {
                if (result.Contains("deleted"))
                {
                    TempData["successMessage"] = result;
                    ;
                }
                else
                {
                    TempData["Errormessage"] = "Error";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["Errormessage"] = ex.Message;
                return View();
            }
        }
    }
}
