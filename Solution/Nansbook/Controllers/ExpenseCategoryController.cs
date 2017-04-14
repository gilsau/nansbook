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
    public class ExpenseCategoryController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(ExpenseCategoryModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                ExpenseCategory exp = new ExpenseCategory();
                exp.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                exp.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : exp.ParentId;
                exp.Name = model.Name;
                exp.CreatedBy = User.Identity.Name;
                exp.CreatedDate = DateTime.Now;
                exp.UpdatedBy = User.Identity.Name;
                exp.UpdatedDate = DateTime.Now;

                db.ExpenseCategories.Add(exp);
                db.SaveChanges();

                TempData["msg"] = string.Format("Expense Category '{0}' was added successfully!", exp.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("ExpenseCategories", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(ExpenseCategoryModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                ExpenseCategory exp = db.ExpenseCategories.SingleOrDefault(s => s.Id == model.Id);

                if (exp != null)
                {
                    exp.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    exp.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : exp.ParentId;
                    exp.Name = model.Name;
                    exp.UpdatedBy = User.Identity.Name;
                    exp.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Expense Category '{0}' was updated successfully!", exp.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("ExpenseCategories", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                ExpenseCategory exp = db.ExpenseCategories.SingleOrDefault(s => s.Id == Id);
                string expName = exp.Name;

                if (exp != null)
                {
                    db.ExpenseCategories.Remove(exp);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Expense Category '{0}' was deleted successfully!", expName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("ExpenseCategories", "home");
        }
    }
}