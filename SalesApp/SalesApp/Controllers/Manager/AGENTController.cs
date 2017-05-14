
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
    public class AGENTController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: AGENTs
        public ActionResult Index()
        {
            var aGENT = db.AGENT.Include(a => a.ADRESS);
            return View(aGENT.ToList().OrderByDescending(o => o.FirstName));
        }

        // GET: AGENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENT aGENT = db.AGENT.Find(id);
            if (aGENT == null)
            {
                return HttpNotFound();
            }
            return View(aGENT);
        }

        // GET: AGENTs/Create
        public ActionResult Create()
        {
            ViewBag.AdresseID = new SelectList(db.ADRESS, "Id", "Street");
            return View();
        }

        // POST: AGENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,AdresseID")] AGENT aGENT)
        {
            if (ModelState.IsValid)
            {
                db.AGENT.Add(aGENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdresseID = new SelectList(db.ADRESS, "Id", "Street", aGENT.AdresseID);
            return View(aGENT);
        }

        // GET: AGENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENT aGENT = db.AGENT.Find(id);
            if (aGENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdresseID = new SelectList(db.ADRESS, "Id", "Street", aGENT.AdresseID);
            return View(aGENT);
        }

        // POST: AGENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,AdresseID")] AGENT aGENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aGENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdresseID = new SelectList(db.ADRESS, "Id", "Street", aGENT.AdresseID);
            return View(aGENT);
        }

        // GET: AGENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENT aGENT = db.AGENT.Find(id);
            if (aGENT == null)
            {
                return HttpNotFound();
            }
            return View(aGENT);
        }

        // POST: AGENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AGENT aGENT = db.AGENT.Find(id);
            db.AGENT.Remove(aGENT);
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
