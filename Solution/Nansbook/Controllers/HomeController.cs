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

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Services()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<Service> list = db.Services.Where(s => s.UserXStore.Id == userStoreId);
            ViewBag.ParentServices = db.Services.Where(e => e.ParentId == null);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Products()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<Product> list = db.Products.Where(s => s.UserXStore.Id == userStoreId);
            ViewBag.ParentProducts = db.Products.Where(e => e.ParentId == null);

            return View(list);
        }
        
        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult GiftCards()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<GiftCard> list = db.GiftCards.Where(s => s.UserXStore.Id == userStoreId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Discounts()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<Discount> list = db.Discounts.Where(s => s.UserXStore.Id == userStoreId);
            ViewBag.ParentDiscounts = db.Discounts.Where(e => e.ParentId == null);

            ViewBag.Products = db.Products.Where(p => p.UserXStore.Id == userStoreId);
            ViewBag.Services = db.Services.Where(s => s.UserXStore.Id == userStoreId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult PaymentMethods()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<PaymentMethod> list = db.PaymentMethods.Where(s => s.UserXStore.Id == userStoreId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult ExpenseCategories()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<ExpenseCategory> list = db.ExpenseCategories.Where(s => s.UserXStore.Id == userStoreId);
            ViewBag.ParentExpenseCategories = db.ExpenseCategories.Where(e => e.ParentId == null);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult CommissionRates()
        {
            int userId = ((UserProfile)Session["User"]).UserId;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<UserXStore> list = db.UserXStores.Where(u => u.UserId == userId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Salaries()
        {
            int userId = ((UserProfile)Session["User"]).UserId;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<UserXStore> list = db.UserXStores.Where(u => u.UserId == userId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Deductions()
        {
            int userId = ((UserProfile)Session["User"]).UserId;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<UserXStore> list = db.UserXStores.Where(u => u.UserId == userId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Users()
        {
            int? userId = ((UserProfile)Session["User"]).UserId;
            int? storeId = Helpers.UserProvider.GetCurrentUserStore().StoreId;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<UserProfile> list = db.UserXStores.Where(us => us.StoreId == storeId).Select(us => us.UserProfile).Distinct<UserProfile>();

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult AppSettings()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<AppSetting> list = db.AppSettings.Where(s => s.UserXStore.Id == userStoreId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Sales()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<Sale> list = db.Sales.Where(s => s.UserXStore.Id == userStoreId);

            return View(list);
        }

        [Authorize]
        [HttpGet]
        [SessionCheckAttribute]
        public ActionResult Expenses()
        {
            int userStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;

            NansbookEntities db = new NansbookEntities();
            IEnumerable<Expense> list = db.Expenses.Where(s => s.UserXStore.Id == userStoreId);

            return View(list);
        }

        [HttpGet]
        public string SwitchStore(int storeId) {
            Helpers.UserProvider.SetCurrentStore(storeId);
            return Helpers.UserProvider.GetCurrentUserStore().Store.Name;
        }
    }
}
