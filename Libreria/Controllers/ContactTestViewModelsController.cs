using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Libreria.Models.ContactTestModel;
using Libreria.ViewModels;

namespace Libreria.Controllers
{
    public class ContactTestViewModelsController : Controller
    {
        private ContactTestDataModel db = new ContactTestDataModel();

        // GET: ContactTestViewModels
        public ActionResult Index()
        {
            return View(db.ContactTestViewModels.ToList());
        }

        // GET: ContactTestViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactTestViewModel contactTestViewModel = db.ContactTestViewModels.Find(id);
            if (contactTestViewModel == null)
            {
                return HttpNotFound();
            }
            return View(contactTestViewModel);
        }

        // GET: ContactTestViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactTestViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,CategoryName,SupplierName")] ContactTestViewModel contactTestViewModel)
        {
            if (ModelState.IsValid)
            {
                db.ContactTestViewModels.Add(contactTestViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactTestViewModel);
        }

        // GET: ContactTestViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactTestViewModel contactTestViewModel = db.ContactTestViewModels.Find(id);
            if (contactTestViewModel == null)
            {
                return HttpNotFound();
            }
            return View(contactTestViewModel);
        }

        // POST: ContactTestViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,CategoryName,SupplierName")] ContactTestViewModel contactTestViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactTestViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactTestViewModel);
        }

        // GET: ContactTestViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactTestViewModel contactTestViewModel = db.ContactTestViewModels.Find(id);
            if (contactTestViewModel == null)
            {
                return HttpNotFound();
            }
            return View(contactTestViewModel);
        }

        // POST: ContactTestViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactTestViewModel contactTestViewModel = db.ContactTestViewModels.Find(id);
            db.ContactTestViewModels.Remove(contactTestViewModel);
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
