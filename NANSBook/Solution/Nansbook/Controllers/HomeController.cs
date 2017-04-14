using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.Data;
using WebMatrix.WebData;
using Nansbook.Models;

namespace Nansbook.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Index()
        {
            return View();
        }

    }
}
