
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
    public class AGENCYController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: AGENCies
        public ActionResult Index()
        {
            var aGENCY = db.AGENCY.Include(a => a.ADRESS);
            return View(aGENCY.ToList().OrderByDescending(o => o.Name));
        }

        // GET: AGENCies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENCY aGENCY = db.AGENCY.Find(id);
            if (aGENCY == null)
            {
                return HttpNotFound();
            }
            return View(aGENCY);
        }

        // GET: AGENCies/Create
        public ActionResult Create()
        {
            ViewBag.AdresseId = new SelectList(db.ADRESS, "Id", "Street");
            return View();
        }

        // POST: AGENCies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AdresseId")] AGENCY aGENCY)
        {
            if (ModelState.IsValid)
            {
                db.AGENCY.Add(aGENCY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdresseId = new SelectList(db.ADRESS, "Id", "Street", aGENCY.AdresseId);
            return View(aGENCY);
        }

        // GET: AGENCies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENCY aGENCY = db.AGENCY.Find(id);
            if (aGENCY == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdresseId = new SelectList(db.ADRESS, "Id", "Street", aGENCY.AdresseId);
            return View(aGENCY);
        }

        // POST: AGENCies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AdresseId")] AGENCY aGENCY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aGENCY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdresseId = new SelectList(db.ADRESS, "Id", "Street", aGENCY.AdresseId);
            return View(aGENCY);
        }

        // GET: AGENCies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENCY aGENCY = db.AGENCY.Find(id);
            if (aGENCY == null)
            {
                return HttpNotFound();
            }
            return View(aGENCY);
        }

        // POST: AGENCies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AGENCY aGENCY = db.AGENCY.Find(id);
            db.AGENCY.Remove(aGENCY);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
