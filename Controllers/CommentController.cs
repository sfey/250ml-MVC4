﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _250ml_MVC4_2.Models;
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

        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.UserProfile).Include(c => c.Happening);
            return View(comments.ToList());
        }

        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");

            ViewBag.UserId = WebSecurity.CurrentUserId;

            

            if (ViewBag.UserId < 1) { ViewBag.UserId = 1; } // wenn nicht eingeloggt = -1; TO DO add userId1=Gast o.ä.
            //new SelectList(db.UserProfiles, "UserId", "UserName");


            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name");
            return View();
        }



        //
        // POST: /Comment/Create

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", comment.UserId);
            ViewBag.UserId = WebSecurity.CurrentUserId;
            if (ViewBag.UserId < 1) { ViewBag.UserId = 1; } // wenn nicht eingeloggt = -1; TO DO add userId1=Gast o.ä.

            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", comment.HappeningId);
            return View(comment);
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = comment.UserId; // new SelectList(db.UserProfiles, "UserId", "UserName", comment.UserId);
            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", comment.HappeningId);
            return View(comment);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = comment.UserId; // new SelectList(db.UserProfiles, "UserId", "UserName", comment.UserId);
            ViewBag.HappeningId = new SelectList(db.Happenings, "HappeningId", "Name", comment.HappeningId);
            return View(comment);
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id = 0)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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