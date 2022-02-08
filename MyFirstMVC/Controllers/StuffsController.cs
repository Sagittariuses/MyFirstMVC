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
    public class StuffsController : Controller
    {
        private AirlineEntities db = new AirlineEntities();

        // GET: Stuffs
        public ActionResult Index()
        {
            var stuff = db.Stuff.Include(s => s.Posts);
            return View(stuff.ToList());
        }

        // GET: Stuffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuff stuff = db.Stuff.Find(id);
            if (stuff == null)
            {
                return HttpNotFound();
            }
            return View(stuff);
        }

        // GET: Stuffs/Create
        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.Posts, "PostId", "PostName");
            return View();
        }

        // POST: Stuffs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StuffID,Surname,Name,Patronymic,Gender,PhoneNumber,Email,RegistrationDate,BirthdayDate,PostID")] Stuff stuff)
        {
            if (ModelState.IsValid)
            {
                db.Stuff.Add(stuff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.Posts, "PostId", "PostName", stuff.PostID);
            return View(stuff);
        }

        // GET: Stuffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuff stuff = db.Stuff.Find(id);
            if (stuff == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostId", "PostName", stuff.PostID);
            return View(stuff);
        }

        // POST: Stuffs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StuffID,Surname,Name,Patronymic,Gender,PhoneNumber,Email,RegistrationDate,BirthdayDate,PostID")] Stuff stuff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stuff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostId", "PostName", stuff.PostID);
            return View(stuff);
        }

        // GET: Stuffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuff stuff = db.Stuff.Find(id);
            if (stuff == null)
            {
                return HttpNotFound();
            }
            return View(stuff);
        }

        // POST: Stuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stuff stuff = db.Stuff.Find(id);
            db.Stuff.Remove(stuff);
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
