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
    /*
     * CRUD-Funktionalität für das Happening-Model
     * * Controller für Happening View
     */
    [InitializeSimpleMembership]
    public class HappeningController : Controller
    {
        private UsersContext db = new UsersContext();

        /*
         *  Gibt eine Liste aller Veranstaltungen zurück (Read)
         *  Aufrufbar für alle Besucher der Webseite
         */ 
        public ActionResult Index()
        {
            var happenings = db.Happenings.Include(h => h.Owner);
            return View(happenings.ToList());
        }

        /*
         * Gibt eine Liste der eigenen Veranstaltungen zurück
         * Abrufbar für die Rolle Verwalter
         */
        [Authorize(Roles = "Verwalter")]
        public ActionResult Own()
        {
            // aktuelle UserId bestimmen
            int CurrentUserId = WebSecurity.CurrentUserId;

            var OwnHappenings = db.Happenings.Where(m => m.UserId == CurrentUserId);
            return View(OwnHappenings.ToList());
        }

        /*
         * Gibt eine Liste aller Veranstaltungen mit einer Mindestbewertung zurück
         * Abrufbar für alle Besucher der Webseite
         */
        public ActionResult ByRating(int id = 0)
        {
            if (id == 0) {
                return RedirectToAction("Index");
            }

            TempData["rating"] = id;
            var happenings = db.Happenings.Where(m => m.Ratings.Sum(x => x.Value) >= id);
            return View(happenings.ToList());
        }

        /*
         * Gibt eine Detail-Ansicht für eine bestimmte Veranstaltung zurück
         */
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

        /*
         * Zeigt das Formular zum anlegen einer neuen Veranstaltung an
         */
        [Authorize(Roles = "Verwalter,Administrator")]
        public ActionResult Create()
        {
            // merken, von wo die Action aufgerufen wurde und entsprechend zurückspringen
            TempData["action"] = ReferrerHelper.ReferrerAction();

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        /*
         * Legt eine neue Veranstaltung an
         */
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

        /*
         * Zeigt das Formular zum Bearbeiten einer Veranstaltung an
         */
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

        /*
         * Speichert die Änderungen an einern Veranstaltung
         */
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

        /*
         * Zeigt das Formular zum löschen einer Veranstaltung an
         */
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

        /* 
         * Löscht eine Veranstaltung
         */
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
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