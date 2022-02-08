using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class ApplicationForTheConditionOfTheAircraftsController : Controller
    {
        private AirlineEntities db = new AirlineEntities();

        // GET: ApplicationForTheConditionOfTheAircrafts
        public ActionResult Index()
        {
            var applicationForTheConditionOfTheAircraft = db.ApplicationForTheConditionOfTheAircraft.Include(a => a.Plane).Include(a => a.Stuff).Include(a => a.Suppliers);
            return View(applicationForTheConditionOfTheAircraft.ToList());
        }

        // GET: ApplicationForTheConditionOfTheAircrafts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationForTheConditionOfTheAircraft applicationForTheConditionOfTheAircraft = db.ApplicationForTheConditionOfTheAircraft.Find(id);
            if (applicationForTheConditionOfTheAircraft == null)
            {
                return HttpNotFound();
            }
            return View(applicationForTheConditionOfTheAircraft);
        }

        // GET: ApplicationForTheConditionOfTheAircrafts/Create
        public ActionResult Create()
        {
            ViewBag.PlaneID = new SelectList(db.Plane, "PlaneID", "PlaneName");
            ViewBag.TechnicianID = new SelectList(db.Stuff, "StuffID", "Surname");
            ViewBag.TheSupplierID = new SelectList(db.Suppliers, "SupplierID", "Company");
            return View();
        }

        // POST: ApplicationForTheConditionOfTheAircrafts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,PlaneID,Status,Comment,TheSupplierID,TechnicianID,Company")] ApplicationForTheConditionOfTheAircraft applicationForTheConditionOfTheAircraft)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationForTheConditionOfTheAircraft.Add(applicationForTheConditionOfTheAircraft);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaneID = new SelectList(db.Plane, "PlaneID", "PlaneName", applicationForTheConditionOfTheAircraft.PlaneID);
            ViewBag.TechnicianID = new SelectList(db.Stuff, "StuffID", "Surname", applicationForTheConditionOfTheAircraft.TechnicianID);
            ViewBag.TheSupplierID = new SelectList(db.Suppliers, "SupplierID", "Company", applicationForTheConditionOfTheAircraft.TheSupplierID);
            return View(applicationForTheConditionOfTheAircraft);
        }

        // GET: ApplicationForTheConditionOfTheAircrafts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationForTheConditionOfTheAircraft applicationForTheConditionOfTheAircraft = db.ApplicationForTheConditionOfTheAircraft.Find(id);
            if (applicationForTheConditionOfTheAircraft == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaneID = new SelectList(db.Plane, "PlaneID", "PlaneName", applicationForTheConditionOfTheAircraft.PlaneID);
            ViewBag.TechnicianID = new SelectList(db.Stuff, "StuffID", "Surname", applicationForTheConditionOfTheAircraft.TechnicianID);
            ViewBag.TheSupplierID = new SelectList(db.Suppliers, "SupplierID", "Company", applicationForTheConditionOfTheAircraft.TheSupplierID);
            return View(applicationForTheConditionOfTheAircraft);
        }

        // POST: ApplicationForTheConditionOfTheAircrafts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationID,PlaneID,Status,Comment,TheSupplierID,TechnicianID,Company")] ApplicationForTheConditionOfTheAircraft applicationForTheConditionOfTheAircraft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationForTheConditionOfTheAircraft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaneID = new SelectList(db.Plane, "PlaneID", "PlaneName", applicationForTheConditionOfTheAircraft.PlaneID);
            ViewBag.TechnicianID = new SelectList(db.Stuff, "StuffID", "Surname", applicationForTheConditionOfTheAircraft.TechnicianID);
            ViewBag.TheSupplierID = new SelectList(db.Suppliers, "SupplierID", "Company", applicationForTheConditionOfTheAircraft.TheSupplierID);
            return View(applicationForTheConditionOfTheAircraft);
        }

        // GET: ApplicationForTheConditionOfTheAircrafts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationForTheConditionOfTheAircraft applicationForTheConditionOfTheAircraft = db.ApplicationForTheConditionOfTheAircraft.Find(id);
            if (applicationForTheConditionOfTheAircraft == null)
            {
                return HttpNotFound();
            }
            return View(applicationForTheConditionOfTheAircraft);
        }

        // POST: ApplicationForTheConditionOfTheAircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationForTheConditionOfTheAircraft applicationForTheConditionOfTheAircraft = db.ApplicationForTheConditionOfTheAircraft.Find(id);
            db.ApplicationForTheConditionOfTheAircraft.Remove(applicationForTheConditionOfTheAircraft);
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
