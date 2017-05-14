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
    public class IProveedorController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: IProveedor
        public ActionResult Index()
        {
            return View(db.I_Proveedor.ToList().OrderByDescending(o => o.Name));
        }

        // GET: IProveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Proveedor i_Proveedor = db.I_Proveedor.Find(id);
            if (i_Proveedor == null)
            {
                return HttpNotFound();
            }
            return View(i_Proveedor);
        }

        // GET: IProveedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IProveedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CtaBancos,Contact,AdressId")] I_Proveedor i_Proveedor)
        {
            if (ModelState.IsValid)
            {
                db.I_Proveedor.Add(i_Proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(i_Proveedor);
        }

        // GET: IProveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Proveedor i_Proveedor = db.I_Proveedor.Find(id);
            if (i_Proveedor == null)
            {
                return HttpNotFound();
            }
            return View(i_Proveedor);
        }

        // POST: IProveedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CtaBancos,Contact,AdressId")] I_Proveedor i_Proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i_Proveedor);
        }

        // GET: IProveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Proveedor i_Proveedor = db.I_Proveedor.Find(id);
            if (i_Proveedor == null)
            {
                return HttpNotFound();
            }
            return View(i_Proveedor);
        }

        // POST: IProveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Proveedor i_Proveedor = db.I_Proveedor.Find(id);
            db.I_Proveedor.Remove(i_Proveedor);
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
