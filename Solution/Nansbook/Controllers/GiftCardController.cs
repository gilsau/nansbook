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
    public class GiftCardController : Controller
    {
        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Add(GiftCardModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                GiftCard card = new GiftCard();
                card.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                card.Name = model.Name;
                card.CreditAmount = model.CreditAmount;
                card.ExpirationDate = model.ExpirationDate != null ? model.ExpirationDate : card.ExpirationDate;
                card.CreatedBy = User.Identity.Name;
                card.CreatedDate = DateTime.Now;
                card.UpdatedBy = User.Identity.Name;
                card.UpdatedDate = DateTime.Now;

                db.GiftCards.Add(card);
                db.SaveChanges();

                TempData["msg"] = string.Format("Gift Card '{0}' was added successfully!", card.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("GiftCards", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Update(GiftCardModel model)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                GiftCard card = db.GiftCards.SingleOrDefault(s => s.Id == model.Id);

                if (card != null)
                {
                    card.UserXStoreId = Helpers.UserProvider.GetCurrentUserStore().Id;
                    card.Name = model.Name;
                    card.CreditAmount = model.CreditAmount;
                    card.ExpirationDate = model.ExpirationDate != null ? model.ExpirationDate : card.ExpirationDate;
                    card.UpdatedBy = User.Identity.Name;
                    card.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Gift Card '{0}' was updated successfully!", card.Name);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("GiftCards", "home");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Delete(int Id)
        {
            try
            {
                NansbookEntities db = new NansbookEntities();
                GiftCard card = db.GiftCards.SingleOrDefault(s => s.Id == Id);
                string cardName = card.Name;

                if (card != null)
                {
                    db.GiftCards.Remove(card);
                    db.SaveChanges();
                }

                TempData["msg"] = string.Format("Gift Card '{0}' was deleted successfully!", cardName);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("GiftCards", "home");
        }
    }
}