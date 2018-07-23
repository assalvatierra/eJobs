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
    public class SupplierPoDtlsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: SupplierPoDtls
        public ActionResult Index(int? hdrId)
        {
            var supplierPoDtls = db.SupplierPoDtls.Include(s => s.SupplierPoHdr).Include(s => s.JobService).Where(d=>d.SupplierPoHdrId== (int)hdrId);
            return View(supplierPoDtls.ToList());
        }

        // GET: SupplierPoDtls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierPoDtl supplierPoDtl = db.SupplierPoDtls.Find(id);
            if (supplierPoDtl == null)
            {
                return HttpNotFound();
            }
            return View(supplierPoDtl);
        }

        // GET: SupplierPoDtls/Create
        public ActionResult Create()
        {
            ViewBag.SupplierPoHdrId = new SelectList(db.SupplierPoHdrs, "Id", "Remarks");
            ViewBag.JobServicesId = new SelectList(db.JobServices, "Id", "Particulars");
            return View();
        }

        // POST: SupplierPoDtls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierPoHdrId,Remarks,Amount,JobServicesId")] SupplierPoDtl supplierPoDtl)
        {
            if (ModelState.IsValid)
            {
                db.SupplierPoDtls.Add(supplierPoDtl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierPoHdrId = new SelectList(db.SupplierPoHdrs, "Id", "Remarks", supplierPoDtl.SupplierPoHdrId);
            ViewBag.JobServicesId = new SelectList(db.JobServices, "Id", "Particulars", supplierPoDtl.JobServicesId);
            return View(supplierPoDtl);
        }

        // GET: SupplierPoDtls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierPoDtl supplierPoDtl = db.SupplierPoDtls.Find(id);
            if (supplierPoDtl == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierPoHdrId = new SelectList(db.SupplierPoHdrs, "Id", "Remarks", supplierPoDtl.SupplierPoHdrId);
            ViewBag.JobServicesId = new SelectList(db.JobServices, "Id", "Particulars", supplierPoDtl.JobServicesId);
            return View(supplierPoDtl);
        }

        // POST: SupplierPoDtls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierPoHdrId,Remarks,Amount,JobServicesId")] SupplierPoDtl supplierPoDtl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierPoDtl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { hdrId = supplierPoDtl.SupplierPoHdrId } );
            }
            ViewBag.SupplierPoHdrId = new SelectList(db.SupplierPoHdrs, "Id", "Remarks", supplierPoDtl.SupplierPoHdrId);
            ViewBag.JobServicesId = new SelectList(db.JobServices, "Id", "Particulars", supplierPoDtl.JobServicesId);
            return View(supplierPoDtl);
        }

        // GET: SupplierPoDtls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierPoDtl supplierPoDtl = db.SupplierPoDtls.Find(id);
            if (supplierPoDtl == null)
            {
                return HttpNotFound();
            }
            return View(supplierPoDtl);
        }

        // POST: SupplierPoDtls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierPoDtl supplierPoDtl = db.SupplierPoDtls.Find(id);
            db.SupplierPoDtls.Remove(supplierPoDtl);
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
