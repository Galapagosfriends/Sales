using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers.Admin
{
    [Authorize(Roles = "Admin,Manager,Contador")]
    public class GASTOS_TYPE_PROVIDERController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: GASTOS_TYPE_PROVIDER
        public ActionResult Index()
        {
            var gASTOS_TYPE_PROVIDER = db.GASTOS_TYPE_PROVIDER.Include(g => g.CATEGORY_PRODUCT).Include(g => g.GASTOS_TYPE).Include(g => g.PROVIDER);
            return View(gASTOS_TYPE_PROVIDER.ToList());
        }

        // GET: GASTOS_TYPE_PROVIDER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER = db.GASTOS_TYPE_PROVIDER.Find(id);
            if (gASTOS_TYPE_PROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_TYPE_PROVIDER);
        }

        // GET: GASTOS_TYPE_PROVIDER/Create
        public ActionResult Create()
        {
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name");
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name");
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name");
            return View();
        }

        // POST: GASTOS_TYPE_PROVIDER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GastosTypeId,Comment,ProviderId,CategoryProductId,Price,Populate")] GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.GASTOS_TYPE_PROVIDER.Add(gASTOS_TYPE_PROVIDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", gASTOS_TYPE_PROVIDER.CategoryProductId);
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name", gASTOS_TYPE_PROVIDER.GastosTypeId);
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name", gASTOS_TYPE_PROVIDER.ProviderId);
            return View(gASTOS_TYPE_PROVIDER);
        }

        // GET: GASTOS_TYPE_PROVIDER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER = db.GASTOS_TYPE_PROVIDER.Find(id);
            if (gASTOS_TYPE_PROVIDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", gASTOS_TYPE_PROVIDER.CategoryProductId);
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name", gASTOS_TYPE_PROVIDER.GastosTypeId);
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name", gASTOS_TYPE_PROVIDER.ProviderId);
            return View(gASTOS_TYPE_PROVIDER);
        }

        // POST: GASTOS_TYPE_PROVIDER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GastosTypeId,Comment,ProviderId,CategoryProductId,Price,Populate")] GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gASTOS_TYPE_PROVIDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", gASTOS_TYPE_PROVIDER.CategoryProductId);
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name", gASTOS_TYPE_PROVIDER.GastosTypeId);
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name", gASTOS_TYPE_PROVIDER.ProviderId);
            return View(gASTOS_TYPE_PROVIDER);
        }

        // GET: GASTOS_TYPE_PROVIDER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER = db.GASTOS_TYPE_PROVIDER.Find(id);
            if (gASTOS_TYPE_PROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_TYPE_PROVIDER);
        }

        // POST: GASTOS_TYPE_PROVIDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER = db.GASTOS_TYPE_PROVIDER.Find(id);
            db.GASTOS_TYPE_PROVIDER.Remove(gASTOS_TYPE_PROVIDER);
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
