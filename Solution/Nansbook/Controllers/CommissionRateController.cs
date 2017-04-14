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
    public class CommissionRateController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(CommissionRateModel model)
        {
            NansbookEntities db = new NansbookEntities();
            CommissionRate crExists = db.CommissionRates.FirstOrDefault(cr => cr.UserXStoreId == model.UserXStoreId);

            //if an entry ALREADY EXISTS for this store, send to update routine
            if (crExists != null)
            {
                model.Id = crExists.Id;
                this.Update(model);
            }

            //add new entry for this store
            else
            {
                try
                {
                    string storeName = db.UserXStores.SingleOrDefault(us => us.Id == model.UserXStoreId).Store.Name;

                    CommissionRate rate = new CommissionRate();
                    rate.UserXStoreId = model.UserXStoreId;
                    rate.StoreCommPct = model.StoreCommPct;
                    rate.StoreCashPct = model.StoreCashPct;
                    rate.CreatedBy = User.Identity.Name;
                    rate.CreatedDate = DateTime.Now;
                    rate.UpdatedBy = User.Identity.Name;
                    rate.UpdatedDate = DateTime.Now;

                    db.CommissionRates.Add(rate);
                    db.SaveChanges();

                    TempData["msg"] = string.Format("Commission Rate for store '{0}' was added successfully!", storeName);
                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;
                }
            }

            return RedirectToAction("CommissionRates", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(CommissionRateModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                string storeName = db.UserXStores.SingleOrDefault(us => us.Id == model.UserXStoreId).Store.Name;
                
                CommissionRate rate = db.CommissionRates.SingleOrDefault(s => s.Id == model.Id);
                if (rate != null)
                {
                    rate.UserXStoreId = model.UserXStoreId;
                    rate.StoreCommPct = model.StoreCommPct;
                    rate.StoreCashPct = model.StoreCashPct;
                    rate.UpdatedBy = User.Identity.Name;
                    rate.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Commission Rate for store '{0}' was updated successfully!", storeName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("CommissionRates", "home");
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
                CommissionRate rate = db.CommissionRates.SingleOrDefault(s => s.Id == Id);
                
                if (rate != null)
                {
                    db.CommissionRates.Remove(rate);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Commission Rate for store '{0}' was deleted successfully!", userStore.Store.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("CommissionRates", "home");
        }
    }
}