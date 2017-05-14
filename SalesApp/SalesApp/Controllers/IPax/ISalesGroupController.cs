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
    public class ISalesGroupController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: ISalesGroup
        public ActionResult Index()
        {
            var i_SalesGroup = db.I_SalesGroup.Include(i => i.I_Agent).Include(i => i.I_MembersNumber);
            return View(i_SalesGroup.ToList().OrderByDescending(o => o.Name).ToList().OrderByDescending(o => o.Name));
        }

        // GET: ISalesGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_SalesGroup i_SalesGroup = db.I_SalesGroup.Find(id);
            if (i_SalesGroup == null)
            {
                return HttpNotFound();
            }
            return View(i_SalesGroup);
        }

        // GET: ISalesGroup/Create
        public ActionResult Create()
        {
            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName");
            ViewBag.MemberNumberId = new SelectList(db.I_MembersNumber, "Id", "Id");
            return View();
        }

        // POST: ISalesGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,WhatsAppName,MemberNumberId,StartDay,Deleted,CreateDate,ModifyDate,Day,Month,Year,ItineraryDays,AgentId,DeletedBy,ModifyBy,Ready,TotalPricePaxs,TotalPriceSale")] I_SalesGroup i_SalesGroup)
        {
            if (ModelState.IsValid)
            {
                db.I_SalesGroup.Add(i_SalesGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName", i_SalesGroup.AgentId);
            ViewBag.MemberNumberId = new SelectList(db.I_MembersNumber, "Id", "Id", i_SalesGroup.MemberNumberId);
            return View(i_SalesGroup);
        }

        // GET: ISalesGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_SalesGroup i_SalesGroup = db.I_SalesGroup.Find(id);
            if (i_SalesGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName", i_SalesGroup.AgentId);
            ViewBag.MemberNumberId = new SelectList(db.I_MembersNumber, "Id", "Id", i_SalesGroup.MemberNumberId);
            return View(i_SalesGroup);
        }

        // POST: ISalesGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,WhatsAppName,MemberNumberId,StartDay,Deleted,CreateDate,ModifyDate,Day,Month,Year,ItineraryDays,AgentId,DeletedBy,ModifyBy,Ready,TotalPricePaxs,TotalPriceSale")] I_SalesGroup i_SalesGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_SalesGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName", i_SalesGroup.AgentId);
            ViewBag.MemberNumberId = new SelectList(db.I_MembersNumber, "Id", "Id", i_SalesGroup.MemberNumberId);
            return View(i_SalesGroup);
        }

        // GET: ISalesGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_SalesGroup i_SalesGroup = db.I_SalesGroup.Find(id);
            if (i_SalesGroup == null)
            {
                return HttpNotFound();
            }
            return View(i_SalesGroup);
        }

        // POST: ISalesGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_SalesGroup i_SalesGroup = db.I_SalesGroup.Find(id);
            db.I_SalesGroup.Remove(i_SalesGroup);
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
