using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers
{  
    public class BoatController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();        
        // GET: PRODUCT_CALENDER/Details/5
        public ActionResult Index(int? id, int? month)
        {
            month = month == null || month==0? DateTime.Now.Month : month;

            if (id == null && ViewBag.ParentCategoyProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Include(p => p.CATEGORY_PRODUCT).Where(pd =>
            (pd.CategoryProductId == id) && (pd.Month == month) && (pd.Year == DateTime.Now.Year));

            ViewBag.ParentCategoyProduct = id;
           if (tBL_PRODUCTCALENDER == null || tBL_PRODUCTCALENDER.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(tBL_PRODUCTCALENDER.ToList());
        }

         [Authorize(Roles = "Admin,Manager")]
        public ActionResult Charter(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetCharter(id);
            return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
        }

         [Authorize(Roles = "Admin,Manager")]
        public ActionResult NoSalir(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetNoSalir(id);
            return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
        }

        // [Authorize(Roles = "Admin,Manager")]
        //public ActionResult SetGastos(int? id, int? CategoryProductId, int? month)
        //{
        //    db.SP_SetGastos(id);
        //    return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
        //}

         [Authorize(Roles = "Admin,Manager")]
        public ActionResult Salir(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetSalir(id);
            return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
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
