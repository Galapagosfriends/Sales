
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers
{
      [Authorize(Roles = "Admin,Manager")]
    public class PRODUCT_CALENDERController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PRODUCT_CALENDER
        //public ActionResult Index()
        //{
        //    var pRODUCT_CALENDER = db.PRODUCT_CALENDER.Include(p => p.CATEGORY_PRODUCT);
        //    return View(pRODUCT_CALENDER.ToList());
        //}

        public ActionResult Index(int? id, int? month)
        {
            month = month == null || month == 0 ? DateTime.Now.Month : month;

            if (id == null && ViewBag.ParentCategoyProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Where(pd =>
            (pd.CategoryProductId == id) && (pd.Month == month) && (pd.Year == DateTime.Now.Year));
            ViewBag.ParentCategoyProduct = id;
            if (tBL_PRODUCTCALENDER == null)
            {
                return HttpNotFound();
            }

            return View(tBL_PRODUCTCALENDER.ToList());
        }


        // GET: PRODUCT_CALENDER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDER pRODUCT_CALENDER = db.PRODUCT_CALENDER.Find(id);
            if (pRODUCT_CALENDER == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_CALENDER);
        }

        // GET: PRODUCT_CALENDER/Create
         [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name");
            return View();
        }

        // POST: PRODUCT_CALENDER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,CategoryProductId,Day,Year,MaxNumber,TotalBooks,Lock,Month,Date")] PRODUCT_CALENDER pRODUCT_CALENDER)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT_CALENDER.Add(pRODUCT_CALENDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", pRODUCT_CALENDER.CategoryProductId);
            return View(pRODUCT_CALENDER);
        }

        // GET: PRODUCT_CALENDER/Edit/5
  //TODO       [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDER pRODUCT_CALENDER = db.PRODUCT_CALENDER.Find(id);
            if (pRODUCT_CALENDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", pRODUCT_CALENDER.CategoryProductId);
            return View(pRODUCT_CALENDER);
        }

        // POST: PRODUCT_CALENDER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,CategoryProductId,Day,Year,MaxNumber,TotalBooks,Lock,Month,Date")] PRODUCT_CALENDER pRODUCT_CALENDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT_CALENDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", pRODUCT_CALENDER.CategoryProductId);
            return View(pRODUCT_CALENDER);
        }

        // GET: PRODUCT_CALENDER/Delete/5
         [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDER pRODUCT_CALENDER = db.PRODUCT_CALENDER.Find(id);
            if (pRODUCT_CALENDER == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_CALENDER);
        }

        // POST: PRODUCT_CALENDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT_CALENDER pRODUCT_CALENDER = db.PRODUCT_CALENDER.Find(id);
            db.PRODUCT_CALENDER.Remove(pRODUCT_CALENDER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


         [Authorize(Roles = "Admin,Manager")]
        public ActionResult Charter(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetCharter(id);
            return RedirectToAction("Index",  new { Id = CategoryProductId, month = month });
        }

         [Authorize(Roles = "Admin,Manager")]
        public ActionResult NoSalir(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetNoSalir(id);
            return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
        }

         [Authorize(Roles = "Admin,Manager")]
        public ActionResult Salir(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetSalir(id);
            return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
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
