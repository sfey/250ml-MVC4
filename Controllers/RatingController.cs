using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _250ml_MVC4_2.Models;
using _250ml_MVC4_2.Helpers;
using _250ml_MVC4_2.Filters;
using WebMatrix.WebData;

namespace _250ml_MVC4_2.Controllers
{
    [InitializeSimpleMembership]
    public class RatingController : Controller
    {
        private UsersContext db = new UsersContext();

        /*
         * Speichert eine Bewertung zu einer Veranstaltung.
         * Wird vom Happening-Controller aufgerufen, wenn ein
         * Benutzer eine Veranstaltung bewertet.
         */
        [HttpPost]
        [Authorize]
        public ActionResult Create(Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction(ReferrerHelper.ReferrerAction(), ReferrerHelper.ReferrerController(), new { Id = ReferrerHelper.ReferrerId() });
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", rating.UserId);
            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", rating.HappeningId);
            return View(rating);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}