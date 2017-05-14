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
    public class IItineraryController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: IItinerary
        public ActionResult Index()
        {
            var i_Itinerary = db.I_Itinerary.Include(i => i.I_CategoryPlace).Include(i => i.I_Days).Include(i => i.I_Proveedor);
            return View(i_Itinerary.ToList().OrderByDescending(o => o.I_Proveedor.Name));
        }

        // GET: IItinerary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Itinerary i_Itinerary = db.I_Itinerary.Find(id);
            if (i_Itinerary == null)
            {
                return HttpNotFound();
            }
            return View(i_Itinerary);
        }

        // GET: IItinerary/Create
        public ActionResult Create()
        {
            ViewBag.ICategoryPlaceId = new SelectList(db.I_CategoryPlace, "Id", "Name");
            ViewBag.IDaysId = new SelectList(db.I_Days, "Id", "Name");
            ViewBag.IProvideId = new SelectList(db.I_Proveedor, "Id", "Name");
            return View();
        }

        // POST: IItinerary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ICategoryPlaceId,IProvideId,IDaysId,Price,MeetingPoint,StartTourTime,EndTourTime,Included,CreatedBy,Created,Deleted,ModifyDate,ModifyBy")] I_Itinerary i_Itinerary)
        {
            if (ModelState.IsValid)
            {
                i_Itinerary.ModifyBy = User.Identity.Name;
                i_Itinerary.Created = DateTime.Now;            
                db.I_Itinerary.Add(i_Itinerary);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ICategoryPlaceId = new SelectList(db.I_CategoryPlace, "Id", "Name", i_Itinerary.ICategoryPlaceId);
            ViewBag.IDaysId = new SelectList(db.I_Days, "Id", "Name", i_Itinerary.IDaysId);
            ViewBag.IProvideId = new SelectList(db.I_Proveedor, "Id", "Name", i_Itinerary.IProvideId);
            return View(i_Itinerary);
        }

        // GET: IItinerary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Itinerary i_Itinerary = db.I_Itinerary.Find(id);
            if (i_Itinerary == null)
            {
                return HttpNotFound();
            }
            ViewBag.ICategoryPlaceId = new SelectList(db.I_CategoryPlace, "Id", "Name", i_Itinerary.ICategoryPlaceId);
            ViewBag.IDaysId = new SelectList(db.I_Days, "Id", "Name", i_Itinerary.IDaysId);
            ViewBag.IProvideId = new SelectList(db.I_Proveedor, "Id", "Name", i_Itinerary.IProvideId);
            return View(i_Itinerary);
        }

        // POST: IItinerary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ICategoryPlaceId,IProvideId,IDaysId,Price,MeetingPoint,StartTourTime,EndTourTime,Included,CreatedBy,Created,Deleted,ModifyDate,ModifyBy")] I_Itinerary i_Itinerary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Itinerary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ICategoryPlaceId = new SelectList(db.I_CategoryPlace, "Id", "Name", i_Itinerary.ICategoryPlaceId);
            ViewBag.IDaysId = new SelectList(db.I_Days, "Id", "Name", i_Itinerary.IDaysId);
            ViewBag.IProvideId = new SelectList(db.I_Proveedor, "Id", "Name", i_Itinerary.IProvideId);
            return View(i_Itinerary);
        }

        // GET: IItinerary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Itinerary i_Itinerary = db.I_Itinerary.Find(id);
            if (i_Itinerary == null)
            {
                return HttpNotFound();
            }
            return View(i_Itinerary);
        }

        // POST: IItinerary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Itinerary i_Itinerary = db.I_Itinerary.Find(id);
            db.I_Itinerary.Remove(i_Itinerary);
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
