﻿using System;
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

            SupplierPoDtl supplier = db.SupplierPoDtls.Where(s => s.SupplierPoHdrId == hdrId).FirstOrDefault();

            List<SupplierPoItem> supItems = db.SupplierPoItems.Where(s => s.SupplierPoDtlId == supplier.Id).ToList();
            if (supItems == null) {
                supItems.Add(new SupplierPoItem {
                    Id = 0,
                    InvItem = null,
                    InvItemId = 0, 
                    SupplierPoDtl = null,
                    SupplierPoDtlId = 0,
                });
            }

            List<InvItem> invItems = db.InvItems.ToList();
            if (invItems == null)
            {
                invItems.Add(new InvItem
                {
                    Id = 0,
                    
                });
            }

            ViewBag.HdrId = hdrId;
            ViewBag.Id = supplier.Id;
            ViewBag.supplierPoItems = supItems;
            ViewBag.InvItemsList = invItems;
            
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
        public ActionResult Create(int? hdrid)
        {
            ViewBag.Hdrid = hdrid;
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

        public ActionResult ItemCreate(int? dtlsId) {
            ViewBag.Id = dtlsId;
            ViewBag.SupplierPoDtlId = new SelectList(db.SupplierPoDtls, "Id", "Id", dtlsId);
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "Description");
            return View();
        }

        // POST: SupplierPoDtls/ItemCreate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemCreate([Bind(Include = "Id,SupplierPoDtlId,InvItemId")] SupplierPoItem supplierPoItem)
        {
            if (ModelState.IsValid)
            {
                db.SupplierPoItems.Add(supplierPoItem);
                db.SaveChanges();

                var sup = db.SupplierPoDtls.Find(supplierPoItem.SupplierPoDtlId);

                return RedirectToAction("Index",new { hdrId = sup.SupplierPoHdrId});
            }

            ViewBag.SupplierPoDtlId = new SelectList(db.SupplierPoDtls, "Id", "Id", supplierPoItem.SupplierPoDtlId);
            ViewBag.InvItems = new SelectList(db.InvItems, "Id", "Description", supplierPoItem.InvItemId);
            return View(supplierPoItem);
        }


        // POST: SupplierPoDtls/Remove/SupPOItemId=1
        public ActionResult Remove(int SupPOItemId)
        {
            SupplierPoItem supPOItem = db.SupplierPoItems.Find(SupPOItemId);
            db.SupplierPoItems.Remove(supPOItem);
            db.SaveChanges();

            var sup = db.SupplierPoDtls.Find(supPOItem.SupplierPoDtlId);

            return RedirectToAction("Index", "SupplierPoDtls", new { hdrId = sup.SupplierPoHdrId });
        }


        public ActionResult addInvItem(int InvID, int DtlsId)
        {

                db.SupplierPoItems.Add(new SupplierPoItem
                {
                    SupplierPoDtlId = DtlsId,
                    InvItemId = InvID
                });
                
                db.SaveChanges();

            var sup = db.SupplierPoDtls.Find(DtlsId);
            return RedirectToAction("Index", "SupplierPoDtls", new { hdrId = sup.SupplierPoHdrId });
           
        }

    }
}
