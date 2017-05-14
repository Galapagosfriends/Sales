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
    public class ISalesController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: ISales
        public ActionResult Index()
        {
            var i_Sales = db.I_Sales.Include(i => i.I_Agent).Include(i => i.I_Itinerary).Include(i => i.I_SalesGroup).Include(i => i.I_Pax);
            return View(i_Sales.ToList().OrderByDescending(o => o.Dates));
        }

        // GET: ISales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Sales i_Sales = db.I_Sales.Find(id);
            if (i_Sales == null)
            {
                return HttpNotFound();
            }
            return View(i_Sales);
        }

        // GET: ISales/Create
        public ActionResult Create()
        {
            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName");
            ViewBag.ItineraryId = new SelectList(db.I_Itinerary, "Id", "MeetingPoint");
            ViewBag.SalesGroupId = new SelectList(db.I_SalesGroup, "Id", "Name");
            ViewBag.PaxId = new SelectList(db.I_Pax, "Id", "First_Name");
            return View();
        }

        // POST: ISales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AgentId,SalesGroupId,PaxId,ItineraryId,FlightIn,FlightOut,Dates,Comment,PrecioNett,PrecioNettTotalPax")] I_Sales i_Sales)
        {
            if (ModelState.IsValid)
            {
                db.I_Sales.Add(i_Sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName", i_Sales.AgentId);
            ViewBag.ItineraryId = new SelectList(db.I_Itinerary, "Id", "MeetingPoint", i_Sales.ItineraryId);
            ViewBag.SalesGroupId = new SelectList(db.I_SalesGroup, "Id", "Name", i_Sales.SalesGroupId);
            ViewBag.PaxId = new SelectList(db.I_Pax, "Id", "First_Name", i_Sales.PaxId);
            return View(i_Sales);
        }

        // GET: ISales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Sales i_Sales = db.I_Sales.Find(id);
            if (i_Sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName", i_Sales.AgentId);
            ViewBag.ItineraryId = new SelectList(db.I_Itinerary, "Id", "MeetingPoint", i_Sales.ItineraryId);
            ViewBag.SalesGroupId = new SelectList(db.I_SalesGroup, "Id", "Name", i_Sales.SalesGroupId);
            ViewBag.PaxId = new SelectList(db.I_Pax, "Id", "First_Name", i_Sales.PaxId);
            return View(i_Sales);
        }

        // POST: ISales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgentId,SalesGroupId,PaxId,ItineraryId,FlightIn,FlightOut,Dates,Comment,PrecioNett,PrecioNettTotalPax")] I_Sales i_Sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.I_Agent, "Id", "FirstName", i_Sales.AgentId);
            ViewBag.ItineraryId = new SelectList(db.I_Itinerary, "Id", "MeetingPoint", i_Sales.ItineraryId);
            ViewBag.SalesGroupId = new SelectList(db.I_SalesGroup, "Id", "Name", i_Sales.SalesGroupId);
            ViewBag.PaxId = new SelectList(db.I_Pax, "Id", "First_Name", i_Sales.PaxId);
            return View(i_Sales);
        }

        // GET: ISales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Sales i_Sales = db.I_Sales.Find(id);
            if (i_Sales == null)
            {
                return HttpNotFound();
            }
            return View(i_Sales);
        }

        // POST: ISales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Sales i_Sales = db.I_Sales.Find(id);
            db.I_Sales.Remove(i_Sales);
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
