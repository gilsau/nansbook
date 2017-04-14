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
    public class AppSettingController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(AppSettingModel model)
        {
            return RedirectToAction("AppSettings", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(AppSettingModel model)
        {
            return RedirectToAction("AppSettings", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            return RedirectToAction("AppSettings", "home");
        }
    }
}