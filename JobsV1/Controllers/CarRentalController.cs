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
    public class CarRentalController : Controller
    {
        private JobDBContainer db = new JobDBContainer();
     

        private List<SelectListItem> MealsAcc = new List<SelectListItem> {
                new SelectListItem { Value = "1", Text =  "Driver Meals and Accomodation included" },
                new SelectListItem { Value = "0", Text =  "Will Provide Meals and Accomodation" }
                };

        private List<SelectListItem> Fuel = new List<SelectListItem> {
                new SelectListItem { Value = "1", Text = "Fuel Included in the Package" },
                new SelectListItem { Value = "0", Text = "Will Provide Fuel"  }
                };

        // GET: CarRental
        public ActionResult Index()
        {
            ViewBag.Title = "Davao Car Rental | Van, SUV/AUV/MPV, Sedan Rentals | Real Wheels Rent A Car Davao | Start Your Journey With Us! ";
            ViewBag.Description = @"Rent a Car company offering affordable selfdrive or with driver car rental service in Davao City, Philippines.
                 We offer -Grandia/Super/Premium, MPV / AUV and SUV for rent, Innova rentals, sedan rentals, 4x4 rentals, pickup rentals and van rentals in the City.
                 We offer daily, weekly, monthly rental and affordable rates for long term rentals.
                 We also partnered to several car rentals in Davao for us to provide a reliable and quality service.
                 ";

            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();
            ViewBag.Packages = db.CarRatePackages.ToList();
            return View("Index", db.CarUnits.Include(c => c.CarRates).Include(m=>m.CarUnitMetas).ToList() );

        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult MainImage(int? id)
        {
            var dir = Server.MapPath("~/Images/CarRental");
            var imgFileName = "PlaceHolder.png";
            var car = db.CarUnits.Find(id);
            var carimg = car.CarImages.Where(d => d.SysCode == "MAIN").FirstOrDefault();
            if (carimg != null)
                imgFileName = carimg.ImgUrl;

            var path = System.IO.Path.Combine(dir, imgFileName);
            return base.File(path, "image/jpeg");
        }
        public ActionResult UnitImage(int? id)
        {
            var dir = Server.MapPath("~/Images/CarRental");
            var imgFileName = "PlaceHolder.png";
            var car = db.CarUnits.Find(id);
            var carimg = car.CarImages.Where(d => d.SysCode == "MAIN").FirstOrDefault();
            if (carimg != null)
                imgFileName = carimg.ImgUrl;

            var path = System.IO.Path.Combine(dir, imgFileName);
            return base.File(path, "image/jpeg");
        }

        public ActionResult Reservation(int unitid)
        {

            CarReservation reservation = new CarReservation();
            reservation.DtTrx = DateTime.Now;
            reservation.DtStart = DateTime.Now.ToString();
            reservation.DtEnd = DateTime.Now.AddDays(2).ToString();
            reservation.EstHrPerDay = 0;
            reservation.EstKmTravel = 0;

            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", unitid);

            var carrate = db.CarRates.Where(d => d.CarUnitId == unitid).FirstOrDefault();
            ViewBag.CarRate = carrate.Daily;
            ViewBag.objCarRate = carrate; // db.CarRates.Where(d => d.CarUnitId == unitid);

            ViewBag.Destinations = db.CarDestinations.Where(d => d.CityId == 1).OrderBy(d => d.Kms).ToList();
            ViewBag.UnitId = unitid;
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                db.CarReservations.Add(carReservation);
                db.SaveChanges();

                addCarResPackage(carReservation.Id, 1, 0, 0);
                return RedirectToAction("ReservationNotification");
            }

            return View("Reservation", new { unitid = carReservation.CarUnitId } );
        }

        
        public ActionResult ReservationNotification()
        {
            return View();
        }

        public ActionResult RateConfig(int unitid)
        {
            Models.CarRate unitrate = db.CarRates.Where(d => d.CarUnitId == unitid).SingleOrDefault();
            if (unitrate == null)
            {
                unitrate = new Models.CarRate();
                unitrate.CarUnitId = unitid;
                db.Entry(unitrate).State = EntityState.Added;
                db.SaveChanges();
            }
            ViewBag.Unit = db.CarUnits.Find(unitid);
            return View(unitrate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateConfig([Bind(Include = "Id,Daily,Weekly,Monthly,KmFree,KmRate,CarUnitId,OtRate")] CarRate carrate)
        {
            if(ModelState.IsValid)
            {
                db.Entry(carrate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carrate);
        }

        public ActionResult LookupDestination(int cityid)
        {
            return View(db.CarDestinations.Where(d=>d.CityId== cityid).OrderBy(d=>d.Kms).ToList());
        }

        public PartialViewResult CarRate(int? unitid)
        {
            return PartialView("CarRate", db.CarRates.Where(d => d.CarUnitId == unitid));
        }

        public PartialViewResult CarReserve()
        {
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            ViewBag.MealsAcc = new SelectList(MealsAcc, "Value", "Text");
            ViewBag.Fuel = new SelectList(Fuel, "Value", "Text");
            ViewBag.CarUnitList = db.CarUnits.ToList();
            return PartialView("CarReserve");
        }


        public PartialViewResult FormReservation()
        {
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            ViewBag.MealsAcc = new SelectList(MealsAcc, "Value", "Text");
            ViewBag.Fuel = new SelectList(Fuel, "Value", "Text");
            return PartialView("FormReservation");
        }

        public PartialViewResult FormPackages() {

            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            ViewBag.Packages = db.CarRatePackages.ToList();
            return PartialView("FormPackages");
        }

        public PartialViewResult FormSummary()
        {
            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            ViewBag.Packages = db.CarRatePackages.ToList();
            return PartialView("FormSummary");
        }
        
        public ActionResult FormThankYou(int rsvId) {
            ViewBag.rsvId = rsvId;
            return View();
        }

        // GET: CarReservations/Create
        public ActionResult FormRenter(int? id)
        {
            CarReservation reservation = new CarReservation();
            reservation.DtTrx = DateTime.Now;
            reservation.DtStart = DateTime.Now.ToString();
            reservation.DtEnd = DateTime.Now.AddDays(2).ToString();

            //get previous id
            CarReservation lastId = db.CarReservations.ToList().OrderByDescending(c => c.Id).LastOrDefault();

            CarRatePackage selfDrive = db.CarRatePackages.Find(1);
            
            ViewBag.RsvId = lastId != null ?  lastId.Id + 1 : 1 ;
            
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", id);
            ViewBag.id = id;
            ViewBag.carRatesPackages = db.CarRateUnitPackages.ToList();
            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();

            //except self drive package
            ViewBag.Packages = db.CarRatePackages.ToList();
            return View(reservation);
        }

        // POST: CarReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormRenter([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel")] CarReservation carReservation, int packageid, int mealAcc, int fuel)
        {
            if (ModelState.IsValid)
            {
                db.CarReservations.Add(carReservation);
                db.SaveChanges();

                addCarResPackage(carReservation.Id, packageid, mealAcc, fuel);
                return RedirectToAction("FormThankYou", new { rsvId = carReservation.Id });
            }

            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carReservation.CarUnitId);
            
            ViewBag.id = carReservation.CarUnitId;
            ViewBag.carRatesPackages = db.CarRateUnitPackages.ToList();
            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();
            //except self drive package
            ViewBag.Packages = db.CarRatePackages.ToList();
            return View(carReservation);
        }


        public ActionResult CarDetail(int? unitid)
        {
            //add title and description 
            ViewBag.Title =  db.CarUnitMetas.Where(c=>c.CarUnitId == unitid).FirstOrDefault().PageTitle;
            ViewBag.Description = db.CarUnitMetas.Where(c => c.CarUnitId == unitid).FirstOrDefault().MetaDesc;
            ViewBag.ImgSrc = db.CarImages.Where(c => c.CarUnitId == unitid).FirstOrDefault().ImgUrl;
            ViewBag.Id = unitid;
            var car = db.CarRates.Where(c => c.CarUnitId == unitid).FirstOrDefault();
            ViewBag.dailyRate = car.Daily;
            ViewBag.weeklyRate = car.Weekly;
            ViewBag.monthlyRate = car.Monthly;
            var carUnitView = db.CarViewPages.Where(s => s.CarUnitId == unitid).FirstOrDefault();
            return View(carUnitView.Viewname, db.CarUnits.Where(d => d.Id == unitid).FirstOrDefault());
        }
        
        public ActionResult ReservationRequest()
        {
            return View();
        }

       
        public void addCarResPackage(int CarReservationId, int packageid, int mealsAcc, int fuel)
        {
            CarResPackage packages = new CarResPackage();
            packages.CarRateUnitPackageId = packageid;
            packages.CarReservationId = CarReservationId;
            packages.DrvMealByClient = mealsAcc;
            packages.FuelByClient = fuel;
            packages.DrvRoomByClient = mealsAcc;
            db.CarResPackages.Add(packages);
            db.SaveChanges();
            
        }



    }
}