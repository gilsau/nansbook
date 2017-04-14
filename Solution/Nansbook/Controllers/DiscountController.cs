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
    public class DiscountController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(DiscountModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Discount disc = new Discount();
                disc.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                disc.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : disc.ParentId;
                disc.ProductId = model.ProductId > 0 ? model.ProductId : disc.ProductId;
                disc.ServiceId = model.ServiceId > 0 ? model.ServiceId : disc.ServiceId;
                disc.Name = model.Name;
                disc.Amount = model.Amount != null ? model.Amount : disc.Amount;
                disc.Percentage = model.Percentage != null ? model.Percentage : disc.Percentage;
                disc.ExpirationDate = model.ExpirationDate != null ? model.ExpirationDate : disc.ExpirationDate;
                disc.CreatedBy = User.Identity.Name;
                disc.CreatedDate = DateTime.Now;
                disc.UpdatedBy = User.Identity.Name;
                disc.UpdatedDate = DateTime.Now;

                db.Discounts.Add(disc);
                db.SaveChanges();

                string item = model.ProductId != null ? db.Products.SingleOrDefault(p => p.Id == model.ProductId).Name : (model.ServiceId != null ? db.Services.SingleOrDefault(p => p.Id == model.ServiceId).Name : string.Empty);

                TempData["msg"] = string.Format("Discount '{0}({1:C})' was added successfully!", item, disc.Amount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Discounts", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(DiscountModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Discount disc = db.Discounts.SingleOrDefault(s => s.Id == model.Id);

                if (disc != null)
                {
                    disc.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    disc.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : disc.ParentId;
                    disc.ProductId = model.ProductId > 0 ? model.ProductId : disc.ProductId;
                    disc.ServiceId = model.ServiceId > 0 ? model.ServiceId : disc.ServiceId;
                    disc.Name = model.Name;
                    disc.Amount = model.Amount != null ? model.Amount : disc.Amount;
                    disc.Percentage = model.Percentage != null ? model.Percentage : disc.Percentage;
                    disc.ExpirationDate = model.ExpirationDate != null ? model.ExpirationDate : disc.ExpirationDate;
                    disc.UpdatedBy = User.Identity.Name;
                    disc.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                string item = model.ProductId != null ? db.Products.SingleOrDefault(p => p.Id == model.ProductId).Name : (model.ServiceId != null ? db.Services.SingleOrDefault(p => p.Id == model.ServiceId).Name : string.Empty);

                TempData["msg"] = string.Format("Discount '{0}({1:C})' was updated successfully!", item, disc.Amount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Discounts", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Discount disc = db.Discounts.SingleOrDefault(s => s.Id == Id);
                string discName = disc.Product != null ? disc.Product.Name : (disc.Service != null ? disc.Service.Name : string.Empty);
                decimal? discAmt = disc.Amount;

                if (disc != null)
                {
                    db.Discounts.Remove(disc);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Discount '{0}({1:C})' was deleted successfully!", discName, discAmt);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Discounts", "home");
        }
    }
}