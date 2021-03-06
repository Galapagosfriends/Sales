﻿
using System;
using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SalesApp.Models.Entities;
using Microsoft.AspNet.Identity;

namespace SalesApp.Controllers
{
    [Authorize(Roles = "Admin,Manager,Contador")]
    public class PRODUCT_RESERVATIONController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PRODUCT_RESERVATION
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Index(int ProductCalenderId, int? CategoryProductId)
        {
            var pRODUCT_RESERVATION = db.PRODUCT_RESERVATION.Include(p => p.AGENCY).Include(p => p.AGENT).Include(p => p.PAX).Include(p => p.PAYMENT_TYPE).Include(p => p.PRODUCT_CALENDER).Include(p => p.PRODUCT_CALENDERHOUR).Include(p => p.PRODUCT_RESERVATION_TYPE).Include(p => p.TOUR_DAYS).Include(p => p.DIVE).Include(p => p.PRODUCT_CALENDER.CATEGORY_PRODUCT).Include(s => s.CABIN).Where(d => d.ProductCalenderId == ProductCalenderId);

            if (pRODUCT_RESERVATION.ToList().Count < 1)
            {
                return RedirectToAction("Create", new { ProductCalenderId = ProductCalenderId, CategoryProductId = CategoryProductId });
            }

            return View(pRODUCT_RESERVATION.ToList().OrderBy(o => o.PaxId));
        }

        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult MoveAll(int productCalenderId, int categoryProductId)
        {
            string contolname = "";

            switch (categoryProductId)
            {
                case 3:
                    contolname = "Lancha";
                    break;
                case 4:
                    contolname = "Lancha";
                    break;
                case 5:
                    contolname = "SolitarioJorge";
                    break;
                case 6:
                    contolname = "Kayak";
                    break;
                default:
                    break;
            }

            db.SP_Mover_AllPaxs(productCalenderId, categoryProductId).SingleOrDefault();

            return RedirectToAction("Index", contolname, new { ProductCalenderId = categoryProductId });
        }


        public ActionResult BacktoList(int? ProductCalenderId)
        {
            string backtoList = Request.Params["BacktoList"] == null && Request.Params["Create"] != null ? "Create" : Request.Params["BacktoList"];

            string calId = ProductCalenderId == null ? Request.Params["ProductCalenderId"] : ProductCalenderId.ToString();
            int id = 0;
            if (calId.Length > 0)
            {
                id = int.Parse(calId);
                PRODUCT_CALENDER p = db.PRODUCT_CALENDER.Where(s => s.Id == id).FirstOrDefault();
                if (p == null)
                {
                    return HttpNotFound();
                }
            }
            return RedirectToAction("Index", new { ProductCalenderId = id });
        }

