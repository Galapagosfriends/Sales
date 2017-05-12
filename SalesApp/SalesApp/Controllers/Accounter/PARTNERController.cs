
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers.Accounter
{
    public class PARTNERController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PARTNER
        public ActionResult Index()
        {
            return View(db.PARTNER.ToList());
        }

        // GET: PARTNER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTNER pARTNER = db.PARTNER.Find(id);
            if (pARTNER == null)
            {
                return HttpNotFound();
            }
            return View(pARTNER);
        }

        // GET: PARTNER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PARTNER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,TourID,AdresseID,PriceID")] PARTNER pARTNER)
        {
            if (ModelState.IsValid)
            {
                db.PARTNER.Add(pARTNER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pARTNER);
        }

        // GET: PARTNER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTNER pARTNER = db.PARTNER.Find(id);
            if (pARTNER == null)
            {
                return HttpNotFound();
            }
            return View(pARTNER);
        }

        // POST: PARTNER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,TourID,AdresseID,PriceID")] PARTNER pARTNER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARTNER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pARTNER);
        }

        // GET: PARTNER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTNER pARTNER = db.PARTNER.Find(id);
            if (pARTNER == null)
            {
                return HttpNotFound();
            }
            return View(pARTNER);
        }

        // POST: PARTNER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PARTNER pARTNER = db.PARTNER.Find(id);
            db.PARTNER.Remove(pARTNER);
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
