using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nansbook.Models;

namespace Nansbook.Controllers
{
    public class ProductController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(ProductModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Product prod = new Product();
                prod.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                prod.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : prod.ParentId;
                prod.Name = model.Name;
                prod.Price = model.Price != null ? model.Price : prod.Price;
                prod.CreatedBy = User.Identity.Name;
                prod.CreatedDate = DateTime.Now;
                prod.UpdatedBy = User.Identity.Name;
                prod.UpdatedDate = DateTime.Now;

                db.Products.Add(prod);
                db.SaveChanges();

                TempData["msg"] = string.Format("Product '{0}' was added successfully!", prod.Name);
            }
            catch (Exception ex) {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Products", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(ProductModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Product prod = db.Products.SingleOrDefault(s => s.Id == model.Id);

                if (prod != null)
                {
                    prod.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    prod.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : prod.ParentId;
                    prod.Name = model.Name;
                    prod.Price = model.Price != null ? model.Price : prod.Price;
                    prod.UpdatedBy = User.Identity.Name;
                    prod.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Product '{0}' was updated successfully!", prod.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Products", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Product prod = db.Products.SingleOrDefault(s => s.Id == Id);
                string prodName = prod.Name;
                
                if (prod != null)
                {
                    db.Products.Remove(prod);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Product '{0}' was deleted successfully!", prodName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Products", "home");
        }
    }
}