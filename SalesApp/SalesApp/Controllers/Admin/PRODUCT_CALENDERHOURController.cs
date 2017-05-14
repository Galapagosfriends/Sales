using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
     [Authorize(Roles = "Admin,Manager")]
    public class PRODUCT_CALENDERHOURController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PRODUCT_CALENDERHOUR
        public ActionResult Index()
        {
            var pRODUCT_CALENDERHOUR = db.PRODUCT_CALENDERHOUR.Include(p => p.PRODUCT_CALENDER);
            return View(pRODUCT_CALENDERHOUR.ToList());
        }

        // GET: PRODUCT_CALENDERHOUR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDERHOUR pRODUCT_CALENDERHOUR = db.PRODUCT_CALENDERHOUR.Find(id);
            if (pRODUCT_CALENDERHOUR == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_CALENDERHOUR);
        }

        // GET: PRODUCT_CALENDERHOUR/Create
         [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDER, "Id", "Id");
            return View();
        }

        // POST: PRODUCT_CALENDERHOUR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,ProductCalenderId,MaxNumber,TotalBooks,Lock,Time")] PRODUCT_CALENDERHOUR pRODUCT_CALENDERHOUR)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT_CALENDERHOUR.Add(pRODUCT_CALENDERHOUR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDER, "Id", "Id", pRODUCT_CALENDERHOUR.ProductCalenderId);
            return View(pRODUCT_CALENDERHOUR);
        }

        // GET: PRODUCT_CALENDERHOUR/Edit/5
         [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDERHOUR pRODUCT_CALENDERHOUR = db.PRODUCT_CALENDERHOUR.Find(id);
            if (pRODUCT_CALENDERHOUR == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDER, "Id", "Id", pRODUCT_CALENDERHOUR.ProductCalenderId);
            return View(pRODUCT_CALENDERHOUR);
        }

        // POST: PRODUCT_CALENDERHOUR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,ProductCalenderId,MaxNumber,TotalBooks,Lock,Time")] PRODUCT_CALENDERHOUR pRODUCT_CALENDERHOUR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT_CALENDERHOUR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDER, "Id", "Id", pRODUCT_CALENDERHOUR.ProductCalenderId);
            return View(pRODUCT_CALENDERHOUR);
        }

        // GET: PRODUCT_CALENDERHOUR/Delete/5
         [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDERHOUR pRODUCT_CALENDERHOUR = db.PRODUCT_CALENDERHOUR.Find(id);
            if (pRODUCT_CALENDERHOUR == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_CALENDERHOUR);
        }

        // POST: PRODUCT_CALENDERHOUR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT_CALENDERHOUR pRODUCT_CALENDERHOUR = db.PRODUCT_CALENDERHOUR.Find(id);
            db.PRODUCT_CALENDERHOUR.Remove(pRODUCT_CALENDERHOUR);
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
