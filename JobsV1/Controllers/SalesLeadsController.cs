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
    public class SalesLeadsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: SalesLeads
        public ActionResult Index()
        {
            var salesLeads = db.SalesLeads.Include(s => s.Customer);
            return View(salesLeads.ToList());
        }

        // GET: SalesLeads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLead salesLead = db.SalesLeads.Find(id);
            if (salesLead == null)
            {
                return HttpNotFound();
            }
            return View(salesLead);
        }

        // GET: SalesLeads/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: SalesLeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Details,Remarks,CustomerId,CustName")] SalesLead salesLead)
        {
            if (ModelState.IsValid)
            {
                db.SalesLeads.Add(salesLead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", salesLead.CustomerId);
            return View(salesLead);
        }

        // GET: SalesLeads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLead salesLead = db.SalesLeads.Find(id);
            if (salesLead == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", salesLead.CustomerId);
            return View(salesLead);
        }

        // POST: SalesLeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Details,Remarks,CustomerId,CustName")] SalesLead salesLead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesLead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", salesLead.CustomerId);
            return View(salesLead);
        }

        // GET: SalesLeads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLead salesLead = db.SalesLeads.Find(id);
            if (salesLead == null)
            {
                return HttpNotFound();
            }
            return View(salesLead);
        }

        // POST: SalesLeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesLead salesLead = db.SalesLeads.Find(id);
            db.SalesLeads.Remove(salesLead);
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
