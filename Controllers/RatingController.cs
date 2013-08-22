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

        //
        // GET: /Rating/

        //public ActionResult Index()
        //{
        //    var ratings = db.Ratings.Include(r => r.UserProfile).Include(r => r.Happening);
        //    return View(ratings.ToList());
        //}

        ////
        //// GET: /Rating/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Rating rating = db.Ratings.Find(id);
        //    if (rating == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rating);
        //}

        ////
        //// GET: /Rating/Create

        //public ActionResult Create()
        //{
        //    ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
        //    ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name");
        //    return View();
        //}

        //
        // POST: /Rating/Create

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

        //
        // GET: /Rating/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Rating rating = db.Ratings.Find(id);
        //    if (rating == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", rating.UserId);
        //    ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", rating.HappeningId);
        //    return View(rating);
        //}

        ////
        //// POST: /Rating/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Rating rating)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(rating).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", rating.UserId);
        //    ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", rating.HappeningId);
        //    return View(rating);
        //}

        ////
        //// GET: /Rating/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Rating rating = db.Ratings.Find(id);
        //    if (rating == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rating);
        //}

        ////
        //// POST: /Rating/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Rating rating = db.Ratings.Find(id);
        //    db.Ratings.Remove(rating);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}