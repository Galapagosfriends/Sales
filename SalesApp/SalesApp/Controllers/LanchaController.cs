
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers
{
   
    public class LanchaController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();        
        // GET: PRODUCT_CALENDER/Details/5
        public ActionResult Index(int? id, int? month)
        {
            int startMonth =  DateTime.Now.Month; 
            if(Request.IsAuthenticated && (User.IsInRole("Contador") || User.IsInRole("Admin"))){
                startMonth =4;
            }
            month = month == null || month==0? startMonth : month;


            if (id == null && ViewBag.ParentCategoyProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tBL_PRODUCTCALENDER = db.PRODUCT_CALENDER.Where(pd =>
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

         [Authorize(Roles = "Admin,Manager")]
        public ActionResult Salir(int? id, int? CategoryProductId, int? month)
        {
            db.SP_SetSalir(id);
            return RedirectToAction("Index", new { Id = CategoryProductId, month = month });
        }

         [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult SetGastos(int? Id, int? CategoryProductId)
        {
            db.SP_SetGastos(Id, CategoryProductId);
          return RedirectToAction("SetGastos", "GASTOS_OPERATION", new { Id = Id, CategoryProductId = CategoryProductId });
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
