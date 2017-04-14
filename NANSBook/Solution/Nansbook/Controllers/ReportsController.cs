using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nansbook.Models;

namespace Nansbook.Controllers
{
    public class ReportsController : Controller
    {
        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Index()
        {
            return View();
        }

    }
}
