
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers
{
    public class PAYMENT_TYPEController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PAYMENT_TYPE
        public ActionResult Index()
        {
            return View(db.PAYMENT_TYPE.ToList());
        }

        // GET: PAYMENT_TYPE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_TYPE pAYMENT_TYPE = db.PAYMENT_TYPE.Find(id);
            if (pAYMENT_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_TYPE);
        }

        // GET: PAYMENT_TYPE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PAYMENT_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] PAYMENT_TYPE pAYMENT_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.PAYMENT_TYPE.Add(pAYMENT_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pAYMENT_TYPE);
        }

        // GET: PAYMENT_TYPE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_TYPE pAYMENT_TYPE = db.PAYMENT_TYPE.Find(id);
            if (pAYMENT_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_TYPE);
        }

        // POST: PAYMENT_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] PAYMENT_TYPE pAYMENT_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAYMENT_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pAYMENT_TYPE);
        }

        // GET: PAYMENT_TYPE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_TYPE pAYMENT_TYPE = db.PAYMENT_TYPE.Find(id);
            if (pAYMENT_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_TYPE);
        }

        // POST: PAYMENT_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAYMENT_TYPE pAYMENT_TYPE = db.PAYMENT_TYPE.Find(id);
            db.PAYMENT_TYPE.Remove(pAYMENT_TYPE);
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
