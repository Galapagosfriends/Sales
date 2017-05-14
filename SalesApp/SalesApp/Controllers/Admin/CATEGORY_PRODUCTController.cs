
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{
    [Authorize(Roles ="Admin,Manager")]
    public class CATEGORY_PRODUCTController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        //// GET: CATEGORY_PRODUCT
        //public ActionResult Index()
        //{
        //    var cATEGORY_PRODUCT = db.CATEGORY_PRODUCT.Include(c => c.CATEGORY);
        //    return View(cATEGORY_PRODUCT.ToList().OrderByDescending(o => o.Name));
        //}


        // GET: TBL_CATEGORYPRODUCT
        public ActionResult Index(int? Id)
        {

            if (Id == null)
            {
                if (ViewBag.ParentCategoy > 0)
                {
                    Id = ViewBag.ParentCategoy;
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tBL_CATEGORYPRODUCT = db.CATEGORY_PRODUCT.Include(t => t.CATEGORY).Where(cp => cp.CategoryId == Id);
            if (tBL_CATEGORYPRODUCT == null)
            {
                return HttpNotFound();
            }

            ViewBag.ParentCategoy = Id;
            return View(tBL_CATEGORYPRODUCT.ToList().OrderByDescending(o => o.Name));
        }

        // GET: TBL_CATEGORYPRODUCT
        //public ActionResult Index()
        //{
        //    int Id = ViewBag.ParentCategoy;
        //    if (Id <= 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var tBL_CATEGORYPRODUCT = db.CATEGORY_PRODUCT.Include(t => t.CATEGORY).Where(cp => cp.CategoryId == Id);
        //    if (tBL_CATEGORYPRODUCT == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.ParentCategoy = Id;
        //    return View(tBL_CATEGORYPRODUCT.ToList().OrderByDescending(o => o.Name));
        //}






        // GET: CATEGORY_PRODUCT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY_PRODUCT cATEGORY_PRODUCT = db.CATEGORY_PRODUCT.Find(id);
            if (cATEGORY_PRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY_PRODUCT);
        }

        // GET: CATEGORY_PRODUCT/Create
         [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CATEGORY, "Id", "Name");
            return View();
        }

        // POST: CATEGORY_PRODUCT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId,Lock")] CATEGORY_PRODUCT cATEGORY_PRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.CATEGORY_PRODUCT.Add(cATEGORY_PRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.CATEGORY, "Id", "Name", cATEGORY_PRODUCT.CategoryId);
            return View(cATEGORY_PRODUCT);
        }

        // GET: CATEGORY_PRODUCT/Edit/5
         [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY_PRODUCT cATEGORY_PRODUCT = db.CATEGORY_PRODUCT.Find(id);
            if (cATEGORY_PRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.CATEGORY, "Id", "Name", cATEGORY_PRODUCT.CategoryId);
            return View(cATEGORY_PRODUCT);
        }

        // POST: CATEGORY_PRODUCT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,Lock")] CATEGORY_PRODUCT cATEGORY_PRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATEGORY_PRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.ParentCategoy = cATEGORY_PRODUCT.CategoryId;
                return RedirectToAction("Index", new { Id = cATEGORY_PRODUCT.CategoryId });
            }
            ViewBag.CategoryId = new SelectList(db.CATEGORY, "Id", "Name", cATEGORY_PRODUCT.CategoryId);
            return View(cATEGORY_PRODUCT);
        }

        // GET: CATEGORY_PRODUCT/Delete/5
         [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY_PRODUCT cATEGORY_PRODUCT = db.CATEGORY_PRODUCT.Find(id);
            if (cATEGORY_PRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY_PRODUCT);
        }

        // POST: CATEGORY_PRODUCT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            CATEGORY_PRODUCT cATEGORY_PRODUCT = db.CATEGORY_PRODUCT.Find(id);
            db.CATEGORY_PRODUCT.Remove(cATEGORY_PRODUCT);
            int i = cATEGORY_PRODUCT.CategoryId;
            db.SaveChanges();
            return RedirectToAction("Index", new { Id = i });
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
