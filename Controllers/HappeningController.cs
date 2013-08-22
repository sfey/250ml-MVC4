using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using _250ml_MVC4_2.Models;
using WebMatrix.WebData;
using _250ml_MVC4_2.Filters;
using _250ml_MVC4_2.Helpers;

namespace _250ml_MVC4_2.Controllers
{
    [InitializeSimpleMembership]
    public class HappeningController : Controller
    {
        private UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            var happenings = db.Happenings.Include(h => h.Owner);
            return View(happenings.ToList());
        }

        [Authorize(Roles = "Verwalter")]
        public ActionResult Own()
        {
            // aktuelle UserId bestimmen
            int CurrentUserId = WebSecurity.CurrentUserId;

            var OwnHappenings = db.Happenings.Where(m => m.UserId == CurrentUserId);
            return View(OwnHappenings.ToList());
        }

        public ActionResult ByRating(int id = 0)
        {
            if (id == 0) {
                return RedirectToAction("Index");
            }

            TempData["rating"] = id;
            var happenings = db.Happenings.Where(m => m.Ratings.Sum(x => x.Value) >= id);
            return View(happenings.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Happening happening = db.Happenings.Find(id);
            if (happening == null)
            {
                TempData["error"] = "Es existiert keine Veranstaltung mit dieser Id!";
                return RedirectToAction("Index");
            }
            return View(happening);
        }

        [Authorize(Roles = "Verwalter,Administrator")]
        public ActionResult Create()
        {
            // merken, von wo die Action aufgerufen wurde und entsprechend zurückspringen
            TempData["action"] = ReferrerHelper.ReferrerAction();

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Verwalter,Administrator")]
        public ActionResult Create(Happening happening)
        {
            if (ModelState.IsValid)
            {
                db.Happenings.Add(happening);
                db.SaveChanges();
                return RedirectToAction((string)TempData["action"]);
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", happening.UserId);
            return View(happening);
        }

        [Authorize(Roles = "Verwalter,Administrator")]
        public ActionResult Edit(int id = 0)
        {
            Happening happening = db.Happenings.Find(id);

            // wenn der User nicht Owner ist Fehlermeldung anzeigen
            // und Zugriff verweigern
            if(!User.IsInRole("Administrator")) {
                if (!happening.IsOwner(WebSecurity.CurrentUserId))
                {
                    TempData["error"] = "Sie sind nicht berechtigt diese Veranstaltung zu bearbeiten!";
                    return RedirectToAction("Index");
                }
            }

            // falls keine Veranstaltung mit dieser Id existiert 
            // Fehler anzeigen
            if (happening == null)
            {
                TempData["error"] = "Es existiert keine Veranstaltung mit dieser Id!";
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", happening.UserId);
            return View(happening);
        }

        [HttpPost]
        [Authorize(Roles = "Verwalter,Administrator")]
        public ActionResult Edit(Happening happening)
        {
            // wenn der User nicht Owner ist Fehlermeldung anzeigen
            // und Zugriff verweigern
            if (!User.IsInRole("Administrator"))
            {
                if (!happening.IsOwner(WebSecurity.CurrentUserId))
                {
                    TempData["error"] = "Sie sind nicht berechtigt diese Veranstaltung zu bearbeiten!";
                    return RedirectToAction("Index");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(happening).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", happening.UserId);
            return View(happening);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Happening happening = db.Happenings.Find(id);

            if (happening == null)
            {
                TempData["error"] = "Es existiert keine Veranstaltung mit dieser Id!";
                return RedirectToAction("Index");
            }
            return View(happening);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Happening happening = db.Happenings.Find(id);
            db.Happenings.Remove(happening);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[Authorize]
        //public ActionResult Rate(Rating rating)
        //{
        //    db.Ratings.Add(rating);
        //    db.SaveChanges();
        //    return RedirectToAction("Details", new { id = rating.HappeningId });
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Commentate(Comment comment)
        //{
        //    db.Comments.Add(comment);
        //    db.SaveChanges();
        //    return RedirectToAction("Details", new { id = comment.HappeningId });
        //}
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}