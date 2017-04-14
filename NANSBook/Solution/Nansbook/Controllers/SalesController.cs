using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nansbook.Models;

namespace Nansbook.Controllers
{
    public class SalesController : Controller
    {
        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult StoreSalesEntry()
        {
            NansbookEntities db = new NansbookEntities();
            List<SalesEntry> list = db.SalesEntries.ToList();
            
            webpages_Roles techRole = db.webpages_Roles.SingleOrDefault(r => r.RoleName == "Technician");
            ViewBag.Technicians = db.webpages_Roles.SingleOrDefault(r => r.RoleName == "Technician").UserProfiles;
            ViewBag.Services = db.Categories.Where(s => s.CategoryType == 2);
            ViewBag.Products = db.Categories.Where(s => s.CategoryType == 6);
            ViewBag.Discounts = db.Categories.Where(s => s.CategoryType == 4);

            return View(list);
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult IndSalesEntry()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Services()
        {
            NansbookEntities db = new NansbookEntities();
            List<Category> list = db.Categories.Where(c => c.CategoryType == 2).ToList();
            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddService(CategoryModel model)
        {
            try
            {
                //create service
                NansbookEntities db = new NansbookEntities();
                Category cat = new Category();
                cat.CategoryType = 2;
                cat.Name = model.Name;
                cat.Description = model.Description;
                cat.Price = decimal.Parse(model.Price);
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred adding this service. {0}", ex.Message);
            }

            return RedirectToAction("Services");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdateService(CategoryModel model)
        {
            try
            {
                //update service
                NansbookEntities db = new NansbookEntities();
                Category cat = db.Categories.Single(c => c.Id == model.Id);
                cat.Name = model.Name;
                cat.Description = model.Description;
                cat.Price = decimal.Parse(model.Price);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred updating the service. {0}", ex.Message);
            }

            return RedirectToAction("Services");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelService(CategoryModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if (ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    int id2 = int.Parse(id);
                    Category cat = db.Categories.Single(c => c.Id == id2);

                    if (cat != null)
                    {
                        //remove category
                        db.Categories.Remove(cat);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Services");
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Products()
        {
            NansbookEntities db = new NansbookEntities();
            List<Category> list = db.Categories.Where(c => c.CategoryType == 6).ToList();
            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddProduct(CategoryModel model)
        {
            try
            {
                //create service
                NansbookEntities db = new NansbookEntities();
                Category cat = new Category();
                cat.CategoryType = 6;
                cat.Name = model.Name;
                cat.Description = model.Description;
                cat.Price = decimal.Parse(model.Price);
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred adding this product. {0}", ex.Message);
            }

            return RedirectToAction("Products");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdateProduct(CategoryModel model)
        {
            try
            {
                //update service
                NansbookEntities db = new NansbookEntities();
                Category cat = db.Categories.Single(c => c.Id == model.Id);
                cat.Name = model.Name;
                cat.Description = model.Description;
                cat.Price = decimal.Parse(model.Price);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred updating this product. {0}", ex.Message);
            }

            return RedirectToAction("Products");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelProduct(CategoryModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if (ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    int id2 = int.Parse(id);
                    Category cat = db.Categories.Single(c => c.Id == id2);

                    if (cat != null)
                    {
                        //remove category
                        db.Categories.Remove(cat);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Products");
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult PayMethods()
        {
            NansbookEntities db = new NansbookEntities();
            List<Category> list = db.Categories.Where(c => c.CategoryType == 3).ToList();
            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddPayMethod(CategoryModel model)
        {
            try
            {
                //create service
                NansbookEntities db = new NansbookEntities();
                Category cat = new Category();
                cat.CategoryType = 3;
                cat.Name = model.Name;
                cat.Description = model.Description;
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred adding this pay method. {0}", ex.Message);
            }

            return RedirectToAction("PayMethods");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdatePayMethod(CategoryModel model)
        {
            try
            {
                //update service
                NansbookEntities db = new NansbookEntities();
                Category cat = db.Categories.Single(c => c.Id == model.Id);
                cat.Name = model.Name;
                cat.Description = model.Description;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred updating this pay method. {0}", ex.Message);
            }

            return RedirectToAction("PayMethods");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelPayMethod(CategoryModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if (ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    int id2 = int.Parse(id);
                    Category cat = db.Categories.Single(c => c.Id == id2);

                    if (cat != null)
                    {
                        //remove category
                        db.Categories.Remove(cat);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("PayMethods");
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Discounts()
        {
            NansbookEntities db = new NansbookEntities();
            List<Category> list = db.Categories.Where(c => c.CategoryType == 4).ToList();
            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddDiscount(CategoryModel model)
        {
            try
            {
                //create service
                NansbookEntities db = new NansbookEntities();
                Category cat = new Category();
                cat.CategoryType = 4;
                cat.Name = model.Name;
                cat.Description = model.Description;
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred adding this discount. {0}", ex.Message);
            }

            return RedirectToAction("Discounts");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdateDiscount(CategoryModel model)
        {
            try
            {
                //update service
                NansbookEntities db = new NansbookEntities();
                Category cat = db.Categories.Single(c => c.Id == model.Id);
                cat.Name = model.Name;
                cat.Description = model.Description;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred updating this discount. {0}", ex.Message);
            }

            return RedirectToAction("Discounts");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelDiscount(CategoryModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if (ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    int id2 = int.Parse(id);
                    Category cat = db.Categories.Single(c => c.Id == id2);

                    if (cat != null)
                    {
                        //remove category
                        db.Categories.Remove(cat);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Discounts");
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult GiftCards()
        {
            return View();
        }

    }

    public class SalesEntryModel
    {
        public int TechnicianId { get; set; }
        public int ServiceId { get; set; }
        public int ProductId { get; set; }
        public decimal SaleAmt { get; set; }
        public decimal TipAmt { get; set; }
        public decimal DeductionAmt { get; set; }
        public int DiscountId { get; set; }
        public DateTime TimeOfSale { get; set; }
    }
}
