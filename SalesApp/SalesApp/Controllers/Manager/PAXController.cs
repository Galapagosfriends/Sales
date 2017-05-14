
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
     [Authorize(Roles = "Admin,Manager")]
    public class PAXController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PAXes
        public ActionResult Index()
        {
            return View(db.PAX.ToList().OrderByDescending(o => o.First_Name));
        }

        // GET: PAXes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pax pAX = db.PAX.Find(id);
            if (pAX == null)
            {
                return HttpNotFound();
            }
            return View(pAX);
        }
    
        // GET: PAXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PAXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,Birthdate,Passport,CountryId,AdressId,Gender,Email,PaxCategory,Created")] Pax pAX)
        {
            if (ModelState.IsValid)
            {
                db.PAX.Add(pAX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pAX);
        }

        //// GET: PAXes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Pax pAX = db.Pax.Find(id);
        //    if (pAX == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pAX);
        //}

        public ActionResult Edit(int? id, int? resvId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_RESERVATION pRODUCT_RESERVATION = db.PRODUCT_RESERVATION.Find(id);
            ViewBag.ProductCalenderId = id;
            ViewBag.ProductCalenderId = resvId;
            Pax pAX = db.PAX.Include(prp => prp.PRODUCT_RESERVATION).Where(p=> p.Id == id).SingleOrDefault();
            
            if (pAX == null)
            {
                return HttpNotFound();
            }
                      
            return View(pAX);
        }


              


        // POST: PAXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,Birthdate,Passport,CountryId,AdressId,Gender,Email,PaxCategory,Created")] Pax pAX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAX).State = EntityState.Modified;
                
                db.SaveChanges();

                return RedirectToAction("Index", "PRODUCT_RESERVATION", new { Id = Request.Params["resvId"] });

              //  return RedirectToAction("Index");
            }
            return View(pAX);
        }

        // GET: PAXes/Delete/5
         [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pax pAX = db.PAX.Find(id);
            if (pAX == null)
            {
                return HttpNotFound();
            }
            return View(pAX);
        }

        // POST: PAXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pax pAX = db.PAX.Find(id);
            db.PAX.Remove(pAX);
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
