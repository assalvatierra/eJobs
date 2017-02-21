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
    public class JobServicesController : Controller
    {
        private int NewSupplierSysId = 1;
        private JobDBContainer db = new JobDBContainer();

        // GET: JobServices
        public ActionResult Index()
        {
            var jobServices = db.JobServices.Include(j => j.JobMain).Include(j => j.Supplier).Include(j => j.Service).Include(j => j.SupplierItem);
            return View(jobServices.ToList());
        }

        public ActionResult Services(int? id)
        {
            ViewBag.JobMainId = id;

            var Job = db.JobMains.Where(d => d.Id == id).FirstOrDefault();
            ViewBag.JobOrder = Job;

            var jobServices = db.JobServices.Include(j => j.JobMain).Include(j => j.Supplier).Include(j => j.Service).Include(j => j.SupplierItem).Include(j=> j.JobServicePickups).Where(d => d.JobMainId == id);
            System.Collections.ArrayList providers = new System.Collections.ArrayList();
            foreach (var item in jobServices)
            {
                if (item.JobServicePickups != null)
                {
                    string sTmp = "";
                    try
                    {
                        sTmp = item.JobServicePickups.FirstOrDefault().ProviderName;
                    }
                    catch
                    {
                        sTmp = "Provider not defined.";
                    }

                    if (!providers.Contains(sTmp))
                    {
                        providers.Add(sTmp);
                    }
                }
            }        
            
            ViewBag.Providers = providers;

            return View( jobServices.OrderBy(d=>d.DtStart).ToList() );

        }

        // GET: JobServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobServices jobServices = db.JobServices.Find(id);
            if (jobServices == null)
            {
                return HttpNotFound();
            }
            return View(jobServices);
        }

        // GET: JobServices/Create
        public ActionResult Create(int? JobMainId)
        {
            Models.JobMain job = db.JobMains.Find((int)JobMainId);
            Models.JobServices js = new JobServices();
            js.JobMainId = (int)JobMainId;

            DateTime dtTmp = new DateTime(job.JobDate.Year, job.JobDate.Month, job.JobDate.Day, 8, 0, 0);
            js.DtStart = dtTmp;
            js.DtEnd = dtTmp.AddDays(job.NoOfDays - 1).AddHours(10);
            js.Remarks = "10hrs use per day P250 in excess, Driver and Fuel Included";

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name");
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description");
            return View(js);
        }

        // POST: JobServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobMainId,ServicesId,SupplierId,DtStart,DtEnd,Particulars,QuotedAmt,SupplierAmt,ActualAmt,Remarks,SupplierItemId")] JobServices jobServices)
        {
            if (ModelState.IsValid)
            {
                db.JobServices.Add(jobServices);
                db.SaveChanges();

                if (jobServices.SupplierId == NewSupplierSysId)
                    return RedirectToAction("CreateSupplier", new { Svcid = jobServices.Id });
                else
                    return RedirectToAction("Services", new { id = jobServices.JobMainId });
            }

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);
            return View(jobServices);
        }

        // GET: JobServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobServices jobServices = db.JobServices.Find(id);
            if (jobServices == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);
            return View(jobServices);
        }

        // POST: JobServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobMainId,ServicesId,SupplierId,DtStart,DtEnd,Particulars,QuotedAmt,SupplierAmt,ActualAmt,Remarks,SupplierItemId")] JobServices jobServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobServices).State = EntityState.Modified;
                db.SaveChanges();

                if (jobServices.SupplierId == NewSupplierSysId)
                    return RedirectToAction("CreateSupplier", new { Svcid = jobServices.Id });
                else
                    return RedirectToAction("Services", new { id = jobServices.JobMainId });
            }
            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);
            return View(jobServices);
        }

        // GET: JobServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobServices jobServices = db.JobServices.Find(id);
            if (jobServices == null)
            {
                return HttpNotFound();
            }
            return View(jobServices);
        }

        // POST: JobServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobServices jobServices = db.JobServices.Find(id);
            int jId = jobServices.JobMainId;
            db.JobServices.Remove(jobServices);
            db.SaveChanges();
            return RedirectToAction("Services", new { id = jId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult CreateSupplier(int? Svcid)
        {
            JobServices svc = db.JobServices.Find(Svcid);
            JobMain job = db.JobMains.Find(svc.JobMainId);
            ViewBag.JobOrder = job;
            ViewBag.Service = svc;
            Session["CurrentJobId"] = svc.JobMainId;
            Session["CurrentSvcId"] = svc.Id;

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.SupplierTypeId = new SelectList(db.SupplierTypes, "Id", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult CreateSupplier([Bind(Include = "Id,Name,Contact1,Contact2,Contact3,Email,Details,CityId,SupplierTypeId")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();

                int jobid = (int)Session["CurrentJobId"];
                int svcid = (int)Session["CurrentSvcId"];
                JobServices jobsvc = db.JobServices.Find(svcid);
                jobsvc.SupplierId = supplier.Id;
                db.Entry(jobsvc).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Services", new { id = jobid });
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", supplier.CityId);
            ViewBag.SupplierTypeId = new SelectList(db.SupplierTypes, "Id", "Description");
            return View(supplier);

        }

        public ActionResult InitServicePickup(int? id)
        {
            Models.JobServicePickup svcpu;

            Models.JobServices svc = db.JobServices.Find(id);
            if (svc.JobServicePickups.FirstOrDefault() == null)
            {
                svcpu = new JobServicePickup();
                svcpu.JobServicesId = svc.Id;
                svcpu.JsDate = svc.JobMain.JobDate;
                svcpu.JsTime = svc.JobMain.JobDate.ToString("hh:mm:00");
                svcpu.ClientName = svc.JobMain.Customer.Name;
                svcpu.ClientContact = (svc.JobMain.Customer.Contact1 == null ? "" : svc.JobMain.Customer.Contact1) +
                    (svc.JobMain.Customer.Contact2 == null ? "" : "/" + svc.JobMain.Customer.Contact2);
                svcpu.ProviderName = svc.SupplierItem.InCharge;
                svcpu.ProviderContact = svc.SupplierItem.Tel1
                    + (svc.SupplierItem.Tel2 == null ? "" : "/" + svc.SupplierItem.Tel2)
                    + (svc.SupplierItem.Tel3 == null ? "" : "/" + svc.SupplierItem.Tel3);

                db.JobServicePickups.Add(svcpu);
                db.SaveChanges();
            }
            else
            {
                svcpu = svc.JobServicePickups.FirstOrDefault();
            }

            return RedirectToAction("ServicePickup", new { id = svcpu.Id } );
        }

        public ActionResult ResetServicePickup(int? id)
        {
            Models.JobServicePickup svcpu = db.JobServicePickups.Find(id);
            Models.JobServices svc = db.JobServices.Find(svcpu.JobServicesId);

            svcpu.ClientName = (svcpu.ClientName==null||svcpu.ClientName.Trim()==""?svc.JobMain.Customer.Name: svcpu.ClientName);

            string sClientContact = 
                (svc.JobMain.Customer.Contact1 == null ? "" : svc.JobMain.Customer.Contact1) +
                (svc.JobMain.Customer.Contact2 == null ? "" : "/" + svc.JobMain.Customer.Contact2);

            svcpu.ClientContact = (svcpu.ClientContact == null || svcpu.ClientContact.Trim() == "" ? sClientContact:svcpu.ClientContact);

            svcpu.ProviderName = svc.SupplierItem.InCharge;
            svcpu.ProviderContact = svc.SupplierItem.Tel1
                + (svc.SupplierItem.Tel2 == null ? "" : "/" + svc.SupplierItem.Tel2)
                + (svc.SupplierItem.Tel3 == null ? "" : "/" + svc.SupplierItem.Tel3);

            db.Entry(svcpu).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ServicePickup", new { id = svcpu.Id });
        }


        public ActionResult ServicePickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobServicePickup jobServicePickup = db.JobServicePickups.Find(id);
            if (jobServicePickup == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Contact = db.JobContacts.Where(d => d.ContactType == "100").ToList();

            return View(jobServicePickup);
        }


        [HttpPost, ActionName("ServicePickup")]
        public ActionResult ServicePickup([Bind(Include = "Id,JobServicesId,JsDate,JsTime,JsLocation,ClientName,ClientContact,ProviderName,ProviderContact")] JobServicePickup jobServicePickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobServicePickup).State = EntityState.Modified;
                db.SaveChanges();

                int ij = db.JobServices.Find(jobServicePickup.JobServicesId).JobMainId;
                return RedirectToAction("Services", new { id = ij });
            }
            //ViewBag.JobServicesId = new SelectList(db.JobServices, "Id", "Particulars", jobServicePickup.JobServicesId);

            return View(jobServicePickup);

        }

        public ActionResult TextMessage(int? id)
        {
            string sData = "Pickup Details";

            Models.JobServicePickup svcpu;
            Models.JobServices svc = db.JobServices.Find(id);
            if (svc.JobServicePickups.FirstOrDefault() == null)
            {
                sData += "\nPickup: undefined ";
            }
            else
            {
                Decimal quote = ( svc.QuotedAmt==null ? 0 : (decimal) svc.QuotedAmt ) ;

                svcpu = svc.JobServicePickups.FirstOrDefault();
                sData += "\nDate:" + ((DateTime)svc.DtStart).ToString("dd MMM yyyy (ddd)");
                sData += "\nTime&Location:" + svcpu.JsTime + " " + svcpu.JsLocation;
                sData += "\nGuest:" + svcpu.ClientName +" #"+ svcpu.ClientContact;
                sData += "\nDriver:" + svcpu.ProviderName + " #" + svcpu.ProviderContact;
                sData += "\nUnit:" + svc.SupplierItem.Description + " " + svc.SupplierItem.Remarks;
                sData += "\nRate:P" + quote.ToString("##,###.00");
                sData += "\nParticulars:" + svc.Particulars;
                sData += "\n  " + svc.Remarks;
                sData += "\n\nHave a safe trip,\nAJ88 Car Rental";
            }

            ViewBag.StrData = sData;
            return View();

        }

    }
}
