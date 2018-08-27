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
    public class InvCarRecordsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: InvCarRecords
        public ActionResult Index()
        {
            var invCarRecords = db.InvCarRecords.Include(i => i.InvCarRecordType).Include(i => i.InvItem);
            return View(invCarRecords.ToList());
        }

        // GET: InvCarRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvCarRecord invCarRecord = db.InvCarRecords.Find(id);
            if (invCarRecord == null)
            {
                return HttpNotFound();
            }
            return View(invCarRecord);
        }

        // GET: InvCarRecords/Create
        public ActionResult Create()
        {
            ViewBag.InvCarRecordTypeId = new SelectList(db.InvCarRecordTypes, "Id", "Description");
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode");
            return View();
        }

        // POST: InvCarRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvItemId,InvCarRecordTypeId,Odometer,dtDone,NextOdometer,NextSched,Remarks")] InvCarRecord invCarRecord)
        {
            if (ModelState.IsValid)
            {
                db.InvCarRecords.Add(invCarRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvCarRecordTypeId = new SelectList(db.InvCarRecordTypes, "Id", "Description", invCarRecord.InvCarRecordTypeId);
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode", invCarRecord.InvItemId);
            return View(invCarRecord);
        }

        // GET: InvCarRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvCarRecord invCarRecord = db.InvCarRecords.Find(id);
            if (invCarRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvCarRecordTypeId = new SelectList(db.InvCarRecordTypes, "Id", "Description", invCarRecord.InvCarRecordTypeId);
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode", invCarRecord.InvItemId);
            return View(invCarRecord);
        }

        // POST: InvCarRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvItemId,InvCarRecordTypeId,Odometer,dtDone,NextOdometer,NextSched,Remarks")] InvCarRecord invCarRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invCarRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvCarRecordTypeId = new SelectList(db.InvCarRecordTypes, "Id", "Description", invCarRecord.InvCarRecordTypeId);
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode", invCarRecord.InvItemId);
            return View(invCarRecord);
        }

        // GET: InvCarRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvCarRecord invCarRecord = db.InvCarRecords.Find(id);
            if (invCarRecord == null)
            {
                return HttpNotFound();
            }
            return View(invCarRecord);
        }

        // POST: InvCarRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvCarRecord invCarRecord = db.InvCarRecords.Find(id);
            db.InvCarRecords.Remove(invCarRecord);
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
