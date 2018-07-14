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
    public class CustomersController : Controller
    {
        private JobDBContainer db = new JobDBContainer();
        private List<SelectListItem> StatusList = new List<SelectListItem> {
                new SelectListItem { Value = "ACT", Text = "Active" },
                new SelectListItem { Value = "INC", Text = "Inactive" },
                new SelectListItem { Value = "BAD", Text = "Bad Account" }
                };

        // GET: Customers
        public ActionResult Index()
        {
            var customerList = db.Customers.ToList();
            string category;
            string companyName;


            List<CustomerDetails> customerDetailList = new List<CustomerDetails>();
            foreach (var customer in customerList)
            {
                CustCategory custcategory = new CustCategory();
                CustCat custcat = new CustCat();
                CustEntity companyEntity = new CustEntity();
                CustEntMain company = new CustEntMain();

                try {
                    custcat = db.CustCats.Where(c => c.CustomerId == customer.Id).FirstOrDefault();
                    custcategory = db.CustCategories.Where(cat => cat.Id == custcat.CustCategoryId).FirstOrDefault();
                    
                } catch (Exception ex) {
                    custcategory = new CustCategory {
                        Id = 0,
                        Name = "Not Assigned",
                        iconPath = "0"
                    };
                }

                try
                {
                     companyEntity = db.CustEntities.Where(ce => ce.CustomerId == customer.Id).FirstOrDefault();
                     company = db.CustEntMains.Where(co => co.Id == companyEntity.CustEntMainId).FirstOrDefault();

                } catch (Exception ex) {
                    company = new CustEntMain
                    {
                        Id = 0,
                        Name = "Not Assigned",
                        Address = "none",
                        Contact1 = "0",
                        Contact2 = "0",
                        iconPath = "0"
                    };
                }

                customerDetailList.Add(new CustomerDetails
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Contact1 = customer.Contact1,
                    Contact2 = customer.Contact2,
                    Remarks = customer.Remarks,
                    Status = customer.Status,
                    JobID = 1,
                    CustCategoryID = custcategory.Id,
                    CustCategoryIcon = custcategory.iconPath,
                    CustEntID = company.Id,
                    CustEntName = company.Name,
                    CustEntIconPath = company.iconPath
                    // JobID = db.JobMains.Where(jm => jm.CustomerId.Equals(customer.Id)).FirstOrDefault() == null ? 0 : db.JobMains.Where(jm => jm.CustomerId.Equals(customer.Id)).FirstOrDefault().Id,

                });

            }


            //return View(db.Customers.ToList());
            return View(customerDetailList);

        }
        

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            //generate partial view list for companies
            PartialView_Companies(id);
            PartialView_Jobs(id);
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Status == null || customer.Status.Trim() == "") customer.Status = "ACT";

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.Status = new SelectList( StatusList,"value", "text" ,customer.Status );

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(StatusList, "value", "text", customer.Status);

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        private void PartialView_Companies(int? id) {

            //PartialView for Details of the Customer
            List<CustEntMain> CompanyList = new List<CustEntMain>();
            //error
            var CompanyRecord = db.CustEntities.Where(c => c.CustomerId == id).ToList();

            if (CompanyRecord == null)
            {

                CompanyList.Add(new CustEntMain
                {
                    Id = 0,
                    Name = "None",
                    Address = "None",
                    Contact1 = "None",
                    Contact2 = "None",
                    iconPath = "None"
                });

            }
            else
            {

                foreach (var record in CompanyRecord)
                {
                    CompanyList.Add(new CustEntMain
                    {
                        Id = record.CustEntMain.Id,
                        Name = record.CustEntMain.Name,
                        Address = record.CustEntMain.Address,
                        Contact1 = record.CustEntMain.Contact1,
                        Contact2 = record.CustEntMain.Contact2,
                        iconPath = record.CustEntMain.iconPath
                    });

                }

            }
            ViewBag.companyList = CompanyList;
            ViewBag.CustomerID = id;

        }


        private void PartialView_Jobs(int? id)
        {

            //PartialView for Details of the Customer
            List<CustomerJobDetails> jobList = new List<CustomerJobDetails>();
            //error
            var jobRecord = db.JobMains.Where(j => j.CustomerId == id).ToList();

            if (jobRecord == null)
            {

                jobList.Add(new CustomerJobDetails
                {
                    Id = 0,
                    JobDate = "7/24/2018",
                    Description = "none",
                    AgreedAmt = "0",
                    NoOfDays = "0",
                    NoOfPax = "0",
                    StatusRemarks = "none"
                });

            }
            else
            {

                foreach (var record in jobRecord)
                {
                    jobList.Add(new CustomerJobDetails
                    {

                        Id = record.Id,
                        JobDate = record.JobDate.ToString(),
                        Description = record.Description,
                        AgreedAmt = record.AgreedAmt.ToString(),
                        NoOfDays = record.NoOfDays.ToString(),
                        NoOfPax = record.NoOfPax.ToString(),
                        StatusRemarks = record.JobStatus.Status

                    });

                }

            }

            ViewBag.jobList = jobList;

        }
    }
}
