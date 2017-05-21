using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers.Accounter
{
     [Authorize(Roles = "Admin,Manager,Contador")]
    public class GASTOS_OPERATIONController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: GASTOS_OPERATION
        public ActionResult Index(int ProductCalenderId, int CategoryProductId)
        {
            var pGASTOS_OPERATION = db.GASTOS_OPERATION.Include(m => m.GASTOS_TYPE_PROVIDER).Include(s=>s.GASTOS_TYPE_PROVIDER.CATEGORY_PRODUCT).Where(d => d.ProductCalenderId == ProductCalenderId).OrderByDescending(s => s.GASTOS_TYPE_PROVIDER.PROVIDER.Name);

            if (pGASTOS_OPERATION.ToList().Count < 1)
            {
                return RedirectToAction("Create", new { ProductCalenderId = ProductCalenderId });
            }

            return View(pGASTOS_OPERATION.ToList().OrderByDescending(o => o.GASTOS_TYPE_PROVIDER.Comment));
        }

         [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult SetGastos(int? ProductCalenderId, int? CategoryProductId)
        {
            db.SP_SetGastos(ProductCalenderId, CategoryProductId);
            return RedirectToAction("Index", new { ProductCalenderId = ProductCalenderId, CategoryProductId = CategoryProductId });
        }


        // GET: GASTOS_TYPE_PROVIDER/Create
        public ActionResult AddProviderType(int ProductCalenderId, int CategoryProductId)
        {
            var gASTOS_TYPE_PROVIDER = db.GASTOS_TYPE_PROVIDER
                .Include(g => g.CATEGORY_PRODUCT)
                .Include(g => g.GASTOS_TYPE)
                .Include(g => g.PROVIDER).Where(n => n.CategoryProductId == CategoryProductId && (n.Populate == false || n.Populate == null));
            return View(gASTOS_TYPE_PROVIDER.ToList().OrderByDescending(o => o.PROVIDER.Name));

            //ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name");
            //ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name");
            //ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name");

            //return View();
        }

        // POST: GASTOS_TYPE_PROVIDER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        public ActionResult Add([Bind(Include = "Id,GastosTypeId,Comment,ProviderId,CategoryProductId,Price")] GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER)
        {
            if (ModelState.IsValid)
            {
                GASTOS_OPERATION gASTOS_OPERATION = new GASTOS_OPERATION()
                {
                    ProductCalenderId = int.Parse(Request["ProductCalenderId"])
                    ,Description = gASTOS_TYPE_PROVIDER.Comment
                    ,Price = gASTOS_TYPE_PROVIDER.Price
                    ,GastosTypeProviderId = gASTOS_TYPE_PROVIDER.Id
                };

                db.GASTOS_OPERATION.Add(gASTOS_OPERATION);
                db.SaveChanges();
                return GoBack(gASTOS_OPERATION);
            }

            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", gASTOS_TYPE_PROVIDER.CategoryProductId);
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name", gASTOS_TYPE_PROVIDER.GastosTypeId);
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name", gASTOS_TYPE_PROVIDER.ProviderId);
            return View(gASTOS_TYPE_PROVIDER);
        }

        public ActionResult NewProviderType(int ProductCalenderId, int CategoryProductId)
        {
            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name");
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name");
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProviderType([Bind(Include = "Id,GastosTypeId,Comment,ProviderId,CategoryProductId,Price")] GASTOS_TYPE_PROVIDER gASTOS_TYPE_PROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.GASTOS_TYPE_PROVIDER.Add(gASTOS_TYPE_PROVIDER);
                db.SaveChanges();

                GASTOS_OPERATION gASTOS_OPERATION = new GASTOS_OPERATION()
                {
                    ProductCalenderId = int.Parse(Request["ProductCalenderId"])
                    ,Description = gASTOS_TYPE_PROVIDER.Comment
                    ,Price = gASTOS_TYPE_PROVIDER.Price
                    ,GastosTypeProviderId = gASTOS_TYPE_PROVIDER.Id
                };

                db.GASTOS_OPERATION.Add(gASTOS_OPERATION);
                db.SaveChanges();
                return GoBack(gASTOS_OPERATION);
               
            }

            ViewBag.CategoryProductId = new SelectList(db.CATEGORY_PRODUCT, "Id", "Name", gASTOS_TYPE_PROVIDER.CategoryProductId);
            ViewBag.GastosTypeId = new SelectList(db.GASTOS_TYPE, "Id", "Name", gASTOS_TYPE_PROVIDER.GastosTypeId);
            ViewBag.ProviderId = new SelectList(db.PROVIDER, "Id", "Name", gASTOS_TYPE_PROVIDER.ProviderId);
            return View(gASTOS_TYPE_PROVIDER);
        }
        
     
        // GET: GASTOS_OPERATION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_OPERATION gASTOS_OPERATION = db.GASTOS_OPERATION.Find(id);
            if (gASTOS_OPERATION == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_OPERATION);
        }

        // GET: GASTOS_OPERATION/Create
        public ActionResult Create(int? ProductCalenderId, int? CategoryProductId)
        {
            if (ProductCalenderId == null || CategoryProductId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CALENDER pRODUCT_CALENDER = db.PRODUCT_CALENDER.Find(ProductCalenderId);
            if (pRODUCT_CALENDER == null)
            {
                return HttpNotFound();
            }


            GASTOS_OPERATION gASTOS_OPERATION = new GASTOS_OPERATION() { ProductCalenderId = ProductCalenderId };
            ViewBag.GastosTypeProviderId = new SelectList(db.GASTOS_TYPE_PROVIDER, "Id", "Comment", gASTOS_OPERATION.GastosTypeProviderId);

            return View(gASTOS_OPERATION);
        }

        // POST: GASTOS_OPERATION/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductCalenderId,Description,GastosTypeProviderId,PayDate,BillingPath,Created,Deleted,Closed,Price")] GASTOS_OPERATION gASTOS_OPERATION)
        {
            if (ModelState.IsValid)
            {
                db.GASTOS_OPERATION.Add(gASTOS_OPERATION);
                db.SaveChanges();
                return RedirectToAction("Index",new { ProductCalenderId = gASTOS_OPERATION.ProductCalenderId });
            }

            return View(gASTOS_OPERATION);
        }

        // GET: GASTOS_OPERATION/Edit/5
        public ActionResult Edit(int? id, int? ProductCalenderId, int? CategoryProductId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_OPERATION gASTOS_OPERATION = db.GASTOS_OPERATION.Find(id);
            if (gASTOS_OPERATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.GastosTypeProviderId = new SelectList(db.GASTOS_TYPE_PROVIDER, "Id", "Comment", gASTOS_OPERATION.GastosTypeProviderId);
            // ViewBag.CategoryId = new SelectList(db.CATEGORY, "Id", "Name");
            return View(gASTOS_OPERATION);
        }

        // POST: GASTOS_OPERATION/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductCalenderId,Description,GastosTypeProviderId,PayDate,BillingPath,Created,Deleted,Closed,Price")] GASTOS_OPERATION gASTOS_OPERATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gASTOS_OPERATION).State = EntityState.Modified;
                db.SaveChanges();
                return GoBack(gASTOS_OPERATION);
            }
            return View(gASTOS_OPERATION);
        }


        public ActionResult GoBack(GASTOS_OPERATION gASTOS_OPERATION)
        {
            GASTOS_OPERATION ls = db.GASTOS_OPERATION.Include(m => m.GASTOS_TYPE_PROVIDER).Where(m => m.Id == gASTOS_OPERATION.Id).FirstOrDefault();
            return RedirectToAction("Index", new { ProductCalenderId = ls.ProductCalenderId, CategoryProductId = ls.GASTOS_TYPE_PROVIDER.CategoryProductId });

        }

        // GET: GASTOS_OPERATION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GASTOS_OPERATION gASTOS_OPERATION = db.GASTOS_OPERATION.Find(id);
            if (gASTOS_OPERATION == null)
            {
                return HttpNotFound();
            }
            return View(gASTOS_OPERATION);
        }

        // POST: GASTOS_OPERATION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GASTOS_OPERATION gASTOS_OPERATION = db.GASTOS_OPERATION.Find(id);
            int? ProductCalenderId = gASTOS_OPERATION.ProductCalenderId;
            int CategoryProductId = gASTOS_OPERATION.GASTOS_TYPE_PROVIDER.CategoryProductId;

            db.GASTOS_OPERATION.Remove(gASTOS_OPERATION);
            db.SaveChanges();
            return RedirectToAction("Index", new { ProductCalenderId = ProductCalenderId, CategoryProductId = CategoryProductId });

           
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
