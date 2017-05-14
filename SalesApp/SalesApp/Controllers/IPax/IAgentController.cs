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
    public class IAgentController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: IAgent
        public ActionResult Index()
        {
            return View(db.I_Agent.ToList().ToList().OrderByDescending(o => o.FirstName));
        }

        // GET: IAgent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Agent i_Agent = db.I_Agent.Find(id);
            if (i_Agent == null)
            {
                return HttpNotFound();
            }
            return View(i_Agent);
        }

        // GET: IAgent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IAgent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email")] I_Agent i_Agent)
        {
            if (ModelState.IsValid)
            {
                db.I_Agent.Add(i_Agent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(i_Agent);
        }

        // GET: IAgent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Agent i_Agent = db.I_Agent.Find(id);
            if (i_Agent == null)
            {
                return HttpNotFound();
            }
            return View(i_Agent);
        }

        // POST: IAgent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email")] I_Agent i_Agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i_Agent);
        }

        // GET: IAgent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Agent i_Agent = db.I_Agent.Find(id);
            if (i_Agent == null)
            {
                return HttpNotFound();
            }
            return View(i_Agent);
        }

        // POST: IAgent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Agent i_Agent = db.I_Agent.Find(id);
            db.I_Agent.Remove(i_Agent);
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
