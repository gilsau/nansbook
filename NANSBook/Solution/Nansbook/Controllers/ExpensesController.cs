using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nansbook.Models;

namespace Nansbook.Controllers
{
    public class ExpensesController : Controller
    {
        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult StoreExpEntry()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult IndExpEntry()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Expenses()
        {
            NansbookEntities db = new NansbookEntities();
            List<Category> list = db.Categories.Where(c2 => c2.CategoryType == 1 && (c2.ParentId == 0 || c2.ParentId == null)).OrderBy(c2 => c2.Name).ToList();
            
            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddExpense(CategoryModel model)
        {
            try
            {
                //create service
                NansbookEntities db = new NansbookEntities();
                Category cat = new Category();
                cat.CategoryType = 1;
                cat.ParentId = model.ParentId > 0 ? model.ParentId : cat.ParentId;
                cat.Name = model.Name;
                cat.Description = model.Description;
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred adding this expense. {0}", ex.Message);
            }

            return RedirectToAction("Expenses");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdateExpense(CategoryModel model)
        {
            try
            {
                //update service
                NansbookEntities db = new NansbookEntities();
                Category cat = db.Categories.Single(c => c.Id == model.Id);
                cat.ParentId = model.ParentId > 0 ? model.ParentId : cat.ParentId;
                cat.Name = model.Name;
                cat.Description = model.Description;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred updating this expense. {0}", ex.Message);
            }

            return RedirectToAction("Expenses");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelExpense(CategoryModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if (ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    //find cat
                    int id2 = int.Parse(id);
                    Category cat = db.Categories.Single(c => c.Id == id2);
                    
                    //delete subcategories
                    List<int> subCats = cat.Category1.Select(sc => sc.Id).ToList();
                    foreach (int subcatId in subCats)
                    {
                        Category subcat = db.Categories.Single(c => c.Id == subcatId);
                        db.Categories.Remove(subcat);
                    }

                    ////remove category
                    if (cat != null)
                    {
                        db.Categories.Remove(cat);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Expenses");
        }

    }
}
