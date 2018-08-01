using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using JobsV1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace JobsV1.Controllers
{
    #region support classes
    public class cJobOrder
    {
        [Key]
        public int Id { get; set; }
        public Models.JobMain Main { get; set; }
        public List<cJobService> Services { get; set; }
        public List<cjobCounter> ActionCounter { get; set; }
    }

    public class cJobService
    {
        [Key]
        public int Id { get; set; }
        public Models.JobServices Service { get; set; }
        public IQueryable<Models.JobAction> Actions { get; set; }
        public IQueryable<Models.SrvActionItem> SvcActions { get; set; }
        public IQueryable<Models.JobServiceItem> SvcItems { get; set; }
        public IQueryable<Models.SupplierPoDtl> SupplierPos { get; set; }
    }

    public class cjobCounter
    {
        public int JobId { get; set; }
        public int? CodeId { get; set; }
        public string CodeDesc { get; set; }
        public int CntItem { get; set; }
        public int CntDone { get; set; }
    }
    #endregion

    public class JobOrderController : Controller
    {
        // NEW CUSTOMER Reference ID
        private int NewCustSysId = 1;
        // Job Status
        private int JOBINQUIRY = 1;
        private int JOBRESERVATION = 2;
        private int JOBCONFIRMED = 3;
        private int JOBCLOSED = 4;
        private int JOBCANCELLED = 5;
        private int JOBTEMPLATE = 6;

        private JobDBContainer db = new JobDBContainer();
        // GET: JobOrder
        public ActionResult Index()
        {
            IQueryable<Models.JobMain> jobMains = db.JobMains
                .Include(j => j.Customer)
                .Include(j => j.Branch)
                .Include(j => j.JobStatus)
                .Include(j => j.JobThru)
                .OrderBy(d => d.JobDate);
            jobMains = (IQueryable<Models.JobMain>)jobMains.Where(d => d.JobStatusId == JOBRESERVATION || d.JobStatusId == JOBCONFIRMED);
            List<cjobCounter> jobActionCntr = getJobActionCount(jobMains.Select(d => d.Id).ToList());
            var data = new List<cJobOrder>();

           
            foreach(var main in jobMains)
            {
                cJobOrder joTmp = new cJobOrder();
                joTmp.Main = main;
                joTmp.Services = new List<cJobService>();

                List<Models.JobServices> joSvc = db.JobServices.Where(d => d.JobMainId == main.Id).ToList();
                foreach( var svc in joSvc)
                {
                    cJobService cjoTmp = new cJobService();
                    cjoTmp.Service = svc;

                    var ActionDone = db.JobActions.Where(d => d.JobServicesId == svc.Id).Select(s => s.SrvActionItemId);

                    cjoTmp.SvcActions = db.SrvActionItems.Where(d => d.ServicesId == svc.ServicesId && !ActionDone.Contains(d.Id) ).Include(d => d.SrvActionCode);
                    cjoTmp.Actions = db.JobActions.Where(d => d.JobServicesId == svc.Id).Include(d=>d.SrvActionItem);
                    cjoTmp.SvcItems = db.JobServiceItems.Where(d => d.JobServicesId == svc.Id).Include(d => d.InvItem);
                    cjoTmp.SupplierPos = db.SupplierPoDtls.Where(d => d.JobServicesId == svc.Id).Include(i => i.SupplierPoHdr);

                    joTmp.Services.Add(cjoTmp);
                }

                joTmp.ActionCounter = jobActionCntr.Where(d => d.JobId == joTmp.Main.Id).ToList();

                data.Add(joTmp);
            }

            List<Customer> customers = db.Customers.ToList();
            ViewBag.companyList = customers;

            return View(data);
        }


        public List<cjobCounter> getJobActionCount(List<Int32> jobidlist )
        {
            #region sqlstr
            string sqlstr = @"
select max(x.jobid) JobId, x.Id CodeId, max(x.code) CodeDesc, sum(x.ActionCount) CntItem, sum(x.DoneCount) CntDone
from 
(

select max(a.JobMainId) jobid , d.Id, max(d.CatCode) code, '0' as ActionCount, count(b.Id) DoneCount
from JobServices a
left outer join JobActions b on a.Id = b.JobServicesId
left outer join SrvActionitems c on b.SrvActionItemId = c.Id
left outer join SrvActionCodes d on c.SrvActionCodeId = d.Id
Group by a.JobMainId,d.Id

union

select max(a.JobMainId) jobid , d.Id, max(d.CatCode) code, count(c.Id) as ActionCount, '0' as DoneCount
from JobServices a
left outer join [Services] b on a.ServicesId = b.Id
left outer join SrvActionitems c on b.Id = c.ServicesId
left outer join SrvActionCodes d on c.SrvActionCodeId = d.Id
Group by a.JobMainId,d.Id
)x Group by x.jobid, x.Id
order by x.jobid
;

";
            #endregion
            List<cjobCounter> jobcntr = db.Database.SqlQuery<cjobCounter>(sqlstr).Where(d=>jobidlist.Contains(d.JobId)).ToList();
            return jobcntr;
        }

        #region Inventory Items
        public ActionResult InventoryItemList(int serviceId)
        {
            var data = db.JobServiceItems.Where(d => d.JobServicesId == serviceId).Include(j => j.InvItem).ToList();
            ViewBag.hdrdata = db.JobServices.Find(serviceId);
            return View(data); 
        }

        public ActionResult BrowseInvItem_withSchedule(int JobServiceId)
        {
            DBClasses dbclass = new DBClasses();
            Models.getItemSchedReturn gret = dbclass.ItemSchedules();
            ViewBag.dtLabel = gret.dLabel;
            ViewBag.serviceId = JobServiceId;
            return View(gret.ItemSched);
        }

        public ActionResult AddItem(int itemId, int serviceId)
        {
            string sqlstr = "Insert Into JobServiceItems([JobServicesId],[InvItemId]) values(" + serviceId.ToString() + "," + itemId.ToString() + ")";
            db.Database.ExecuteSqlCommand(sqlstr);

            return RedirectToAction("Index", new { serviceId = serviceId });

        }

        public ActionResult RemoveItem(int itemId, int serviceId)
        {
            string sqlstr = "Delete from JobServiceItems where JobServicesId = " + serviceId.ToString()
                + " AND InvItemId = " + itemId.ToString();

            db.Database.ExecuteSqlCommand(sqlstr);

            return RedirectToAction("InventoryItemList", new { serviceId = serviceId });
        }
        #endregion

        #region Customer Detail
        private List<SelectListItem> StatusList = new List<SelectListItem> {
                new SelectListItem { Value = "ACT", Text = "Active" },
                new SelectListItem { Value = "INC", Text = "Inactive" },
                new SelectListItem { Value = "BAD", Text = "Bad Account" }
                };

        public ActionResult CompanyDetail(int jobid, int custid)
        {
            var data = db.Customers.Find(custid);

            if (data.Name == "<< New Customer >>")
            {
                return RedirectToAction("CreateCustomer", new { CreateCustJobId = jobid });
            }

            ViewBag.Status = new SelectList(StatusList, "value", "text", data.Status);

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyDetail([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer)
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
        public ActionResult CreateCustomer(int CreateCustJobId)
        {
            var jobCust = db.JobMains.Find(CreateCustJobId);
            var data = new Models.Customer();
            data.Name = jobCust.Description;
            data.Email = jobCust.CustContactEmail;
            data.Contact1 = jobCust.CustContactNumber;

            data.Status = "ACT";

            ViewBag.Status = new SelectList(StatusList, "value", "text", data.Status);
            ViewBag.Status = new SelectList("JobStatusId", "Id", "text", data.Status);
            ViewBag.jobOrderId = CreateCustJobId;
          
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Status == null || customer.Status.Trim() == "") customer.Status = "ACT";

                db.Customers.Add(customer);
                db.SaveChanges();
                
                string JobId = Request.Form["jobOrderId"];
                db.Database.ExecuteSqlCommand(@"
                    Update JobMains set CustomerId=" + customer.Id + " where Id=" + JobId + ";"
                    );

                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(StatusList, "value", "text", customer.Status);
            return View(customer);
        }

        
        #endregion

        #region jobMain

        public ActionResult JobDetails(int jobid)
        {
            var jobMain = db.JobMains.Find(jobid);
            ViewBag.CustomerId = new SelectList(db.Customers.Where(d => d.Status == "ACT"), "Id", "Name", jobMain.CustomerId);
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", jobMain.BranchId);
            ViewBag.JobStatusId = new SelectList(db.JobStatus, "Id", "Status", jobMain.JobStatusId);
            ViewBag.JobThruId = new SelectList(db.JobThrus, "Id", "Desc", jobMain.JobThruId);
            return View(jobMain);
        }


        // GET: JobMains/Create
        public ActionResult jobCreate(int? id)
        {
            JobMain job = new JobMain();
            job.JobDate = System.DateTime.Today;
            job.NoOfDays = 1;
            job.NoOfPax = 1;

            if (id == null)
            {
                ViewBag.CustomerId = new SelectList(db.Customers.Where(d => d.Status == "ACT"), "Id", "Name", NewCustSysId);
            }
            else
            {

                ViewBag.CustomerId = new SelectList(db.Customers.Where(d => d.Status == "ACT"), "Id", "Name", id);
            }

            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name");
            ViewBag.JobStatusId = new SelectList(db.JobStatus, "Id", "Status", JOBCONFIRMED);
            ViewBag.JobThruId = new SelectList(db.JobThrus, "Id", "Desc");

            return View(job);
        }

        
        // POST: JobMains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult jobCreate([Bind(Include = "Id,JobDate,CustomerId,Description,NoOfPax,NoOfDays,AgreedAmt,JobRemarks,JobStatusId,StatusRemarks,BranchId,JobThruId,CustContactEmail,CustContactNumber")] JobMain jobMain)
        {
            if (ModelState.IsValid)
            {
                if (jobMain.CustContactEmail == null && jobMain.CustContactNumber == null)
                {
                    var cust = db.Customers.Find(jobMain.CustomerId);
                    jobMain.CustContactEmail = cust.Email;
                    jobMain.CustContactNumber = cust.Contact1;
                }

                db.JobMains.Add(jobMain);
                db.SaveChanges();

                if (jobMain.CustomerId == NewCustSysId)
                    return RedirectToAction("CreateCustomer",new { CreateCustJobId = jobMain.Id });
                else
                    return RedirectToAction("Index");
                //return RedirectToAction("Services", "JobServices", new { id = jobMain.Id });
                //return RedirectToAction("JobTable", new { span = 30 });

            }

            ViewBag.CustomerId = new SelectList(db.Customers.Where(d => d.Status == "ACT"), "Id", "Name", jobMain.CustomerId);
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", jobMain.BranchId);
            ViewBag.JobStatusId = new SelectList(db.JobStatus, "Id", "Status", jobMain.JobStatusId);
            ViewBag.JobThruId = new SelectList(db.JobThrus, "Id", "Desc", jobMain.JobThruId);
            return View(jobMain);
        }

        public ActionResult ChangeCompany(int id , int newId) {

            JobMain jobMain = db.JobMains.Find(id);
            jobMain.CustomerId = newId;
            db.Entry(jobMain).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Supplier Po
        public ActionResult InitializePO(int srcId)
        {
            var srcdata = db.JobServices.Find(srcId);
            var tmp = new Models.SupplierPoHdr();
            tmp.PoDate = DateTime.Now;
            tmp.Remarks = srcdata.Particulars;
            tmp.RequestBy = User.Identity.Name;
            tmp.DtRequest = DateTime.Now;

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.SupplierPoStatusId = new SelectList(db.SupplierPoStatus, "Id", "Status");
            ViewBag.SrcId = srcId;

            return View(tmp);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InitializePO([Bind(Include = "Id,PoDate,Remarks,SupplierId,SupplierPoStatusId,RequestBy,DtRequest,ApprovedBy,DtApproved")] SupplierPoHdr supplierPoHdr)
        {
            string strSrcId = Request.Form["SrcId"];
            int SrcId = Int32.Parse(strSrcId);
            string strAmt = Request.Form["Amount"];
            decimal PoAmt = decimal.Parse(strAmt);

            if (ModelState.IsValid)
            {
                db.SupplierPoHdrs.Add(supplierPoHdr);
                db.SaveChanges();

                #region Add Details
                var tmp = new Models.SupplierPoDtl();
                tmp.SupplierPoHdrId = supplierPoHdr.Id;
                tmp.Remarks = supplierPoHdr.Remarks;
                tmp.JobServicesId = SrcId;
                tmp.Amount = PoAmt;
                db.SupplierPoDtls.Add(tmp);
                db.SaveChanges();
                #endregion

                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", supplierPoHdr.SupplierId);
            ViewBag.SupplierPoStatusId = new SelectList(db.SupplierPoStatus, "Id", "Status", supplierPoHdr.SupplierPoStatusId);
            return View(supplierPoHdr);
        }

        public ActionResult AddSupplierPODetails(int srcId, int pohdrId)
        {
            var srcdata = db.JobServices.Find(srcId);
            var tmp = new Models.SupplierPoDtl();
            tmp.SupplierPoHdrId = pohdrId;
            tmp.Remarks = srcdata.Particulars;
            tmp.JobServicesId = srcId;
            tmp.Amount = 0;

            return View(tmp);
        }

        #endregion


        #region Services
        public ActionResult JobServiceAdd(int? JobMainId) {
            Models.JobMain job = db.JobMains.Find((int)JobMainId);
            Models.JobServices js = new JobServices();
            js.JobMainId = (int)JobMainId;

            DateTime dtTmp = new DateTime(job.JobDate.Year, job.JobDate.Month, job.JobDate.Day, 8, 0, 0);
            js.DtStart = dtTmp;
            js.DtEnd = dtTmp.AddDays(job.NoOfDays - 1).AddHours(10);
            js.Remarks = "10hrs use per day P250 in excess, Driver and Fuel Included";

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description",job.Description);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name");
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description");
            return View(js);
        }


        // POST: JobServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult JobServiceAdd([Bind(Include = "Id,JobMainId,ServicesId,SupplierId,DtStart,DtEnd,Particulars,QuotedAmt,SupplierAmt,ActualAmt,Remarks,SupplierItemId")] JobServices jobServices)
        {
            var NewSupplierSysId = 1;
            if (ModelState.IsValid)
            {
                jobServices.DtEnd = ((DateTime)jobServices.DtEnd).Add(new TimeSpan(23, 59, 59));
                db.JobServices.Add(jobServices);
                db.SaveChanges();

                if (jobServices.SupplierId == NewSupplierSysId)
                    return RedirectToAction("CreateSupplier", "JobServices", new { Svcid = jobServices.Id });
                else
                    return RedirectToAction("Index","JobOrder", null);
            }

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);
            return View(jobServices);
        }

        // GET: JobServices/Edit/5
        public ActionResult JobServiceEdit(int? id)
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
        public ActionResult JobServiceEdit([Bind(Include = "Id,JobMainId,ServicesId,SupplierId,DtStart,DtEnd,Particulars,QuotedAmt,SupplierAmt,ActualAmt,Remarks,SupplierItemId")] JobServices jobServices)
        {

            int NewSupplierSysId = 1;

            if (ModelState.IsValid)
            {
                jobServices.DtEnd = ((DateTime)jobServices.DtEnd).Add(new TimeSpan(23, 59, 59));
                db.Entry(jobServices).State = EntityState.Modified;

                DateTime dtSvc = (DateTime)jobServices.DtStart;
                List<Models.JobItinerary> iti = db.JobItineraries.Where(d => d.JobMainId == jobServices.JobMainId && d.SvcId == jobServices.Id).ToList();
                foreach (var ititmp in iti)
                {
                    int iHr = dtSvc.Hour, iMn = dtSvc.Minute;
                    if (ititmp.ItiDate != null)
                    {
                        DateTime dtIti = (DateTime)ititmp.ItiDate;
                        iHr = dtIti.Hour;
                        iMn = dtIti.Minute;
                    }
                    ititmp.ItiDate = new DateTime(dtSvc.Year, dtSvc.Month, dtSvc.Day, iHr, iMn, 0);
                    db.Entry(ititmp).State = EntityState.Modified;
                }

                db.SaveChanges();

                if (jobServices.SupplierId == NewSupplierSysId)
                    return RedirectToAction("CreateSupplier", new { Svcid = jobServices.Id });
                else
                    return RedirectToAction("Index");
            }
            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);
            return View(jobServices);
        }

        // GET: JobServices/Details/5
        public ActionResult JobServiceDetails(int? id)
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


        public ActionResult JobSvcDelete(int? id) {

            JobServices jobServices = db.JobServices.Find(id);
            int jId = jobServices.JobMainId;
            db.JobServices.Remove(jobServices);
            db.SaveChanges();
            return RedirectToAction("Index", "JobOrder",null);
        }

        public ActionResult notify() {
            DBClasses dbc = new DBClasses();
            dbc.addNotification("Job Order","Test");
            return RedirectToAction("Index");
        }
        #endregion

        #region supplier
        public ActionResult PoDetails(int? hdrId) {
            var supplierPoDtls = db.SupplierPoDtls.Include(s => s.SupplierPoHdr).Include(s => s.JobService).Where(d => d.SupplierPoHdrId == (int)hdrId);
            SupplierPoDtl supplier = new SupplierPoDtl();
            List<SupplierPoItem> supItems = new List<SupplierPoItem>();
            List<InvItem> invItems = new List<InvItem>();
            var hdr = db.SupplierPoHdrs.Where(h => h.Id == hdrId).ToList();

            supplier = db.SupplierPoDtls.Where(s => s.SupplierPoHdrId == hdrId).FirstOrDefault();
            if (supplier != null)
            {
                supItems = db.SupplierPoItems.Where(s => s.SupplierPoDtlId == supplier.Id).ToList();
            }
            else
            {
                supplier = new SupplierPoDtl
                {
                    Id = 0
                };
            }

            if (supItems == null)
            {
                supItems.Add(new SupplierPoItem
                {
                    Id = 0,
                    InvItem = null,
                    InvItemId = 0,
                    SupplierPoDtl = null,
                    SupplierPoDtlId = 0,
                });
            }

            invItems = db.InvItems.ToList();
            if (invItems == null)
            {
                invItems.Add(new InvItem
                {
                    Id = 0,
                });
            }

            ViewBag.HdrInfo = hdr;
            ViewBag.HdrId = hdrId;
            ViewBag.supplierPoItems = supItems;
            ViewBag.InvItemsList = invItems;
            ViewBag.Id = supplier.Id;

            return View(supplierPoDtls.ToList());
        }



        #endregion

        #region Action Items status update
        //Ajax Call
        public ActionResult MarkDone(int SvcId, int ActionId)
        {
            Models.JobAction jaTmp = new JobAction();
            jaTmp.AssignedTo = User.Identity.Name;
            jaTmp.DtAssigned = DateTime.Now;
            jaTmp.DtPerformed = DateTime.Now;
            jaTmp.PerformedBy = User.Identity.Name;
            jaTmp.Remarks = "Done";
            jaTmp.JobServicesId = SvcId;
            jaTmp.SrvActionItemId = ActionId;

            db.JobActions.Add(jaTmp);
            db.SaveChanges();

            return Json( "from MarkDone:" + SvcId.ToString() + "/" + ActionId.ToString(),
                JsonRequestBehavior.AllowGet);
        }

        //ajax test
        public ActionResult AjaxTest()
        {
            return Json("insomia", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Daily Status Updates
        public ActionResult DailyUpdateList() {

            string Message = "";

               
               var updates = db.Database.SqlQuery<DailyUpdate>(@"
                    select
                    'New SalesLead' as StatusCategory, 
                    a.DtEntered dtTaken,  
                    a.Id refId, a.CustName + ' / ' + a.Details Details 
                    from SalesLeads a
                    where datediff(day, getdate(),a.DtEntered) > -2

                    union

                    select 
                    'SalesLead Activity' as StatusCategory,
                    a.DtActivity dtTaken,
                    a.SalesLeadId refId,
                    a.Particulars Details
                    from 
                    SalesActivities a
                    where a.SalesActStatusId=2 AND datediff(day, getdate(),a.DtActivity) > -2;
                    ").ToList();

                return View(updates);
  
        }
        #endregion
    }
}