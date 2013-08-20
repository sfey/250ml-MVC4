﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _250ml_MVC4_2.Models;
using _250ml_MVC4_2.Filters;

namespace _250ml_MVC4_2.Controllers
{
    [InitializeSimpleMembership]
    public class HappeningController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Happening/

        public ActionResult Index()
        {
            db.Happenings.ToList();


           return View(db.Happenings.ToList());
        }

        //
        // GET: /Happening/Details/5

        public ActionResult Details(int id = 0)
        {
            Happening happening = db.Happenings.Find(id);
            if (happening == null)
            {
                return HttpNotFound();
            }
            return View(happening);
        }

        //
        // GET: /Happening/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Happening/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Happening happening)
        {
            if (ModelState.IsValid)
            {
                db.Happenings.Add(happening);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(happening);
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddRating(Rating rating)
        {
            if (ModelState.IsValid) {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = rating.HappeningId });
            }

            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid) {

                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = comment.HappeningId });

               /* TempData["model"] = comment; 
                return RedirectToAction("Create", "Comment", comment); */
            }
            
            
            return RedirectToAction("Create", "Comment", comment);
        }

        //
        // GET: /Happening/Edit/5

        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Happening happening = db.Happenings.Find(id);
            if (happening == null)
            {
                return HttpNotFound();
            }
            return View(happening);
        }

        //
        // POST: /Happening/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Happening happening)
        {
            if (ModelState.IsValid)
            {
                db.Entry(happening).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(happening);
        }

        //
        // GET: /Happening/Delete/5

        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Happening happening = db.Happenings.Find(id);
            if (happening == null)
            {
                return HttpNotFound();
            }
            return View(happening);
        }

        //
        // POST: /Happening/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Happening happening = db.Happenings.Find(id);
            db.Happenings.Remove(happening);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}