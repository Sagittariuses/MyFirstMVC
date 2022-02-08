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
    public class PlanesController : Controller
    {
        private AirlineEntities db = new AirlineEntities();

        // GET: Planes
        public ActionResult Index()
        {
            var plane = db.Plane.Include(p => p.Suppliers).Include(p => p.TicketInfo);
            return View(plane.ToList());
        }

        // GET: Planes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Plane.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            return View(plane);
        }

        // GET: Planes/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierID", "Company");
            ViewBag.PlaneID = new SelectList(db.TicketInfo, "TicketID", "PlaceOfDeparture");
            return View();
        }

        // POST: Planes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaneID,PlaneName,Model,QuantityOfSeats,NumberOfUnitsInThePark,Type,Length,Wingspan,CruisingSpeed,MaximumFlightAltitude,MaximumFlightRange,SupplierId")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                db.Plane.Add(plane);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierID", "Company", plane.SupplierId);
            ViewBag.PlaneID = new SelectList(db.TicketInfo, "TicketID", "PlaceOfDeparture", plane.PlaneID);
            return View(plane);
        }

        // GET: Planes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Plane.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierID", "Company", plane.SupplierId);
            ViewBag.PlaneID = new SelectList(db.TicketInfo, "TicketID", "PlaceOfDeparture", plane.PlaneID);
            return View(plane);
        }

        // POST: Planes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaneID,PlaneName,Model,QuantityOfSeats,NumberOfUnitsInThePark,Type,Length,Wingspan,CruisingSpeed,MaximumFlightAltitude,MaximumFlightRange,SupplierId")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierID", "Company", plane.SupplierId);
            ViewBag.PlaneID = new SelectList(db.TicketInfo, "TicketID", "PlaceOfDeparture", plane.PlaneID);
            return View(plane);
        }

        // GET: Planes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Plane.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            return View(plane);
        }

        // POST: Planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plane plane = db.Plane.Find(id);
            db.Plane.Remove(plane);
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
