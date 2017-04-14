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
    public class UserController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(UserModel model)
        {
            return RedirectToAction("Users", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(UserModel model)
        {
            return RedirectToAction("Users", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            return RedirectToAction("Users", "home");
        }
    }
}