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
    public class ExpenseController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(ExpenseModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Expense exp = new Expense();
                exp.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                exp.ExpenseCategoryId = model.ExpenseCategoryId != null ? model.ExpenseCategoryId : exp.ExpenseCategoryId;
                exp.PaymentMethodId = model.PaymentMethodId != null ? model.PaymentMethodId : exp.PaymentMethodId;
                exp.Amount = model.Amount;
                exp.Description = model.Description;
                exp.Vendor = model.Vendor;
                exp.CreatedBy = User.Identity.Name;
                exp.CreatedDate = DateTime.Now;
                exp.UpdatedBy = User.Identity.Name;
                exp.UpdatedDate = DateTime.Now;

                db.Expenses.Add(exp);
                db.SaveChanges();

                string expense = db.ExpenseCategories.SingleOrDefault(e => e.Id == model.ExpenseCategoryId).Name;

                TempData["msg"] = string.Format("Expense '{0}({1:C})' was added successfully!", expense, exp.Amount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Expenses", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(ExpenseModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Expense exp = db.Expenses.SingleOrDefault(s => s.Id == model.Id);

                if (exp != null)
                {
                    exp.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    exp.ExpenseCategoryId = model.ExpenseCategoryId != null ? model.ExpenseCategoryId : exp.ExpenseCategoryId;
                    exp.PaymentMethodId = model.PaymentMethodId != null ? model.PaymentMethodId : exp.PaymentMethodId;
                    exp.Amount = model.Amount;
                    exp.Description = model.Description;
                    exp.Vendor = model.Vendor;
                    exp.UpdatedBy = User.Identity.Name;
                    exp.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                string expense = db.ExpenseCategories.SingleOrDefault(e => e.Id == model.ExpenseCategoryId).Name;

                TempData["msg"] = string.Format("Expense '{0}({1:C})' was updated successfully!", expense, exp.Amount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Expenses", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Expense exp = db.Expenses.SingleOrDefault(s => s.Id == Id);
                string expCat = db.ExpenseCategories.SingleOrDefault(e => e.Id == Id).Name;
                decimal expAmount = exp.Amount;

                if (exp != null)
                {
                    db.Expenses.Remove(exp);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Expense '{0}({1:C})' was added successfully!", expCat, expAmount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Expenses", "home");
        }
    }
}