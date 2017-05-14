
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
     [Authorize(Roles = "Admin,Manager")]
    public class PROVIDERController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PROVIDER
        public ActionResult Index()
        {
            return View(db.PROVIDER.ToList().OrderByDescending(o => o.Name).OrderBy(o => o.Name));
        }

        // GET: PROVIDER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVIDER pROVIDER = db.PROVIDER.Find(id);
            if (pROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(pROVIDER);
        }

        // GET: PROVIDER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PROVIDER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PROVIDER pROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.PROVIDER.Add(pROVIDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pROVIDER);
        }

        // GET: PROVIDER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVIDER pROVIDER = db.PROVIDER.Find(id);
            if (pROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(pROVIDER);
        }

        // POST: PROVIDER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PROVIDER pROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROVIDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROVIDER);
        }

        // GET: PROVIDER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVIDER pROVIDER = db.PROVIDER.Find(id);
            if (pROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(pROVIDER);
        }

        // POST: PROVIDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROVIDER pROVIDER = db.PROVIDER.Find(id);
            db.PROVIDER.Remove(pROVIDER);
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