        // GET: PRODUCT_RESERVATION/Details/5
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pRODUCT_RESERVATION = db.PRODUCT_RESERVATION.Where(pd => pd.ProductCalenderId == id);
            if (pRODUCT_RESERVATION == null || pRODUCT_RESERVATION.Count() == 0)
            {
                return RedirectToAction("Create", new { ProductCalenderId = id });
            }
            return View(pRODUCT_RESERVATION.ToList().OrderByDescending(o => o.PAX.First_Name));
        }


        // GET: PRODUCT_RESERVATION/Create
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Create(int? ProductCalenderId, int? CategoryProductId, int? tourdays)
        {
            ViewBag.AgencyId = new SelectList(db.AGENCY.OrderByDescending(a => a.Name), "Id", "Name");
            ViewBag.AgentId = new SelectList(db.AGENT.OrderByDescending(a => a.FirstName), "ID", "FirstName");
            ViewBag.PaxId = new SelectList(db.PAX, "Id", "First_Name");
            ViewBag.PaymentTypeId = new SelectList(db.PAYMENT_TYPE, "Id", "Description");
            ViewBag.ProductCalenderId = ProductCalenderId;
            ViewBag.PaymentStatusId = new SelectList(db.PAYMENT_STATUS, "Id", "Description");
            var s = (from a in db.Cabin
                     from od in a.PRODUCT_RESERVATION.Where(r => r.ProductCalenderId == ProductCalenderId).DefaultIfEmpty()
                     where (a.Id != od.CabinId & a.CategoryProductId == CategoryProductId)
                     select new
                     {
                         Id = a.Id,
                         Name = a.Name,
                         Description = a.Name + " " + a.Description,
                         CategoryProductId = a.CategoryProductId
                     })
                      ;
            ViewBag.CabinId = new SelectList(s, "Id", "Description");


            int tu;
            if (int.TryParse(Request["tourdays"] == null ? "" : Request["tourdays"], out tu))
            {
                ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS.Where(f => f.TourDaysId == tu), "Id", "Name");
            }
            else
            {
                ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS, "Id", "Name");
            }
            ViewBag.ProductReservationTypeId = new SelectList(db.PRODUCT_RESERVATION_TYPE, "Id", "Name");
            ViewBag.DiveId = new SelectList(db.DIVE, "Id", "Name");
            return View();
        }

        // POST: PRODUCT_RESERVATION/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Create([Bind(Include = "Id,ProductCalenderId,AgentId,PaymentTypeId,PaymentStatusId,AgencyId,Description,PaxId,Price,CreateDate,TourDaysId,ProductReservationTypeId,DivePrice,DiveId,PickUp,DropOff,Restrictions,Continua,CabinId,BloqueoDate,FacturaNr,NetoPrice")] PRODUCT_RESERVATION pRODUCT_RESERVATION)
        {
            string backtoList = Request.Params["BacktoList"] == null && Request.Params["Create"] != null ? "Create" : Request.Params["BacktoList"];
            if (backtoList.Equals("BacktoList"))
            {
                string calId = Request.Params["ProductCalenderId"];
                int id = 0;
                if (calId.Length > 0)
                {
                    id = int.Parse(calId);
                    PRODUCT_CALENDER p = db.PRODUCT_CALENDER.Where(s => s.Id == id).FirstOrDefault();
                    if (p == null)
                    {                      
                        return View(" Create HttpNotFoundReservation", HttpStatusCode.BadRequest);
                    }
                }
                return RedirectToAction("Index", new { ProductCalenderId = id });
            }
            else
            {
                if (backtoList.Equals("")) return HttpNotFound();

                if (ModelState.IsValid)
                {
                    int cid = Request.Params["ProductCalenderId"] == null ? pRODUCT_RESERVATION.Id : int.Parse(Request.Params["ProductCalenderId"].ToString());
                    int cuantas = 1;
                    if (Request.Form["CuantaVeces"] != null)
                    {
                        cuantas = int.Parse(Request.Form["CuantaVeces"].ToString()) == 0 ? 1 : int.Parse(Request.Form["CuantaVeces"].ToString());

                    }


                    for (int i = 0; i < cuantas; i++)
                    {
                        // SET @ID = SCOPE_IDENTITY()
                        int paxId = db.SP_Insert_PaxDefault((i + 1).ToString() + "pax ");
                        pRODUCT_RESERVATION.PaxId = paxId;
                        pRODUCT_RESERVATION.UserName = User.Identity.GetUserName();
                        pRODUCT_RESERVATION.PC_ID = System.Web.HttpContext.Current.Request.UserHostAddress == null ? HttpContext.Request.UserHostAddress : System.Web.HttpContext.Current.Request.UserHostAddress;

                        pRODUCT_RESERVATION.CreateDate = DateTime.Now;
                        pRODUCT_RESERVATION.ProductCalenderId = db.PRODUCT_CALENDER.Find(cid).Id;
                        db.PRODUCT_RESERVATION.Add(pRODUCT_RESERVATION);
                        db.SaveChanges();
                    }

                    db.SP_Insert_CountReservation(pRODUCT_RESERVATION.ProductCalenderId);

                    PRODUCT_CALENDER p = db.PRODUCT_CALENDER.Find(pRODUCT_RESERVATION.ProductCalenderId);
                    pRODUCT_RESERVATION.PRODUCT_CALENDER = p;

                    if (pRODUCT_RESERVATION.PRODUCT_CALENDER.CategoryProductId == 5)
                    {
                        db.SP_Insert_ReservationContinue(pRODUCT_RESERVATION.Id, pRODUCT_RESERVATION.ProductCalenderId, pRODUCT_RESERVATION.PRODUCT_CALENDER.CategoryProductId);
                    }

                    return RedirectToAction("Index", new { ProductCalenderId = pRODUCT_RESERVATION.ProductCalenderId });
                }
                ViewBag.AgencyId = new SelectList(db.AGENCY.OrderByDescending(a => a.Name), "Id", "Name", pRODUCT_RESERVATION.AgencyId);
                ViewBag.AgentId = new SelectList(db.AGENT.OrderByDescending(a => a.FirstName), "ID", "FirstName", pRODUCT_RESERVATION.AgentId);
                ViewBag.PaxId = new SelectList(db.PAX, "Id", "First_Name", pRODUCT_RESERVATION.PaxId);
                ViewBag.PaymentTypeId = new SelectList(db.PAYMENT_TYPE, "Id", "Description", pRODUCT_RESERVATION.PaymentTypeId);
                ViewBag.ProductCalenderId = pRODUCT_RESERVATION.ProductCalenderId;
                ViewBag.PaymentStatusId = new SelectList(db.PAYMENT_STATUS, "Id", "Description", pRODUCT_RESERVATION.PaymentStatusId);
                var s = (from a in db.Cabin
                         from od in a.PRODUCT_RESERVATION.Where(r => r.ProductCalenderId == pRODUCT_RESERVATION.ProductCalenderId).DefaultIfEmpty()
                         where (a.Id != od.CabinId & a.CategoryProductId == pRODUCT_RESERVATION.PRODUCT_CALENDER.CategoryProductId)
                         select new
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Description = a.Name + " " + a.Description,
                             CategoryProductId = a.CategoryProductId
                         })
                      ;
                ViewBag.CabinId = new SelectList(s, "Id", "Description", pRODUCT_RESERVATION.CabinId);


                int tu;
                if (int.TryParse(Request["tourdays"].ToString(), out tu))
                {
                    ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS.Where(f => f.TourDaysId == tu), "Id", "Name", pRODUCT_RESERVATION.TourDaysId);
                }
                else
                {
                    ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS, "Id", "Name", pRODUCT_RESERVATION.TourDaysId);
                }

                ViewBag.ProductReservationTypeId = new SelectList(db.PRODUCT_RESERVATION_TYPE, "Id", "Name", pRODUCT_RESERVATION.ProductReservationTypeId);
                ViewBag.DiveId = new SelectList(db.DIVE, "Id", "Name", pRODUCT_RESERVATION.DiveId);

                return View(pRODUCT_RESERVATION);
            }
        }


        // GET: PRODUCT_RESERVATION/Edit/5
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Edit(int? id, int? ProductCalenderId, int? CategoryProductId, int? tourdays)
        {
            if (id == null)
            {
                return View(" Edit HttpNotFoundReservation", HttpStatusCode.BadRequest);
            }
            PRODUCT_RESERVATION pRODUCT_RESERVATION = db.PRODUCT_RESERVATION.Include(a => a.PRODUCT_CALENDER).Include(a => a.PRODUCT_CALENDER.CATEGORY_PRODUCT).Include(a => a.CABIN).Where(a => a.Id == id).First();
            if (pRODUCT_RESERVATION == null)
            {
                return View(" EditReservation Found", HttpStatusCode.BadRequest);            
            }

            int tu;
            if (int.TryParse(Request["tourdays"].ToString(), out tu))
            {
                ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS.Where(f => f.TourDaysId == tu), "Id", "Name", pRODUCT_RESERVATION.TourDaysId);
            }
            else
            {
                ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS, "Id", "Name", pRODUCT_RESERVATION.TourDaysId);
            }
            ViewBag.AgencyId = new SelectList(db.AGENCY, "Id", "Name", pRODUCT_RESERVATION.AgencyId);
            ViewBag.AgentId = new SelectList(db.AGENT, "ID", "FirstName", pRODUCT_RESERVATION.AgentId);
            ViewBag.PaxId = new SelectList(db.PAX, "Id", "First_Name", pRODUCT_RESERVATION.PaxId);
            ViewBag.PaymentTypeId = new SelectList(db.PAYMENT_TYPE, "Id", "Description", pRODUCT_RESERVATION.PaymentTypeId);
            ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDER, "Id", "Id", pRODUCT_RESERVATION.ProductCalenderId);
            ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDERHOUR, "Id", "Times", pRODUCT_RESERVATION.ProductCalenderId);
            ViewBag.PaymentStatusId = new SelectList(db.PAYMENT_STATUS, "Id", "Description", pRODUCT_RESERVATION.PaymentStatusId);
            ViewBag.ProductReservationTypeId = new SelectList(db.PRODUCT_RESERVATION_TYPE, "Id", "Name", pRODUCT_RESERVATION.ProductReservationTypeId);
            ViewBag.DiveId = new SelectList(db.DIVE, "Id", "Name", pRODUCT_RESERVATION.DiveId);

            var s = (from a in db.Cabin
                     from od in a.PRODUCT_RESERVATION.Where(r => r.ProductCalenderId == pRODUCT_RESERVATION.ProductCalenderId).DefaultIfEmpty()
                     where (a.Id != od.CabinId & a.CategoryProductId == pRODUCT_RESERVATION.PRODUCT_CALENDER.CategoryProductId)
                     select new
                     {
                         Id = a.Id,
                         Name = a.Name,
                         Description = a.Name + " " + a.Description,
                         CategoryProductId = a.CategoryProductId
                     })
                       ;
            ViewBag.CabinId = new SelectList(s, "Id", "Description", pRODUCT_RESERVATION.CabinId);
            return View(pRODUCT_RESERVATION);
        }

        // POST: PRODUCT_RESERVATION/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Edit([Bind(Include = "Id,ProductCalenderId,AgentId,PaymentTypeId,PaymentStatusId,AgencyId,Description,PaxId,Price,CreateDate,TourDaysId,ProductReservationTypeId,DivePrice,DiveId,PickUp,DropOff,Restrictions,Continua,CabinId,BloqueoDate,FacturaNr,NetoPrice")] PRODUCT_RESERVATION pRODUCT_RESERVATION)
        {
            PRODUCT_CALENDER p = db.PRODUCT_CALENDER.Find(pRODUCT_RESERVATION.ProductCalenderId);
            pRODUCT_RESERVATION.PRODUCT_CALENDER = p;

            string backtoList = Request.Params["BacktoList"] == null && Request.Params["Edit"] != null ? "Edit" : Request.Params["BacktoList"];
            if (backtoList.Equals("BacktoList"))
            {
                if (p == null)
                {
                    return View("HttpNotFoundReservation", HttpNotFound().StatusDescription);
                }
                return RedirectToAction("Index", new { ProductCalenderId = p.Id, CategoryProductId = p.CategoryProductId });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (pRODUCT_RESERVATION.PaxId == null)
                    {
                        var pax = db.SP_Insert_PaxDefault(pRODUCT_RESERVATION.ProductCalenderId.ToString());
                        pRODUCT_RESERVATION.PaxId = int.Parse(pax.ToString());
                    }
                    pRODUCT_RESERVATION.UserName = User.Identity.GetUserName();
                    pRODUCT_RESERVATION.PC_ID = System.Web.HttpContext.Current.Request.UserHostAddress == null ? HttpContext.Request.UserHostAddress : System.Web.HttpContext.Current.Request.UserHostAddress;

                    pRODUCT_RESERVATION.ProductReservationTypeId = pRODUCT_RESERVATION.ProductReservationTypeId == 0 ? 1 : pRODUCT_RESERVATION.ProductReservationTypeId;
                    pRODUCT_RESERVATION.CreateDate = pRODUCT_RESERVATION.CreateDate == null ? DateTime.Now.Date : pRODUCT_RESERVATION.CreateDate;
                    db.Entry(pRODUCT_RESERVATION).State = EntityState.Modified;

                    db.SaveChanges();
                    db.SP_Insert_CountReservation(pRODUCT_RESERVATION.ProductCalenderId);
                    db.SP_Insert_ReservationContinue(pRODUCT_RESERVATION.Id, pRODUCT_RESERVATION.ProductCalenderId, pRODUCT_RESERVATION.PRODUCT_CALENDER.CategoryProductId);

                    return RedirectToAction("Index", new { ProductCalenderId = pRODUCT_RESERVATION.ProductCalenderId });
                }

                ViewBag.AgencyId = new SelectList(db.AGENCY, "Id", "Name", pRODUCT_RESERVATION.AgencyId);
                ViewBag.AgentId = new SelectList(db.AGENT, "ID", "FirstName", pRODUCT_RESERVATION.AgentId);
                ViewBag.PaxId = new SelectList(db.PAX, "Id", "First_Name", pRODUCT_RESERVATION.PaxId);
                ViewBag.PaymentTypeId = new SelectList(db.PAYMENT_TYPE, "Id", "Description", pRODUCT_RESERVATION.PaymentTypeId);
                ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDER, "Id", "Id", pRODUCT_RESERVATION.ProductCalenderId);
                //ViewBag.ProductCalenderId = new SelectList(db.PRODUCT_CALENDERHOUR, "Id", "Times", pRODUCT_RESERVATION.ProductCalenderId);
                ViewBag.PaymentStatusId = new SelectList(db.PAYMENT_STATUS, "Id", "Description", pRODUCT_RESERVATION.PaymentStatusId);
                ViewBag.TourDaysId = new SelectList(db.TOUR_DAYS, "Id", "Name", pRODUCT_RESERVATION.TourDaysId);
                ViewBag.ProductReservationTypeId = new SelectList(db.PRODUCT_RESERVATION_TYPE, "Id", "Name", pRODUCT_RESERVATION.ProductReservationTypeId);
                ViewBag.DiveId = new SelectList(db.DIVE, "Id", "Name", pRODUCT_RESERVATION.DiveId);
                var s = (from a in db.Cabin
                         from od in a.PRODUCT_RESERVATION.Where(r => r.ProductCalenderId == pRODUCT_RESERVATION.ProductCalenderId).DefaultIfEmpty()
                         where (a.Id != od.CabinId & a.CategoryProductId == pRODUCT_RESERVATION.PRODUCT_CALENDER.CategoryProductId)
                         select new
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Description = a.Name + " " + a.Description,
                             CategoryProductId = a.CategoryProductId
                         })
                         ;
                ViewBag.CabinId = new SelectList(s, "Id", "Description", pRODUCT_RESERVATION.CabinId);
                return View(pRODUCT_RESERVATION);
            }
        }

        // GET: PRODUCT_RESERVATION/Delete/5
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_RESERVATION pRODUCT_RESERVATION = db.PRODUCT_RESERVATION.Find(id);
            if (pRODUCT_RESERVATION == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_RESERVATION);
        }

        // POST: PRODUCT_RESERVATION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Contador")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT_RESERVATION pRODUCT_RESERVATION = db.PRODUCT_RESERVATION.Find(id);
            var calenderId = pRODUCT_RESERVATION.ProductCalenderId;
            var categoryProductId = pRODUCT_RESERVATION.ProductCalenderId;

            string backtoList = Request.Params["BacktoList"] == null ? "" : Request.Params["BacktoList"];
            if (backtoList.Equals("BacktoList"))
            {
                PRODUCT_CALENDER p = db.PRODUCT_CALENDER.Find(pRODUCT_RESERVATION.ProductCalenderId);
                if (p == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Index", new { ProductCalenderId = p.Id, CategoryProductId = p.CategoryProductId });
            }
            else
            {
                db.PRODUCT_RESERVATION.Remove(pRODUCT_RESERVATION);
                db.Database.ExecuteSqlCommand("delete FROM [dbo].[PRODUCT_RESERVATION] where [BookId] = " + pRODUCT_RESERVATION.Id);

                if (pRODUCT_RESERVATION.PaxId <= 0)
                {
                    db.SP_Delete_PaxDefault(pRODUCT_RESERVATION.PaxId);
                }

                db.SaveChanges();
                db.SP_Insert_CountReservation(calenderId);
            }

            return RedirectToAction("Index", new { ProductCalenderId = calenderId, CategoryProductId = categoryProductId });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Excel(int pcaId)
        {
            var model = db.V_GetPaxList.Where(s => s.ProductCalenderId == pcaId).ToList().OrderByDescending(o => o.First_Name);
            Export export = new Export();
            export.ToExcel(Response, model);
        }
    }
    //helper class
    public class Export
    {
        public void ToExcel(HttpResponseBase Response, object clientsList)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = clientsList;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=FilePax.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }


}