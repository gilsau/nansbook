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
    public class DeductionController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(DeductionModel model)
        {
            try
            {
             
                NansbookEntities db = new NansbookEntities();
                string storeName = db.UserXStores.SingleOrDefault(us => us.Id == model.UserXStoreId).Store.Name;
                
                Deduction deduct = new Deduction();
                deduct.UserXStoreId = model.UserXStoreId;
                deduct.FixedAmtPerSvcSale = model.FixedAmtPerSvcSale.HasValue ? model.FixedAmtPerSvcSale : deduct.FixedAmtPerSvcSale;
	            deduct.PctPerSvcSale = model.PctPerSvcSale.HasValue ? model.PctPerSvcSale : deduct.PctPerSvcSale;
	            deduct.PctTotAllSvcSales = model.PctTotAllSvcSales.HasValue ? model.PctTotAllSvcSales : deduct.PctTotAllSvcSales;
	            deduct.DailyAmt = model.DailyAmt.HasValue ? model.DailyAmt : deduct.DailyAmt;
	            deduct.DailyCleaningExp = model.DailyCleaningExp.HasValue ? model.DailyCleaningExp : deduct.DailyCleaningExp;
	            deduct.DailyCleaningExpWkday = model.DailyCleaningExpWkday;
	            deduct.TipProcessingPct = model.TipProcessingPct.HasValue ? model.TipProcessingPct : deduct.TipProcessingPct;
	            deduct.CreatedBy = User.Identity.Name;
                deduct.CreatedDate = DateTime.Now;
                deduct.UpdatedBy = User.Identity.Name;
                deduct.UpdatedDate = DateTime.Now;

                db.Deductions.Add(deduct);
                db.SaveChanges();

                TempData["msg"] = string.Format("Deductions were added for store '{0}' successfully!", storeName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Deductions", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(DeductionModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                string storeName = db.UserXStores.SingleOrDefault(us => us.Id == model.UserXStoreId).Store.Name;
                
                Deduction deduct = db.Deductions.SingleOrDefault(s => s.Id == model.Id);

                if (deduct != null)
                {
                    deduct.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    deduct.FixedAmtPerSvcSale = model.FixedAmtPerSvcSale.HasValue ? model.FixedAmtPerSvcSale : deduct.FixedAmtPerSvcSale;
                    deduct.PctPerSvcSale = model.PctPerSvcSale.HasValue ? model.PctPerSvcSale : deduct.PctPerSvcSale;
                    deduct.PctTotAllSvcSales = model.PctTotAllSvcSales.HasValue ? model.PctTotAllSvcSales : deduct.PctTotAllSvcSales;
                    deduct.DailyAmt = model.DailyAmt.HasValue ? model.DailyAmt : deduct.DailyAmt;
                    deduct.DailyCleaningExp = model.DailyCleaningExp.HasValue ? model.DailyCleaningExp : deduct.DailyCleaningExp;
                    deduct.DailyCleaningExpWkday = model.DailyCleaningExpWkday;
                    deduct.TipProcessingPct = model.TipProcessingPct.HasValue ? model.TipProcessingPct : deduct.TipProcessingPct;
                    deduct.UpdatedBy = User.Identity.Name;
                    deduct.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Deductions for store '{0}' was updated successfully!", storeName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Deductions", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Deduction deduct = db.Deductions.SingleOrDefault(s => s.Id == Id);
                string storeName = deduct.UserXStore.Store.Name;
                
                if (deduct != null)
                {
                    db.Deductions.Remove(deduct);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Deductions at store '{0}' were deleted successfully!", storeName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Deductions", "home");
        }
    }
}