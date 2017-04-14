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
    public class PaymentMethodController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(PaymentMethodModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                PaymentMethod pay = new PaymentMethod();
                pay.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                pay.Name = model.Name;
                pay.CreatedBy = User.Identity.Name;
                pay.CreatedDate = DateTime.Now;
                pay.UpdatedBy = User.Identity.Name;
                pay.UpdatedDate = DateTime.Now;

                db.PaymentMethods.Add(pay);
                db.SaveChanges();

                TempData["msg"] = string.Format("Payment Method '{0}' was added successfully!", pay.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("PaymentMethods", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(PaymentMethodModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                PaymentMethod pay = db.PaymentMethods.SingleOrDefault(s => s.Id == model.Id);

                if (pay != null)
                {
                    pay.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    pay.Name = model.Name;
                    pay.UpdatedBy = User.Identity.Name;
                    pay.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Payment Method '{0}' was updated successfully!", pay.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("PaymentMethods", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                PaymentMethod pay = db.PaymentMethods.SingleOrDefault(s => s.Id == Id);
                string payName = pay.Name;

                if (pay != null)
                {
                    db.PaymentMethods.Remove(pay);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Payment Method '{0}' was deleted successfully!", payName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("PaymentMethods", "home");
        }
    }
}