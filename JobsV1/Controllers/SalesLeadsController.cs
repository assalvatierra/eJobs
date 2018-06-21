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
    public class SalesLeadsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: SalesLeads
        public ActionResult Index()
        {
            var salesLeads = db.SalesLeads.Include(s => s.Customer)
                .Include(s=>s.SalesLeadCategories)
                .Include(s => s.SalesStatus);

            ViewBag.StatusCodes = db.SalesStatusCodes.ToList();
            return View(salesLeads.ToList());
        }

        // GET: SalesLeads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLead salesLead = db.SalesLeads.Find(id);
            if (salesLead == null)
            {
                return HttpNotFound();
            }
            return View(salesLead);
        }

        // GET: SalesLeads/Create
        public ActionResult Create()
        {
            var tmp = new Models.SalesLead();
            tmp.Date = DateTime.Now;
            tmp.DtEntered = DateTime.Now;
            tmp.EnteredBy = HttpContext.User.Identity.Name;

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View(tmp);
        }

        // POST: SalesLeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Details,Remarks,CustomerId,CustName,DtEntered,EnteredBy")] SalesLead salesLead)
        {
            if (ModelState.IsValid)
            {
                db.SalesLeads.Add(salesLead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", salesLead.CustomerId);
            return View(salesLead);
        }

        // GET: SalesLeads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLead salesLead = db.SalesLeads.Find(id);
            if (salesLead == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", salesLead.CustomerId);
            return View(salesLead);
        }

        // POST: SalesLeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Details,Remarks,CustomerId,CustName,DtEntered,EnteredBy")] SalesLead salesLead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesLead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", salesLead.CustomerId);
            return View(salesLead);
        }

        // GET: SalesLeads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLead salesLead = db.SalesLeads.Find(id);
            if (salesLead == null)
            {
                return HttpNotFound();
            }
            return View(salesLead);
        }

        // POST: SalesLeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesLead salesLead = db.SalesLeads.Find(id);
            db.SalesLeads.Remove(salesLead);
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


        #region Sales Lead Category
        public ActionResult SalesLeadCat(int id)
        {
            var data = db.SalesLeadCategories.Where(d => d.SalesLeadId == id);
            ViewBag.CategoryList = db.SalesLeadCatCodes.ToList();
            ViewBag.SalesLeadId = (int)id;
            return View(data.ToList());
        }

        public ActionResult AddSalesLeadCat(int CodeId, int slId)
        {
            string Message = "";
            try
            {
                db.Database.ExecuteSqlCommand(@"
                insert into SalesLeadCategories([SalesLeadCatCodeId],[SalesLeadId])
                values (" + CodeId.ToString() + "," + slId.ToString() + "); "
                    );

                Message = "Success: " + CodeId.ToString() + "Added...";
                ViewBag.SalesLeadId = slId;
            }
            catch(Exception ex)
            {
                Message = "Error: " + ex.Message;
            }

            return RedirectToAction("SalesLeadCat", new { id = slId });
        }

        public ActionResult RemoveSalesLeadCat(int CodeId, int slId)
        {
            string Message = "";
            try
            {
                db.Database.ExecuteSqlCommand(@"
                delete from SalesLeadCategories
                where (SalesLeadCatCodeId=" + CodeId.ToString() + @"
                AND SalesLeadId=" + slId.ToString() + "); "
                    );

                Message = "Success: " + CodeId.ToString() + "Removed...";
                ViewBag.SalesLeadId = slId;
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;
            }

            return RedirectToAction("SalesLeadCat", new { id = slId });
        }
        #endregion

        #region AddSalesStatus
        public ActionResult AddSalesStatus(int slId, int StatusId)
        {
            string strMsg = "";
            try
            {
                db.Database.ExecuteSqlCommand(@"
                Insert into SalesStatus([DtStatus],[SalesStatusCodeId],[SalesLeadId])
                Values('" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + StatusId + "','" + slId.ToString() + @"');
                ");

                strMsg = "Success";
            }
            catch(Exception Ex)
            {
                strMsg = "Error:" + Ex.Message;
            }

            ViewBag.Message = strMsg;
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Request
        public ActionResult ListActivityCodes(int id)
        {
            var data = db.SalesActCodes.ToList();
            ViewBag.SalesLeadId = id;

            return View(data);
        }
        public ActionResult AddActivityCode(int slId, int ActCodeId)
        {
            var data = new Models.SalesActivity();
            data.SalesLeadId = slId;
            data.SalesActCodeId = ActCodeId;
            data.DtActivity = DateTime.Now;
            data.SalesActStatusId = 1;
            data.DtEntered = DateTime.Now;
            data.EnteredBy = User.Identity.Name;

            ViewBag.SalesActCodeId = new SelectList(db.SalesActCodes, "Id", "Name", ActCodeId);
            ViewBag.SalesLeadId = new SelectList(db.SalesLeads, "Id", "Details", slId);
            ViewBag.SalesActStatusId = new SelectList(db.SalesActStatus, "Id", "Name");
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddActivityCode([Bind(Include = "Id,SalesLeadId,SalesActCodeId,Particulars,DtActivity,SalesActStatusId,DtEntered,EnteredBy")] SalesActivity salesActivity)
        {
            if (ModelState.IsValid)
            {
                db.SalesActivities.Add(salesActivity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalesActCodeId = new SelectList(db.SalesActCodes, "Id", "Name", salesActivity.SalesActCodeId);
            ViewBag.SalesLeadId = new SelectList(db.SalesLeads, "Id", "Details", salesActivity.SalesLeadId);
            ViewBag.SalesActStatusId = new SelectList(db.SalesActStatus, "Id", "Name", salesActivity.SalesActStatusId);
            return View(salesActivity);
        }
        public ActionResult SalesActivityDone(int id)
        {
            db.Database.ExecuteSqlCommand("update SalesActivities set SalesActStatusId=2 where Id=" + id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
