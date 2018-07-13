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
    public class CustEntitiesController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: CustEntities
        public ActionResult Index()
        {
            var custEntities = db.CustEntities.Include(c => c.CustEntMain).Include(c => c.Customer);
            return View(custEntities.ToList());
        }

        // GET: CustEntities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustEntity custEntity = db.CustEntities.Find(id);
            if (custEntity == null)
            {
                return HttpNotFound();
            }
            return View(custEntity);
        }

        // GET: CustEntities/Create
        public ActionResult Create()
        {
            ViewBag.CustEntMainId = new SelectList(db.CustEntMains, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: CustEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustEntMainId,CustomerId")] CustEntity custEntity)
        {
            if (ModelState.IsValid)
            {
                db.CustEntities.Add(custEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustEntMainId = new SelectList(db.CustEntMains, "Id", "Name", custEntity.CustEntMainId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", custEntity.CustomerId);
            return View(custEntity);
        }

        // GET: CustEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustEntity custEntity = db.CustEntities.Find(id);
            if (custEntity == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustEntMainId = new SelectList(db.CustEntMains, "Id", "Name", custEntity.CustEntMainId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", custEntity.CustomerId);
            return View(custEntity);
        }

        // POST: CustEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustEntMainId,CustomerId")] CustEntity custEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustEntMainId = new SelectList(db.CustEntMains, "Id", "Name", custEntity.CustEntMainId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", custEntity.CustomerId);
            return View(custEntity);
        }

        // GET: CustEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustEntity custEntity = db.CustEntities.Find(id);
            if (custEntity == null)
            {
                return HttpNotFound();
            }
            return View(custEntity);
        }

        // POST: CustEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustEntity custEntity = db.CustEntities.Find(id);
            db.CustEntities.Remove(custEntity);
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
