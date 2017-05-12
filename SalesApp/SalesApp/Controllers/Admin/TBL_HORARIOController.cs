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
    public class TBL_HORARIOController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();
        // GET: TBL_HORARIO
        public ActionResult Index()
        {
            return View(db.TBL_HORARIO.ToList());
        }

        // GET: TBL_HORARIO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_HORARIO tBL_HORARIO = db.TBL_HORARIO.Find(id);
            if (tBL_HORARIO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_HORARIO);
        }

        // GET: TBL_HORARIO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBL_HORARIO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,DAY,Month,MorFrom,MorTo,AftFrom,AftTo,Inserted,Updated,UpdatedUser,HouTotal,HouMonthTotal")] TBL_HORARIO tBL_HORARIO)
        {
            if (ModelState.IsValid)
            {
                db.TBL_HORARIO.Add(tBL_HORARIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_HORARIO);
        }

        // GET: TBL_HORARIO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_HORARIO tBL_HORARIO = db.TBL_HORARIO.Find(id);
            if (tBL_HORARIO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_HORARIO);
        }

        // POST: TBL_HORARIO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,DAY,Month,MorFrom,MorTo,AftFrom,AftTo,Inserted,Updated,UpdatedUser,HouTotal,HouMonthTotal")] TBL_HORARIO tBL_HORARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_HORARIO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_HORARIO);
        }

        // GET: TBL_HORARIO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_HORARIO tBL_HORARIO = db.TBL_HORARIO.Find(id);
            if (tBL_HORARIO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_HORARIO);
        }

        // POST: TBL_HORARIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_HORARIO tBL_HORARIO = db.TBL_HORARIO.Find(id);
            db.TBL_HORARIO.Remove(tBL_HORARIO);
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
