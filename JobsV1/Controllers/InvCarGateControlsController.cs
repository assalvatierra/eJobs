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
    public class InvCarGateControlsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: InvCarGateControls
        public ActionResult Index()
        {
            var invCarGateControls = db.InvCarGateControls.Include(i => i.InvItem);
            return View(invCarGateControls.ToList());
        }

        // GET: InvCarGateControls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvCarGateControl invCarGateControl = db.InvCarGateControls.Find(id);
            if (invCarGateControl == null)
            {
                return HttpNotFound();
            }
            return View(invCarGateControl);
        }

        // GET: InvCarGateControls/Create
        public ActionResult Create(int? control)
        {
            InvCarGateControl invcar = new InvCarGateControl();
            invcar.In_Out_flag = (int)control;
            invcar.dtControl = DateTime.Now;

            if (control == null)
            {
                invcar.In_Out_flag = 1;
            }
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode");
            return View(invcar);
        }

        // POST: InvCarGateControls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvItemId,In_Out_flag,Odometer,dtControl,Remarks,Driver,Inspector")] InvCarGateControl invCarGateControl)
        {
            if (ModelState.IsValid)
            {
                db.InvCarGateControls.Add(invCarGateControl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode", invCarGateControl.InvItemId);
            return View(invCarGateControl);
        }

        // GET: InvCarGateControls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvCarGateControl invCarGateControl = db.InvCarGateControls.Find(id);
            if (invCarGateControl == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode", invCarGateControl.InvItemId);
            return View(invCarGateControl);
        }

        // POST: InvCarGateControls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvItemId,In_Out_flag,Odometer,dtControl,Remarks,Driver,Inspector")] InvCarGateControl invCarGateControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invCarGateControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvItemId = new SelectList(db.InvItems, "Id", "ItemCode", invCarGateControl.InvItemId);
            return View(invCarGateControl);
        }

        // GET: InvCarGateControls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvCarGateControl invCarGateControl = db.InvCarGateControls.Find(id);
            if (invCarGateControl == null)
            {
                return HttpNotFound();
            }
            return View(invCarGateControl);
        }

        // POST: InvCarGateControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvCarGateControl invCarGateControl = db.InvCarGateControls.Find(id);
            db.InvCarGateControls.Remove(invCarGateControl);
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
