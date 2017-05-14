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
    public class IMembersNumberController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: IMembersNumber
        public ActionResult Index()
        {
            return View(db.I_MembersNumber.ToList().OrderByDescending(o => o.Name));
        }

        // GET: IMembersNumber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_MembersNumber i_MembersNumber = db.I_MembersNumber.Find(id);
            if (i_MembersNumber == null)
            {
                return HttpNotFound();
            }
            return View(i_MembersNumber);
        }

        // GET: IMembersNumber/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IMembersNumber/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] I_MembersNumber i_MembersNumber)
        {
            if (ModelState.IsValid)
            {
                db.I_MembersNumber.Add(i_MembersNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(i_MembersNumber);
        }

        // GET: IMembersNumber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_MembersNumber i_MembersNumber = db.I_MembersNumber.Find(id);
            if (i_MembersNumber == null)
            {
                return HttpNotFound();
            }
            return View(i_MembersNumber);
        }

        // POST: IMembersNumber/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] I_MembersNumber i_MembersNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_MembersNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i_MembersNumber);
        }

        // GET: IMembersNumber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_MembersNumber i_MembersNumber = db.I_MembersNumber.Find(id);
            if (i_MembersNumber == null)
            {
                return HttpNotFound();
            }
            return View(i_MembersNumber);
        }

        // POST: IMembersNumber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_MembersNumber i_MembersNumber = db.I_MembersNumber.Find(id);
            db.I_MembersNumber.Remove(i_MembersNumber);
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
