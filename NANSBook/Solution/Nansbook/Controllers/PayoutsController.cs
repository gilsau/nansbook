using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nansbook.Models;

namespace Nansbook.Controllers
{
    public class PayoutsController : Controller
    {
        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult CommRates()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Salaries()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Tips()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Paychecks()
        {
            return View();
        }
    }
}
