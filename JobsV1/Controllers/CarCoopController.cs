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
    public class CarCoopController : Controller
    {

        private JobDBContainer db = new JobDBContainer();
        private CarReserve carRsv = new CarReserve();

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
            ViewBag.Title = "POTTMPC | Van, SUV/AUV/MPV, Sedan Rentals | Start Your Journey With Us! ";
            ViewBag.Description = @"Rent a Car company offering affordable selfdrive or with driver car rental service in Palawan, Philippines.
                 We offer -Grandia/Super/Premium, MPV / AUV and SUV for rent, Innova rentals, sedan rentals, 4x4 rentals, pickup rentals and van rentals in the City.
                 We offer daily, weekly, monthly rental and affordable rates for long term rentals.
                 We also partnered to several car rentals in Palawan for us to provide a reliable and quality service.
                 ";

            ViewBag.isAuthorize = HttpContext.User.Identity.Name == "" ? 0 : 1;
            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();
            ViewBag.Packages = db.CarRatePackages.ToList();

            ViewBag.NavBarTitle = "POTTMPC";
            ViewBag.Copyright = "RealBreeze Travel & Tours, Davao Philippines 8000";

            return View("Index", db.CarUnits.Include(c => c.CarRates).Include(m => m.CarUnitMetas).ToList());

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
            DateTime today = DateTime.Now;
            today = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(today, TimeZoneInfo.Local.Id, "Singapore Standard Time");

            //defaults
            CarReservation reservation = new CarReservation();
            reservation.DtTrx = today;
            reservation.DtStart = today.AddDays(2).ToString();
            reservation.DtEnd = today.AddDays(3).ToString();
            reservation.EstHrPerDay = 0;
            reservation.EstKmTravel = 0;
            reservation.JobRefNo = 0;
            reservation.SelfDrive = 1;  //with driver = 0, self drive = 1;

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
        public ActionResult Reservation([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel,JobRefNo,SelfDrive")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                db.CarReservations.Add(carReservation);
                db.SaveChanges();

                //self drive reservation
                addCarResPackage(carReservation.Id, 1, 0, 0);

                //sent email to POTTMPC@yahoo.com
                var adminEmail = "Reservation.realwheels@gmail.com";
                sendMail(carReservation.Id, adminEmail, "ADMIN", carReservation.RenterName);

                adminEmail = "POTTMPC@yahoo.com";
                sendMail(carReservation.Id, adminEmail, "ADMIN", carReservation.RenterName);

                //client email
                sendMail(carReservation.Id, carReservation.RenterEmail, "CLIENT-PENDING", carReservation.RenterName);

                return RedirectToAction("FormThankYou", new { rsvId = carReservation.Id });
                // return RedirectToAction("ReservationNotification");
            }

            return View("Reservation", new { unitid = carReservation.CarUnitId });
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
            if (ModelState.IsValid)
            {
                db.Entry(carrate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carrate);
        }

        public ActionResult LookupDestination(int cityid)
        {
            return View(db.CarDestinations.Where(d => d.CityId == cityid).OrderBy(d => d.Kms).ToList());
        }

        public PartialViewResult CarRate(int? unitid)
        {
            ViewBag.isAuthorize = HttpContext.User.Identity.Name == "" ? 0 : 1;
            ViewBag.data = 1;
            return PartialView("CarRate", db.CarRates.Where(d => d.CarUnitId == unitid));
        }

        public ActionResult CarReserve(int? id, int? days, int? rentType, int? meals, int? fuel)
        {
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", id);
            ViewBag.MealsAcc = new SelectList(MealsAcc, "Value", "Text", meals);
            ViewBag.Fuel = new SelectList(Fuel, "Value", "Text", fuel);
            ViewBag.CarUnitList = db.CarUnits.ToList().OrderBy(s => s.SortOrder);

            ViewBag.carid = (int)id == 0 ? 1 : id;
            ViewBag.days = days == null ? 1 : days;
            ViewBag.fuelId = fuel == null ? 1 : fuel;
            ViewBag.meals = meals == null ? 1 : meals;

            return View("CarReserve");
        }


        public PartialViewResult FormReservation()
        {
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            ViewBag.MealsAcc = new SelectList(MealsAcc, "Value", "Text");
            ViewBag.Fuel = new SelectList(Fuel, "Value", "Text");
            return PartialView("FormReservation");
        }

        public ActionResult FormPackages(int carId, int days, int rentType, int meals, int fuel)
        {

            int isAuthorize = HttpContext.User.Identity.Name == "" ? 0 : 1;
            ViewBag.PackageList = carRsv.getPackageList(carId, days, rentType, meals, fuel, isAuthorize);

            ViewBag.carId = carId;
            ViewBag.carDesc = db.CarUnits.Find(carId).Description;
            ViewBag.days = days;
            ViewBag.rentType = rentType;
            ViewBag.rentTypeTxt = getRentType(rentType);
            ViewBag.meals = meals;
            ViewBag.fuel = fuel;

            return View("FormPackages");
        }

        public ActionResult FormSummary(int carId, int days, int rentType, int meals, int fuel, int pkg)
        {
            int isAuthorize = HttpContext.User.Identity.Name == "" ? 0 : 1;
            fuel = fuel == 0 ? 0 : 1;
            meals = meals == 0 ? 0 : 1;
            PackageTable PackageSummary = carRsv.getPackageSummary(carId, days, rentType, meals, fuel, pkg, isAuthorize);

            ViewBag.carId = carId;
            ViewBag.carDesc = db.CarUnits.Find(carId).Description;
            ViewBag.days = days;
            ViewBag.RentType = rentType;
            ViewBag.RentTypeTxt = getRentType(rentType);
            ViewBag.meals = meals;
            ViewBag.fuel = fuel;
            ViewBag.pkg = pkg;

            ViewBag.Unit = db.CarUnits.Find(carId).Description;

            return View("FormSummary", PackageSummary);
        }

        public string getRentType(int rentType)
        {
            string rentText = "";
            switch (rentType)
            {
                case 1:
                    rentText = "With Driver";
                    break;
                case 2:
                    rentText = "Self Drive";
                    break;
                case 3:
                    rentText = "LongTerm";
                    break;
                default:
                    rentText = "With Driver";
                    break;
            }

            return rentText;
        }

        public ActionResult FormThankYou(int rsvId)
        {

            var carRsvr = db.CarReservations.Find(rsvId);
            ViewBag.rsvId = rsvId;
            ViewBag.CarDesc = carRsvr.CarUnit.Description;
            ViewBag.ReservationType = carRsvr.Destinations;
            ViewBag.Amount = carRsvr.BaseRate;

            return View();
        }

        public PartialViewResult MobileModalView()
        {
            return PartialView("MobileModalView");
        }

        // GET: CarReservations/Create
        public ActionResult FormRenter(int? id, int days, int rentType, int meals, int fuel, int pkg)
        {
            DateTime today = DateTime.Now;
            today = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(today, TimeZoneInfo.Local.Id, "Singapore Standard Time");

            int Authorize = HttpContext.User.Identity.Name == "" ? 0 : 1;
            PackageTable PackageSummary = carRsv.getPackageSummary((int)id, days, rentType, meals, fuel, pkg, Authorize);

            CarReservation reservation = new CarReservation();
            reservation.DtTrx = today;
            reservation.DtStart = today.AddDays(2).ToString();
            reservation.DtEnd = today.AddDays(3).ToString();
            reservation.JobRefNo = 0;
            reservation.SelfDrive = 0;  //with driver = 0, self drive = 1;
            reservation.EstHrPerDay = 10;
            reservation.EstKmTravel = 100;
            reservation.Destinations = db.CarRatePackages.Find(pkg).Description;
            reservation.UseFor = db.CarRatePackages.Find(pkg).Description;
            reservation.BaseRate = PackageSummary.Rate.ToString();

            //get previous id
            CarReservation lastId = db.CarReservations.ToList().OrderByDescending(c => c.Id).LastOrDefault();

            CarRatePackage selfDrive = db.CarRatePackages.Find(1);

            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", id);
            //get last reservation id
            ViewBag.RsvId = lastId != null ? lastId.Id + 1 : 1;
            ViewBag.id = id;
            ViewBag.fuel = fuel;
            ViewBag.meals = meals;
            ViewBag.pkgId = pkg;

            //except self drive package
            ViewBag.PackagesDesc = db.CarRatePackages.Find(pkg).Description;

            return View(reservation);
        }


        // POST: CarReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormRenter([Bind(Include = "Id,DtTrx,CarUnitId,DtStart,LocStart,DtEnd,LocEnd,BaseRate,Destinations,UseFor,RenterName,RenterCompany,RenterEmail,RenterMobile,RenterAddress,RenterFbAccnt,RenterLinkedInAccnt,EstHrPerDay,EstKmTravel,JobRefNo,SelfDrive")] CarReservation carReservation, int packageid, int mealAcc, int fuel, string host)
        {
            if (ModelState.IsValid)
            {
                db.CarReservations.Add(carReservation);
                db.SaveChanges();

                //add reservation package
                addCarResPackage(carReservation.Id, packageid, mealAcc, fuel);
                
                //sent email to POTTMPC@yahoo.com
                var adminEmail = "Reservation.realwheels@gmail.com";
                sendMail(carReservation.Id, adminEmail, "ADMIN", carReservation.RenterName);

                adminEmail = "POTTMPC@yahoo.com";
                sendMail(carReservation.Id, adminEmail, "ADMIN", carReservation.RenterName);

                //client email
                sendMail(carReservation.Id, carReservation.RenterEmail, "CLIENT-PENDING", carReservation.RenterName);
                
                return RedirectToAction("FormThankYou", new { rsvId = carReservation.Id });
            }

            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carReservation.CarUnitId);
            ViewBag.id = carReservation.CarUnitId;
            ViewBag.carRatesPackages = db.CarRateUnitPackages.ToList();
            ViewBag.CarUnitList = db.CarUnits.ToList();
            ViewBag.CarRates = db.CarRates.ToList();
            ViewBag.Packages = db.CarRatePackages.ToList();

            return View(carReservation);
        }


        public ActionResult CarDetail(int? unitid)
        {
            //car details
            ViewBag.Title = db.CarUnitMetas.Where(c => c.CarUnitId == unitid).FirstOrDefault().PageTitle;
            ViewBag.Description = db.CarUnitMetas.Where(c => c.CarUnitId == unitid).FirstOrDefault().MetaDesc;
            ViewBag.ImgSrc = db.CarImages.Where(c => c.CarUnitId == unitid).FirstOrDefault().ImgUrl;
            ViewBag.Id = unitid;

            //car rates
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

        public string sendMail(int jobId, string renterEmail, string mailType, string recipientName)
        {
            EMailHandler mail = new EMailHandler();

            string siteRedirect = "http://palawantransportcoop.com/reservation/";
            return mail.SendMail(jobId, renterEmail, mailType, recipientName, siteRedirect);
        }
        
        public ActionResult CarView(string carDesc)
        {
            switch (carDesc)
            {
                case "honda-city":
                    return View("~/Views/CarCoop/CarViews/HondaCity.cshtml");
                case "honda-city-2012":
                    return View("~/Views/CarCoop/CarViews/HondaCity2012.cshtml");
                case "ford-everest":
                    return View("~/Views/CarCoop/CarViews/FordEverest.cshtml");
                case "toyota-glgrandia":
                    return View("~/Views/CarCoop/CarViews/ToyotaGLGrandia.cshtml");
                case "toyota-innova":
                    return View("~/Views/CarCoop/CarViews/ToyotaInnova.cshtml");
                case "self-drive":
                    return View("~/Views/CarCoop/CarViews/HondaCity2012-SelfDrive.cshtml");
                case "minivan-sedan":
                    return View("~/Views/CarCoop/CarViews/MiniVanSedan.cshtml");
                case "rent-a-car":
                    return View("~/Views/CarCoop/CarViews/DavaoRentaCar.cshtml");
                case "toyota-avanza":
                    return View("~/Views/CarCoop/CarViews/ToyotaAvanza.cshtml");
                case "van-rental":
                    return View("~/Views/CarCoop/CarViews/VanRental.cshtml");

                //Page 2 start
                case "p2-innova-rental":
                    return View("~/Views/CarCoop/CarViews/ToyotaInnovaCarForRent.cshtml");
                case "innova-self-drive":
                    return View("~/Views/CarCoop/CarViews/RentCarSelfDrive.cshtml");
                case "ford-fiesta":
                    return View("~/Views/CarCoop/CarViews/FordFiesta.cshtml");
                case "honda-self-drive":
                    return View("~/Views/CarCoop/CarViews/HondaCityRental.cshtml");
                case "toyota-fortuner":
                    return View("~/Views/CarCoop/CarViews/ToyotaFortuner.cshtml");
                case "pickup":
                    return View("~/Views/CarCoop/CarViews/Pickup4x4.cshtml");

                //ads tag start
                case "tag-car-rental-davao":
                    return View("~/Views/CarCoop/CarViews/Tags/car-rental-davao.cshtml");
                case "tag-davao-rent-a-car":
                    return View("~/Views/CarCoop/CarViews/Tags/davao-rent-A-car.cshtml");
                case "rent-a-car-davao-city":
                    return View("~/Views/CarCoop/CarViews/Tags/rent-a-car-davao-city.cshtml");
                case "van-for-rent-davao-city":
                    return View("~/Views/CarCoop/CarViews/Tags/van-for-rent-davao-city.cshtml");

                //listing start
                case "sedan-listing":
                    return View("~/Views/CarCoop/CarViews/ListingSedan.cshtml");
                case "ads-listing":
                    return View("~/Views/CarCoop/CarViews/ListingAds.cshtml");
                case "ads-listing-others":
                    return View("~/Views/CarCoop/CarViews/ListingSUV.cshtml");
                case "ads-listing-vans":
                    return View("~/Views/CarCoop/CarViews/ListingVan.cshtml");
                case "ads-listing-mpv":
                    return View("~/Views/CarCoop/CarViews/ListingMPV.cshtml");
                case "ads-listing-pickup":
                    return View("~/Views/CarCoop/CarViews/ListingPickup.cshtml");
                case "ads-listing-page-2":
                    return View("~/Views/CarCoop/CarViews/ListingAds2.cshtml");
                default:
                    return View("~/Views/CarCoop/CarViews/ListingAds.cshtml");
            }
        }
    }
}