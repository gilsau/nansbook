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
    public class ServiceController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(ServiceModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Service svc = new Service();
                svc.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                svc.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : svc.ParentId;
                svc.Name = model.Name;
                svc.Price = model.Price != null ? model.Price : svc.Price;
                svc.CreatedBy = User.Identity.Name;
                svc.CreatedDate = DateTime.Now;
                svc.UpdatedBy = User.Identity.Name;
                svc.UpdatedDate = DateTime.Now;

                db.Services.Add(svc);
                db.SaveChanges();

                TempData["msg"] = string.Format("Service '{0}' was added successfully!", svc.Name);
            }
            catch (Exception ex) {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("services", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(ServiceModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Service svc = db.Services.SingleOrDefault(s => s.Id == model.Id);

                if (svc != null)
                {
                    svc.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    svc.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : svc.ParentId;
                    svc.ParentId = model.ParentId > 0 ? model.ParentId : model.ParentId == 0 ? null : svc.ParentId;
                    svc.Name = model.Name;
                    svc.Price = model.Price != null ? model.Price : svc.Price;
                    svc.UpdatedBy = User.Identity.Name;
                    svc.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Service '{0}' was updated successfully!", svc.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("services", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                Service svc = db.Services.SingleOrDefault(s => s.Id == Id);
                string svcName = svc.Name;
                
                if (svc != null)
                {
                    db.Services.Remove(svc);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Service '{0}' was deleted successfully!", svcName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("services", "home");
        }
    }
}