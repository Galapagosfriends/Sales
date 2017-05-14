
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
    public class ADRESSController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: ADRESSes
        public ActionResult Index()
        {
            var aDRESS = db.ADRESS.Include(a => a.COUNTRY);
            return View(aDRESS.ToList().OrderByDescending(o => o.Id));
        }

        // GET: ADRESSes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADRESS aDRESS = db.ADRESS.Find(id);
            if (aDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDRESS);
        }

        // GET: ADRESSes/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.COUNTRY, "Id", "continent");
            return View();
        }

        // POST: ADRESSes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Street,Post,City,Email,Cel,Tel,CountryID")] ADRESS aDRESS)
        {
            if (ModelState.IsValid)
            {
                db.ADRESS.Add(aDRESS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.COUNTRY, "Id", "continent", aDRESS.CountryID);
            return View(aDRESS);
        }

        // GET: ADRESSes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADRESS aDRESS = db.ADRESS.Find(id);
            if (aDRESS == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.COUNTRY, "Id", "continent", aDRESS.CountryID);
            return View(aDRESS);
        }

        // POST: ADRESSes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Street,Post,City,Email,Cel,Tel,CountryID")] ADRESS aDRESS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDRESS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.COUNTRY, "Id", "continent", aDRESS.CountryID);
            return View(aDRESS);
        }

        // GET: ADRESSes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADRESS aDRESS = db.ADRESS.Find(id);
            if (aDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDRESS);
        }

        // POST: ADRESSes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ADRESS aDRESS = db.ADRESS.Find(id);
            db.ADRESS.Remove(aDRESS);
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
