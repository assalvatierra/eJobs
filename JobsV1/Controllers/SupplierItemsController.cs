using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsV1.Models;

namespace JobsV1.Controllers
{
    public class SupplierItemsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: SupplierItems
        public ActionResult Index()
        {
            var supplierItems = db.SupplierItems.Include(s => s.Supplier);
            return View(supplierItems.ToList());
        }

        // GET: SupplierItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierItem supplierItem = db.SupplierItems.Find(id);
            if (supplierItem == null)
            {
                return HttpNotFound();
            }
            return View(supplierItem);
        }

        // GET: SupplierItems/Create
        public ActionResult Create()
        {
            SupplierItem sup = new SupplierItem();
            sup.Status = "ACT";

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View(sup);
        }

        // POST: SupplierItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Remarks,SupplierId,InCharge,Tel1,Tel2,Tel3,Status")] SupplierItem supplierItem)
        {
            if (ModelState.IsValid)
            {
                db.SupplierItems.Add(supplierItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", supplierItem.SupplierId);
            return View(supplierItem);
        }

        // GET: SupplierItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierItem supplierItem = db.SupplierItems.Find(id);
            if (supplierItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", supplierItem.SupplierId);
            return View(supplierItem);
        }

        // POST: SupplierItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Remarks,SupplierId,InCharge,Tel1,Tel2,Tel3,Status")] SupplierItem supplierItem)
        {
            if (ModelState.IsValid)
            {       
                db.Entry(supplierItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", supplierItem.SupplierId);
            return View(supplierItem);
        }

        // GET: SupplierItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierItem supplierItem = db.SupplierItems.Find(id);
            if (supplierItem == null)
            {
                return HttpNotFound();
            }
            return View(supplierItem);
        }

        // POST: SupplierItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierItem supplierItem = db.SupplierItems.Find(id);
            db.SupplierItems.Remove(supplierItem);
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
