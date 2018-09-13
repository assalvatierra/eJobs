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
    public class CarRentalController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: CarRental
        public ActionResult Index()
        {
            return View("Index", db.CarUnits.Include(c => c.CarRates).ToList() );
        }
        public ActionResult MainImage(int? id)
        {
            var dir = Server.MapPath("/Images/CarRental");
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
            var dir = Server.MapPath("/Images/CarRental");
            var imgFileName = "PlaceHolder.png";
            var car = db.CarUnits.Find(id);
            var carimg = car.CarImages.Where(d => d.SysCode == "UNIT").FirstOrDefault();
            if (carimg != null)
                imgFileName = carimg.ImgUrl;

            var path = System.IO.Path.Combine(dir, imgFileName);
            return base.File(path, "image/jpeg");
        }

        public ActionResult Reservation(int unitid)
        {
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", unitid);

            var carrate = db.CarRates.Where(d => d.CarUnitId == unitid).FirstOrDefault();
            ViewBag.CarRate = carrate.Daily;
            ViewBag.objCarRate = carrate; // db.CarRates.Where(d => d.CarUnitId == unitid);

            ViewBag.Destinations = db.CarDestinations.Where(d => d.CityId == 1).OrderBy(d => d.Kms).ToList();
            ViewBag.CarUnitId = unitid;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                db.CarReservations.Add(carReservation);
                db.SaveChanges();
                return RedirectToAction("ReservationNotification");
            }

            return View("carReservation", new { unitid = carReservation.CarUnitId } );
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

<<<<<<< HEAD

        public ActionResult CarDetail(int? unitid)
        {
            string view = "CarDetail_temp";

            switch ((int)unitid) {
                case 1:
                    view = "CarDetail_van";
                    break;
                case 2:
                    view = "CarDetail_suv";
                    break;
                case 3:
                    view = "CarDetail_mpv";
                    break;
                case 4:
                    view = "CarDetail_sedan";
                    break;
                case 5:
                    view = "CarDetail_pickup";
                    break;

            }

            return PartialView(view, db.CarUnits.Where(d => d.Id == unitid).FirstOrDefault());
        }
=======
        public ActionResult CarDetail(int? unitid)
        {
            return View("CarDetail_temp");
        }

>>>>>>> origin/master
    }
}