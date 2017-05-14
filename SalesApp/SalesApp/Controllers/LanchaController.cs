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
            var tPRODUCTCALENDER = db.PRODUCT_CALENDER.Include(p => p.CATEGORY_PRODUCT).Where(pd =>
            (pd.CategoryProductId == id) && (pd.Month == month) && (pd.Year == DateTime.Now.Year));

            ViewBag.ParentCategoyProduct = id;

            if (tPRODUCTCALENDER == null || tPRODUCTCALENDER.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(tPRODUCTCALENDER.ToList());
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
