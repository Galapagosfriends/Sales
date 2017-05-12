using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers.Admin
{
    [Authorize]
    public class TBL_CATEGORYPRODUCTController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: TBL_CATEGORYPRODUCT
        public ActionResult Index(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tBL_CATEGORYPRODUCT = db.TBL_CATEGORYPRODUCT.Include(t => t.TBL_CATEGORY).Where(cp => cp.CategoryId == Id);
            if (tBL_CATEGORYPRODUCT == null)
            {
                return HttpNotFound();
            }

            ViewBag.ParentCategoy = Id;
            return View(tBL_CATEGORYPRODUCT.ToList());
        }

        // GET: TBL_CATEGORYPRODUCT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CATEGORYPRODUCT tBL_CATEGORYPRODUCT = db.TBL_CATEGORYPRODUCT.Find(id);
            if (tBL_CATEGORYPRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CATEGORYPRODUCT);
        }

        [Authorize(Roles ="Admin")]
        // GET: TBL_CATEGORYPRODUCT/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.TBL_CATEGORY, "CA_CATEGORY_ID", "CA_CATEGORYNAME");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: TBL_CATEGORYPRODUCT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId,Lock")] TBL_CATEGORYPRODUCT tBL_CATEGORYPRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.TBL_CATEGORYPRODUCT.Add(tBL_CATEGORYPRODUCT);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", new { Id = ViewBag.ParentCategoy });
            }

            ViewBag.CategoryId = new SelectList(db.TBL_CATEGORY, "CA_CATEGORY_ID", "CA_CATEGORYNAME", tBL_CATEGORYPRODUCT.CategoryId);
            return View(tBL_CATEGORYPRODUCT);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: TBL_CATEGORYPRODUCT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CATEGORYPRODUCT tBL_CATEGORYPRODUCT = db.TBL_CATEGORYPRODUCT.Find(id);
            if (tBL_CATEGORYPRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.TBL_CATEGORY, "CA_CATEGORY_ID", "CA_CATEGORYNAME", tBL_CATEGORYPRODUCT.CategoryId);
            return View(tBL_CATEGORYPRODUCT);
        }


        [Authorize(Roles = "Admin,Manager")]
        // POST: TBL_CATEGORYPRODUCT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,Lock")] TBL_CATEGORYPRODUCT tBL_CATEGORYPRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_CATEGORYPRODUCT).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
              //  return RedirectToAction("Index");
                return RedirectToAction("Index", new { Id = ViewBag.ParentCategoy });
            }
            ViewBag.CategoryId = new SelectList(db.TBL_CATEGORY, "CA_CATEGORY_ID", "CA_CATEGORYNAME", tBL_CATEGORYPRODUCT.CategoryId);
            return View(tBL_CATEGORYPRODUCT);
        }

        // GET: TBL_CATEGORYPRODUCT/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CATEGORYPRODUCT tBL_CATEGORYPRODUCT = db.TBL_CATEGORYPRODUCT.Find(id);
            if (tBL_CATEGORYPRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CATEGORYPRODUCT);
        }

        // POST: TBL_CATEGORYPRODUCT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_CATEGORYPRODUCT tBL_CATEGORYPRODUCT = db.TBL_CATEGORYPRODUCT.Find(id);
            db.TBL_CATEGORYPRODUCT.Remove(tBL_CATEGORYPRODUCT);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Index", new { Id = ViewBag.ParentCategoy });
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
