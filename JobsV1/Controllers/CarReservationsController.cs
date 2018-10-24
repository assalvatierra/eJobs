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
    public class CarReservationsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: CarReservations
        public ActionResult Index(int? filter)
        {

            ViewBag.PackageList = db.CarRateUnitPackages.ToList(); 
            var carReservations = db.CarReservations.Include(c => c.CarUnit).Include(c=>c.CarResPackages);
            var DateNow = DateTime.Now;
            var DateToday = DateNow.Date;


            DateTime today = DateTime.Today;
            today = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(today, TimeZoneInfo.Local.Id, "Singapore Standard Time");
            
            switch (filter)
            {
                case 1: //OnGoing
                    carReservations = carReservations.Where(c => DateTime.Compare(c.DtTrx, today) >= 0);   //get 1 month before all entries

                    break;
                case 2: //prev
                    carReservations = carReservations.Where(c => DateTime.Compare(c.DtTrx, today) < 0);

                    break;
                default:
                    carReservations = carReservations.Where(c => DateTime.Compare(c.DtTrx, today) >= 0);   //get 1 month before all entries
                    break;
            }
            
            return View(carReservations.ToList());
        }

        // GET: CarReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarReservation carReservation = db.CarReservations.Find(id);
            if (carReservation == null)
            {
                return HttpNotFound();
            }
            return View(carReservation);
        }

        // GET: CarReservations/Create
        public ActionResult Create(int? id)
        {
            
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", id);
            return View();
        }

        // POST: CarReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                db.CarReservations.Add(carReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carReservation.CarUnitId);
            return View(carReservation);
        }

        // GET: CarReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarReservation carReservation = db.CarReservations.Find(id);
            if (carReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carReservation.CarUnitId);
            return View(carReservation);
        }

        // POST: CarReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carReservation.CarUnitId);
            return View(carReservation);
        }

        // GET: CarReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarReservation carReservation = db.CarReservations.Find(id);
            if (carReservation == null)
            {
                return HttpNotFound();
            }
            return View(carReservation);
        }

        // POST: CarReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarReservation carReservation = db.CarReservations.Find(id);
            db.CarReservations.Remove(carReservation);
            DeleteCarResPackages(carReservation.Id);
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

        public void DeleteCarResPackages(int ReservationId) {
            CarResPackage carResReservation = db.CarResPackages.Where(c=>c.CarReservationId == ReservationId).FirstOrDefault();
            db.CarResPackages.Remove(carResReservation);
            db.SaveChanges();
        }
    }
}
