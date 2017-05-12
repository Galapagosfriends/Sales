
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers
{
    public class COUNTRYController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: COUNTRY
        public ActionResult Index()
        {
            return View(db.COUNTRY.ToList());
        }

        // GET: COUNTRY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COUNTRY cOUNTRY = db.COUNTRY.Find(id);
            if (cOUNTRY == null)
            {
                return HttpNotFound();
            }
            return View(cOUNTRY);
        }

        // GET: COUNTRY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: COUNTRY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,continent,Name,capital,iso2,iso3,ioc,tld,currency,phone,Lang_Id")] COUNTRY cOUNTRY)
        {
            if (ModelState.IsValid)
            {
                db.COUNTRY.Add(cOUNTRY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOUNTRY);
        }

        // GET: COUNTRY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COUNTRY cOUNTRY = db.COUNTRY.Find(id);
            if (cOUNTRY == null)
            {
                return HttpNotFound();
            }
            return View(cOUNTRY);
        }

        // POST: COUNTRY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,continent,Name,capital,iso2,iso3,ioc,tld,currency,phone,Lang_Id")] COUNTRY cOUNTRY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOUNTRY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOUNTRY);
        }

        // GET: COUNTRY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COUNTRY cOUNTRY = db.COUNTRY.Find(id);
            if (cOUNTRY == null)
            {
                return HttpNotFound();
            }
            return View(cOUNTRY);
        }

        // POST: COUNTRY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COUNTRY cOUNTRY = db.COUNTRY.Find(id);
            db.COUNTRY.Remove(cOUNTRY);
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
