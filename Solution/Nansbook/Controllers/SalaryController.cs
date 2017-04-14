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
    public class SalaryController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(SalaryModel model)
        {
            NansbookEntities db = new NansbookEntities();
            Salary sExists = db.Salaries.FirstOrDefault(s => s.UserXStoreId == model.UserXStoreId);

            //if an entry ALREADY EXISTS for this store, send to update routine
            if (sExists != null)
            {
                model.Id = sExists.Id;
                this.Update(model);
            }

            //add new entry for this store
            else
            {
                try
                {
                    string storeName = db.UserXStores.SingleOrDefault(us => us.Id == model.UserXStoreId).Store.Name;

                    Salary salary = new Salary();
                    salary.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    salary.Amount = model.Amount;
                    salary.ExpirationDate = model.ExpirationDate != null ? model.ExpirationDate : salary.ExpirationDate;
                    salary.CreatedBy = User.Identity.Name;
                    salary.CreatedDate = DateTime.Now;
                    salary.UpdatedBy = User.Identity.Name;
                    salary.UpdatedDate = DateTime.Now;

                    db.Salaries.Add(salary);
                    db.SaveChanges();

                    TempData["msg"] = string.Format("Salary for store '{0}' was added successfully!", storeName);
                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;
                }
            }

            return RedirectToAction("Salaries", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(SalaryModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                string storeName = db.UserXStores.SingleOrDefault(us => us.Id == model.UserXStoreId).Store.Name;
                
                Salary salary = db.Salaries.SingleOrDefault(s => s.Id == model.Id);

                if (salary != null)
                {
                    salary.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    salary.Amount = model.Amount;
                    salary.ExpirationDate = model.ExpirationDate != null ? model.ExpirationDate : salary.ExpirationDate;
                    salary.UpdatedBy = User.Identity.Name;
                    salary.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Salary for store '{0}' was updated successfully!", storeName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Salaries", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                UserXStore userStore = Helpers.UserProvider.GetCurrentUserStore();
                NansbookEntities db = new NansbookEntities();
                Salary salary = db.Salaries.SingleOrDefault(s => s.Id == Id);
                
                if (salary != null)
                {
                    db.Salaries.Remove(salary);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Salary for store '{0}' was deleted successfully!", userStore.Store.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Salaries", "home");
        }
    }
}