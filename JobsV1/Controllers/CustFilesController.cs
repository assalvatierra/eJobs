using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsV1.Models;
using System.IO;

namespace JobsV1.Controllers
{
    public class CustFilesController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: CustFiles
        public ActionResult Index()
        {
            var custFiles = db.CustFiles.Include(c => c.Customer);
            return View(custFiles.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsynFileUpload(IEnumerable<HttpPostedFileBase> files)
        {
            int count = 1;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        file.SaveAs(path);
                        count++;
                    }
                }
            }
            return new JsonResult
            {
                Data = "Successfully " + count + " file(s) uploaded"
            };
        }

        // GET: CustFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustFiles custFiles = db.CustFiles.Find(id);
            if (custFiles == null)
            {
                return HttpNotFound();
            }
            return View(custFiles);
        }

        // GET: CustFiles/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: CustFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Desc,Folder,Path,Remarks,CustomerId")] CustFiles custFiles)
        {
            if (ModelState.IsValid)
            {
                db.CustFiles.Add(custFiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", custFiles.CustomerId);
            return View(custFiles);
        }

        // GET: CustFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustFiles custFiles = db.CustFiles.Find(id);
            if (custFiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", custFiles.CustomerId);
            return View(custFiles);
        }

        // POST: CustFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Desc,Folder,Path,Remarks,CustomerId")] CustFiles custFiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custFiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", custFiles.CustomerId);
            return View(custFiles);
        }

        // GET: CustFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustFiles custFiles = db.CustFiles.Find(id);
            if (custFiles == null)
            {
                return HttpNotFound();
            }
            return View(custFiles);
        }

        // POST: CustFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustFiles custFiles = db.CustFiles.Find(id);
            db.CustFiles.Remove(custFiles);
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
