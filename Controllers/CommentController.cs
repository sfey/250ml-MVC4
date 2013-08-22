using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _250ml_MVC4_2.Models;
using _250ml_MVC4_2.Helpers;
using System.Web.Routing;
using WebMatrix.WebData;
using _250ml_MVC4_2.Filters;

namespace _250ml_MVC4_2.Controllers
{
    [InitializeSimpleMembership]
    public class CommentController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Comment/

        //public ActionResult Index()
        //{
        //    var comments = db.Comments.Include(c => c.UserProfile).Include(c => c.Happening);
        //    return View(comments.ToList());
        //}

        //
        // GET: /Comment/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Comment comment = db.Comments.Find(id);
        //    if (comment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comment);
        //}

        //
        // GET: /Comment/Create

        //public ActionResult Create()
        //{
        //    ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
        //    ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name");
        //    return View();
        //}

        //
        // POST: /Comment/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction(ReferrerHelper.ReferrerAction(), ReferrerHelper.ReferrerController(), new { Id = ReferrerHelper.ReferrerId() });
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", comment.UserId);
            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", comment.HappeningId);
            return View(comment);
        }

        //
        // GET: /Comment/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            
            // merken woher wir gekommen sind um dahin zurück zu kehren
            TempData["action"] = ReferrerHelper.ReferrerAction();
            TempData["controller"] = ReferrerHelper.ReferrerController();
            TempData["id"] = ReferrerHelper.ReferrerId();

            if( !User.IsInRole("Administrator")) {
                if (!comment.IsOwner(WebSecurity.CurrentUserId)) {
                    TempData["error"] = "Sie sind nicht berechtigt diese Veranstaltung zu bearbeiten!";
                    return RedirectToAction(ReferrerHelper.ReferrerAction(), ReferrerHelper.ReferrerController(), new { Id = ReferrerHelper.ReferrerId() });
                }
            }
           
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", comment.UserId);
            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", comment.HappeningId);
            return View(comment);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Comment comment)
        {
            if (!User.IsInRole("Administrator"))
            {
                if (!comment.IsOwner(WebSecurity.CurrentUserId))
                {
                    TempData["error"] = "Sie sind nicht berechtigt diese Veranstaltung zu bearbeiten!";
                    return RedirectToAction(ReferrerHelper.ReferrerAction(), ReferrerHelper.ReferrerController(), new { Id = ReferrerHelper.ReferrerId() });
                }
            }


            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();

                var ReferrerAction = TempData["action"] as string;
                var ReferrerController = TempData["controller"] as string;
                var ReferrerId = Convert.ToInt32(TempData["id"]);

                return RedirectToAction(ReferrerAction, ReferrerController, new { Id = ReferrerId });
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", comment.UserId);
            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", comment.HappeningId);
            return View(comment);
        }

        //
        // GET: /Comment/Delete/5
        [Authorize(Roles="Administrator")]
        public ActionResult Delete(int id = 0)
        {
            // merken woher wir gekommen sind um dahin zurück zu kehren
            TempData["action"] = ReferrerHelper.ReferrerAction();
            TempData["controller"] = ReferrerHelper.ReferrerController();
            TempData["id"] = ReferrerHelper.ReferrerId();

            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();

            var ReferrerAction = TempData["action"] as string;
            var ReferrerController = TempData["controller"] as string;
            var ReferrerId = Convert.ToInt32(TempData["id"]);

            return RedirectToAction(ReferrerAction, ReferrerController, new { Id = ReferrerId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}