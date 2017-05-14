
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
     [Authorize(Roles = "Admin,Manager,Contador")]
    public class GASTOS_TYPEController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: GASTOS_TYPE
        public ActionResult Index()
        {
            return View(db.GASTOS_TYPE.ToList().OrderByDescending(o => o.Name).OrderBy(o => o.Name));
        }

        // GET: GASTOS_TYPE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_TYPE gASTOS_TYPE = db.GASTOS_TYPE.Find(id);
            if (gASTOS_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_TYPE);
        }

        // GET: GASTOS_TYPE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GASTOS_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] GASTOS_TYPE gASTOS_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.GASTOS_TYPE.Add(gASTOS_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gASTOS_TYPE);
        }

        // GET: GASTOS_TYPE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_TYPE gASTOS_TYPE = db.GASTOS_TYPE.Find(id);
            if (gASTOS_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_TYPE);
        }

        // POST: GASTOS_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GASTOS_TYPE gASTOS_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gASTOS_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gASTOS_TYPE);
        }

        // GET: GASTOS_TYPE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_TYPE gASTOS_TYPE = db.GASTOS_TYPE.Find(id);
            if (gASTOS_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_TYPE);
        }

        // POST: GASTOS_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GASTOS_TYPE gASTOS_TYPE = db.GASTOS_TYPE.Find(id);
            db.GASTOS_TYPE.Remove(gASTOS_TYPE);
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
