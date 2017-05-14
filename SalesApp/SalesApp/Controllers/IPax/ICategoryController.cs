using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers.IPax
{
    [Authorize(Roles = "Admin,IPax")]
    public class ICategoryController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: ICategory
        public ActionResult Index()
        {
            return View(db.I_Category.ToList().OrderByDescending(o => o.Name));
        }

        // GET: ICategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Category i_Category = db.I_Category.Find(id);
            if (i_Category == null)
            {
                return HttpNotFound();
            }
            return View(i_Category);
        }

        // GET: ICategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ICategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] I_Category i_Category)
        {
            if (ModelState.IsValid)
            {
                db.I_Category.Add(i_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(i_Category);
        }

        // GET: ICategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Category i_Category = db.I_Category.Find(id);
            if (i_Category == null)
            {
                return HttpNotFound();
            }
            return View(i_Category);
        }

        // POST: ICategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] I_Category i_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i_Category);
        }

        // GET: ICategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Category i_Category = db.I_Category.Find(id);
            if (i_Category == null)
            {
                return HttpNotFound();
            }
            return View(i_Category);
        }

        // POST: ICategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Category i_Category = db.I_Category.Find(id);
            db.I_Category.Remove(i_Category);
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
