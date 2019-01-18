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
    
    public class CarRateUnitPackagesController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: CarRateUnitPackages
        public ActionResult Index(int? sortby, string status, string package, string unit, string group)
        {
            List<PackageperUnit> UnitPkgList = new List<PackageperUnit>();

            IEnumerable<CarRateUnitPackage> pkglist = db.CarRateUnitPackages.Include(c=>c.CarRatePackage.CarRateGroups).ToList();

            foreach (var list in pkglist)
            {
                int id = db.CarRateGroups.Where(c => c.CarRatePackageId == list.CarRatePackageId).FirstOrDefault() != null ? db.CarRateGroups.Where(c => c.CarRatePackageId == list.CarRatePackageId).FirstOrDefault().RateGroupId : 1 ;
                RateGroup groupPkg = db.RateGroups.Find(id);

                UnitPkgList.Add(new PackageperUnit
                {
                    Id = list.Id,
                    RateperDay = list.CarUnit.CarRates.Where(s => s.CarUnitId == list.CarUnitId).FirstOrDefault().Daily,
                    RateperWeek = list.CarUnit.CarRates.Where(s => s.CarUnitId == list.CarUnitId).FirstOrDefault().Weekly,
                    RateperMonth = list.CarUnit.CarRates.Where(s => s.CarUnitId == list.CarUnitId).FirstOrDefault().Monthly,
                    AddOn = (decimal)list.DailyAddon,
                    FuelDaily = list.FuelDaily,
                    FuelLonghaul = list.FuelLonghaul,
                    Meals = list.CarRatePackage.DailyMeals,
                    Acc = list.CarRatePackage.DailyRoom,
                    PkgDesc = list.CarRatePackage.Description,
                    Unit = list.CarUnit.CarRates.Where(s => s.CarUnitId == list.CarUnitId).FirstOrDefault().CarUnit.Description,
                    Group = groupPkg.GroupName,
                    Status = list.CarRatePackage.Status
                });
            }

            UnitPkgList = UnitPkgList.ToList();

            if (status != "all")
            {
                UnitPkgList = UnitPkgList.Where(p => p.Status.ToLower().Contains(status.ToLower())).ToList();
            }

            if (package != "all")
            {
                UnitPkgList = UnitPkgList.Where(p => p.PkgDesc.ToLower().Contains(package.ToLower())).ToList();
            }

            if (unit != "all")
            {
                UnitPkgList = UnitPkgList.Where(p => p.Unit.ToLower().Contains(unit.ToLower())).ToList();
            }

            if (group != "all" )
            {
                UnitPkgList = UnitPkgList.Where(p=>p.Group.ToLower().Contains(group.ToLower())).ToList();
            }


            ViewBag.Packages = db.CarRatePackages.ToList();
            ViewBag.Group = db.RateGroups.ToList();
            ViewBag.Units = db.CarUnits.ToList();

            return View(UnitPkgList);
        }

        // GET: CarRateUnitPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            if (carRateUnitPackage == null)
            {
                return HttpNotFound();
            }
            return View(carRateUnitPackage);
        }

        // GET: CarRateUnitPackages/Create
        public ActionResult Create()
        {
            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description");
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            return View();
        }

        // POST: CarRateUnitPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarRatePackageId,CarUnitId,DailyRate,FuelLonghaul,FuelDaily,DailyAddon")] CarRateUnitPackage carRateUnitPackage)
        {
            if (ModelState.IsValid)
            {
                db.CarRateUnitPackages.Add(carRateUnitPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description", carRateUnitPackage.CarRatePackageId);
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carRateUnitPackage.CarUnitId);
            return View(carRateUnitPackage);
        }

        // GET: CarRateUnitPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            if (carRateUnitPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description", carRateUnitPackage.CarRatePackageId);
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carRateUnitPackage.CarUnitId);
            return View(carRateUnitPackage);
        }

        // POST: CarRateUnitPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarRatePackageId,CarUnitId,DailyRate,FuelLonghaul,FuelDaily,DailyAddon")] CarRateUnitPackage carRateUnitPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carRateUnitPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description", carRateUnitPackage.CarRatePackageId);
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carRateUnitPackage.CarUnitId);
            return View(carRateUnitPackage);
        }

        // GET: CarRateUnitPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            if (carRateUnitPackage == null)
            {
                return HttpNotFound();
            }
            return View(carRateUnitPackage);
        }

        // POST: CarRateUnitPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            db.CarRateUnitPackages.Remove(carRateUnitPackage);
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
