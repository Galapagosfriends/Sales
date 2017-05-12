
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers.Admin
{
    [Authorize]
    public class TBL_PRODUCTCALENDERController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        //// GET: PRODUCT_CALENDER
        //public ActionResult Index()
        //{
        //    //var tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Include(t => t.TBL_CATEGORYPRODUCT);
        //    //return View(tBL_PRODUCTCALENDER.ToList());
        //    return View(db.PRODUCT_CALENDER.ToList());
        //}

        // GET: PRODUCT_CALENDER/Details/5
        public ActionResult Index(int? id, int? month)
        {
            month = month == null || month==0? DateTime.Now.Month : month;

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

        // GET: PRODUCT_CALENDER/Create
        public ActionResult Create()
        {
            ViewBag.CategoryProductId = new SelectList(db.TBL_CATEGORYPRODUCT, "Id", "Name");
            return View();
        }

        // POST: PRODUCT_CALENDER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryProductId,Day,Year,MaxNumber,TotalBooks,Lock,Month")] TBL_PRODUCTCALENDER tBL_PRODUCTCALENDER)
        {
            if (ModelState.IsValid)
            {
                db.TBL_PRODUCTCALENDER.Add(tBL_PRODUCTCALENDER);
                db.SaveChanges();
                ViewBag.CategoryProductId = new SelectList(db.TBL_CATEGORYPRODUCT, "Id", "Name", tBL_PRODUCTCALENDER.CategoryProductId);
                ViewBag.ParentCategoyProduct = ViewBag.CategoryProductId.SelectedValue;

                return RedirectToAction("Index", new { Id = ViewBag.ParentCategoyProduct });
            }

            ViewBag.CategoryProductId = new SelectList(db.TBL_CATEGORYPRODUCT, "Id", "Name", tBL_PRODUCTCALENDER.CategoryProductId);


            return View(tBL_PRODUCTCALENDER);
        }

        // GET: PRODUCT_CALENDER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_PRODUCTCALENDER tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Find(id);
            if (tBL_PRODUCTCALENDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryProductId = new SelectList(db.TBL_CATEGORYPRODUCT, "Id", "Name", tBL_PRODUCTCALENDER.CategoryProductId);
            return View(tBL_PRODUCTCALENDER);
        }

        // POST: PRODUCT_CALENDER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryProductId,Day,Year,MaxNumber,TotalBooks,Lock")] TBL_PRODUCTCALENDER tBL_PRODUCTCALENDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_PRODUCTCALENDER).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.CategoryProductId = new SelectList(db.TBL_CATEGORYPRODUCT, "Id", "Name", tBL_PRODUCTCALENDER.CategoryProductId);
                ViewBag.ParentCategoyProduct = ViewBag.CategoryProductId.SelectedValue;
                return RedirectToAction("Index", new { Id = ViewBag.ParentCategoyProduct });
                //return RedirectToAction("Index");
            }
            ViewBag.CategoryProductId = new SelectList(db.TBL_CATEGORYPRODUCT, "Id", "Name", tBL_PRODUCTCALENDER.CategoryProductId);

            return View(tBL_PRODUCTCALENDER);
        }

        // GET: PRODUCT_CALENDER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_PRODUCTCALENDER tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Find(id);
            if (tBL_PRODUCTCALENDER == null)
            {
                return HttpNotFound();
            }
            return View(tBL_PRODUCTCALENDER);
        }

        // POST: PRODUCT_CALENDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_PRODUCTCALENDER tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Find(id);
            db.PRODUCT_CALENDER.Remove(tBL_PRODUCTCALENDER);
            db.SaveChanges();
            //  return RedirectToAction("Index");

            return RedirectToAction("Index", new { Id = ViewBag.ParentCategoyProduct });

        }

        public ActionResult UpdateProduct(int prodID, int catID)
        {
            return View();
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
