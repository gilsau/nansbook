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
    public class SaleController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(SaleModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Sale sale = new Sale();
                sale.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                sale.ProductId = model.ProductId != null ? model.ProductId : sale.ProductId;
                sale.ServiceId = model.ServiceId != null ? model.ServiceId : sale.ServiceId;
                sale.SalePaymentMethodId = model.SalePaymentMethodId;
                sale.SaleAmount = model.SaleAmount;
                sale.TipPaymentMethodId = model.TipPaymentMethodId != null ? model.TipPaymentMethodId : sale.TipPaymentMethodId;
                sale.TipAmount = model.TipAmount != null ? model.TipAmount : sale.TipAmount;
                sale.CreatedBy = User.Identity.Name;
                sale.CreatedDate = DateTime.Now;
                sale.UpdatedBy = User.Identity.Name;
                sale.UpdatedDate = DateTime.Now;

                db.Sales.Add(sale);
                db.SaveChanges();

                string itemSold = model.ProductId != null ? db.Products.SingleOrDefault(p => p.Id == model.ProductId).Name : (model.ServiceId != null ? db.Services.SingleOrDefault(p => p.Id == model.ServiceId).Name : string.Empty);

                TempData["msg"] = string.Format("Sale '{0}({1:C})' was added successfully!", itemSold, sale.SaleAmount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Sales", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(SaleModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Sale sale = db.Sales.SingleOrDefault(s => s.Id == model.Id);

                if (sale != null)
                {
                    sale.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    sale.ProductId = model.ProductId != null ? model.ProductId : sale.ProductId;
                    sale.ServiceId = model.ServiceId != null ? model.ServiceId : sale.ServiceId;
                    sale.SalePaymentMethodId = model.SalePaymentMethodId;
                    sale.SaleAmount = model.SaleAmount;
                    sale.TipPaymentMethodId = model.TipPaymentMethodId != null ? model.TipPaymentMethodId : sale.TipPaymentMethodId;
                    sale.TipAmount = model.TipAmount != null ? model.TipAmount : sale.TipAmount;
                    sale.UpdatedBy = User.Identity.Name;
                    sale.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                string itemSold = model.ProductId != null ? db.Products.SingleOrDefault(p => p.Id == model.ProductId).Name : (model.ServiceId != null ? db.Services.SingleOrDefault(p => p.Id == model.ServiceId).Name : string.Empty);

                TempData["msg"] = string.Format("Sale '{0}({1:C})' was updated successfully!", itemSold, sale.SaleAmount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Sales", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Sale sale = db.Sales.SingleOrDefault(s => s.Id == Id);
                string itemSold = sale.Product != null ? sale.Product.Name : (sale.Service != null ? sale.Service.Name : string.Empty);

                string saleName = sale.SaleAmount.ToString();

                if (sale != null)
                {
                    db.Sales.Remove(sale);
                    db.SaveChanges();
                }
                
                TempData["msg"] = string.Format("Sale '{0}({1:C})' was added successfully!", itemSold, sale.SaleAmount);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Sales", "home");
        }
    }
}