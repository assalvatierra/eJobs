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
using System.Data.Entity.Core.Objects;

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
        public decimal Payment { get; set; }
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
        public ActionResult Index(int? sortid, int? serviceId, int? mainid)
        {

            if (sortid != null)
                Session["FilterID"] = (int)sortid;
            else
            {
                if (Session["FilterID"] != null)
                    sortid = (int)Session["FilterID"];
                else
                    sortid = 1;
            }


            IQueryable<Models.JobMain> jobMains = db.JobMains
                .Include(j => j.Customer)
                .Include(j => j.Branch)
                .Include(j => j.JobStatus)
                .Include(j => j.JobThru)
                ;

            /*
            switch (sortid) {
                case 1: //OnGoing
                    jobMains = (IQueryable<Models.JobMain>)jobMains
                        .Where(d => (d.JobStatusId == JOBRESERVATION || d.JobStatusId == JOBCONFIRMED)
                        && (d.JobDate.CompareTo(DateTime.Today) >= 0))
                        .OrderBy(d => d.JobDate);
                    break;
                case 2: //Previous
                    jobMains = (IQueryable<Models.JobMain>)jobMains
                        .Where(d => (d.JobStatusId == JOBRESERVATION || d.JobStatusId == JOBCONFIRMED ) )
                        .Where(p => DbFunctions.AddDays(p.JobDate, 0) < DateTime.Today).OrderByDescending(d => d.JobDate);
                    break;
                case 3: //Closed (60 days below)
                    jobMains = (IQueryable<Models.JobMain>)jobMains
                        .Where(d => (d.JobStatusId == JOBCLOSED || d.JobStatusId == JOBCANCELLED )
                        ).Where(p => DbFunctions.AddDays(p.JobDate, 60) > DateTime.Today).OrderByDescending(d => d.JobDate);

                    break;
                default:
                
                    jobMains = (IQueryable<Models.JobMain>)jobMains
                        .Where(d => (d.JobStatusId == JOBRESERVATION || d.JobStatusId == JOBCONFIRMED));
                    break;
            }
            */
            List<cjobCounter> jobActionCntr = getJobActionCount(jobMains.Select(d => d.Id).ToList());
            var data = new List<cJobOrder>();

           
            foreach(var main in jobMains)
            {
                cJobOrder joTmp = new cJobOrder();
                joTmp.Main = main;
                joTmp.Services = new List<cJobService>();
                joTmp.Main.AgreedAmt = 0;
                joTmp.Payment = 0;

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
                    joTmp.Main.AgreedAmt += svc.ActualAmt;
                    //get latest job date
                    /*
                    if (sortid == 1)
                    {

                        joTmp.Main.JobDate = DateTime.Compare((DateTime)svc.DtStart, DateTime.Today) >= 0 ? (DateTime)svc.DtStart : joTmp.Main.JobDate;

                        if (DateTime.Compare((DateTime)svc.DtStart, DateTime.Today) <= 0
                            && DateTime.Compare((DateTime)svc.DtEnd, DateTime.Today) >= 0)
                        {
                            joTmp.Main.JobDate = DateTime.Today;
                        }


                    }
                    else if(sortid == 2){

                        joTmp.Main.JobDate = DateTime.Compare((DateTime)svc.DtStart, DateTime.Today) < 0 ? (DateTime)svc.DtStart : joTmp.Main.JobDate;
                    }
                    */
                    joTmp.Services.Add(cjoTmp);
                }

                joTmp.ActionCounter = jobActionCntr.Where(d => d.JobId == joTmp.Main.Id).ToList();

                joTmp.Main.JobDate = TempJobDate(joTmp.Main.Id);

                data.Add(joTmp);

                List<Models.JobPayment> jobPayment = db.JobPayments.Where(d => d.JobMainId == main.Id).ToList();
                foreach (var payment in jobPayment)
                {
                    joTmp.Payment += payment.PaymentAmt;
                }


            }

            List<Customer> customers = db.Customers.ToList();
            ViewBag.companyList = customers;

            var jobmainId = serviceId != null ? db.JobServices.Find(serviceId).JobMainId : 0;

            jobmainId = mainid != null ? (int)mainid : jobmainId;
            
            ViewBag.mainId = jobmainId;

            switch (sortid)
            {
                case 1: //OnGoing
                    data = (List<cJobOrder>) data
                        .Where(d => (d.Main.JobStatusId == JOBRESERVATION || d.Main.JobStatusId == JOBCONFIRMED)
                        && (d.Main.JobDate.CompareTo(DateTime.Today) >= 0)).ToList();

                    break;
                case 2: //OnGoing
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBRESERVATION || d.Main.JobStatusId == JOBCONFIRMED))
                        .Where(p => DateTime.Compare(p.Main.JobDate, DateTime.Today) < 0).ToList();

                    break;
                case 3: //OnGoing
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBCLOSED || d.Main.JobStatusId == JOBCANCELLED)
                        ).Where(p => DbFunctions.AddDays(p.Main.JobDate, 60) > DateTime.Today).ToList();

                    break;

                default:

                    data = (List<cJobOrder>)data.ToList();

                    break;
            }

            if (sortid == 1)
            {

                return View(data.OrderBy(d => d.Main.JobDate));
            }
            else
            {
                return View(data.OrderByDescending(d => d.Main.JobDate));

            }
        }


        public DateTime TempJobDate(int mainId)
        {
            //update jobdate
            var main = db.JobMains.Where(j => mainId == j.Id).FirstOrDefault();
            DateTime minDate = new DateTime(9999,12,30);
            DateTime maxDate = new DateTime(1,1,1);

            //loop though all jobservices in the jobmain
            //to get the latest date
            foreach (var svc in db.JobServices.Where(s => s.JobMainId == mainId))
            {
               
                //get min date
               // minDate = (DateTime)svc.DtStart;
                if (DateTime.Compare(minDate, (DateTime)svc.DtStart) >= 0) {
                    minDate = (DateTime)svc.DtStart; //if minDate > Dtstart
                }

                if (DateTime.Compare(DateTime.Today, (DateTime)svc.DtStart) >= 0 && DateTime.Compare(DateTime.Today, (DateTime)svc.DtEnd) <= 0)
                {
                    minDate = DateTime.Today;
                    //skip
                } else {
                    if (DateTime.Compare(DateTime.Today, (DateTime)svc.DtStart) < 0 && DateTime.Compare(DateTime.Today, minDate) > 0)
                    {
                        minDate = (DateTime)svc.DtStart; //if Today > Dtstart
                    }
                }
                
                //get max date
                //maxDate = (DateTime)svc.DtStart;
                if (DateTime.Compare(maxDate, (DateTime)svc.DtEnd) <= 0)
                {
                    maxDate = (DateTime)svc.DtEnd; //if minDate > Dtstart
                }

                
                /*
                //get least latest date
                if (DateTime.Compare(DateTime.Today, (DateTime)svc.DtStart) >= 0)   //if today is later than datestart, assign datestart to jobdate, 
                {

                    main.JobDate = (DateTime)svc.DtStart;

                    if (DateTime.Compare(DateTime.Today, (DateTime)svc.DtEnd) <= 0) //if today earlier than date end, assign jobdate today
                    {
                        //assign date today
                       // main.JobDate = DateTime.Today;
                     //   db.Entry(main).State = EntityState.Modified;
                    }

                }
                else  //if today is earlier than datestart, assign datestart to jobdate, 
                {
                   // main.JobDate = (DateTime)svc.DtStart;
                   // db.Entry(main).State = EntityState.Modified;
                }
                //db.SaveChanges();
                */

            }

            if (DateTime.Compare(DateTime.Today, minDate) == 0)
            {
                main.JobDate = minDate;
            }

            if (DateTime.Compare(DateTime.Today, maxDate) == 0)
            {
                main.JobDate = maxDate;
            }

            if (DateTime.Compare(DateTime.Today, minDate) < 0) {
                main.JobDate = minDate;
            }

            if (DateTime.Compare(DateTime.Today, minDate) > 0)
            {
                if (DateTime.Compare(DateTime.Today, maxDate) < 0)
                {
                    main.JobDate = DateTime.Today;
                }

                if (DateTime.Compare(DateTime.Today, maxDate) > 0)
                {
                    main.JobDate = minDate;
                }

            }

            //return main.JobDate;
            return minDate;
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
            ViewBag.svcId = serviceId;
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
                return RedirectToAction("CreateCustomer", new { CreateCustJobId = jobid } );
            }

            ViewBag.Status = new SelectList(StatusList, "value", "text", data.Status);
            ViewBag.mainid = jobid;
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyDetail([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer, int mainid, int? sortid)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { mainid = mainid});
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
        public ActionResult CreateCustomer([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer, int? sortid)
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
            ViewBag.mainid = jobid;
            ViewBag.CustomerId = new SelectList(db.Customers.Where(d => d.Status == "ACT"), "Id", "Name", jobMain.CustomerId);
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", jobMain.BranchId);
            ViewBag.JobStatusId = new SelectList(db.JobStatus, "Id", "Status", jobMain.JobStatusId);
            ViewBag.JobThruId = new SelectList(db.JobThrus, "Id", "Desc", jobMain.JobThruId);
            return View(jobMain);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobDetails([Bind(Include = "Id,JobDate,CustomerId,Description,NoOfPax,NoOfDays,AgreedAmt,JobRemarks,JobStatusId,StatusRemarks,BranchId,JobThruId,CustContactEmail,CustContactNumber")] JobMain jobMain)
        {
            if (ModelState.IsValid)
            {
                if (jobMain.CustContactEmail == null && jobMain.CustContactNumber == null)
                {
                    var cust = db.Customers.Find(jobMain.CustomerId);
                    jobMain.CustContactEmail = cust.Email;
                    jobMain.CustContactNumber = cust.Contact1;
                }

                db.Entry(jobMain).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index", new { mainid = jobMain.Id });

            }

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

                //if (jobMain.CustomerId == NewCustSysId)
                //    return RedirectToAction("CreateCustomer",new { CreateCustJobId = jobMain.Id });
                //else
                //    return RedirectToAction("Index");
                //return RedirectToAction("Services", "JobServices", new { id = jobMain.Id });
                //return RedirectToAction("JobTable", new { span = 30 });

                return RedirectToAction("Index");

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

            return RedirectToAction("Index", new { mainid = jobMain.Id});
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

                return RedirectToAction("Index", new { sortid= 1, serviceId = SrcId });
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
            ViewBag.id = JobMainId;
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
            if (ModelState.IsValid)
            {
                jobServices.DtEnd = ((DateTime)jobServices.DtEnd).Add(new TimeSpan(23, 59, 59));
                db.JobServices.Add(jobServices);
                db.SaveChanges();
            }

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);

            return RedirectToAction("Index", "JobOrder");
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
            ViewBag.svcId = jobServices.Id;
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

                //db.SaveChanges();
                updateJobDate(jobServices.JobMainId);
                db.SaveChanges();

            }


            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobServices.JobMainId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", jobServices.SupplierId);
            ViewBag.ServicesId = new SelectList(db.Services, "Id", "Name", jobServices.ServicesId);
            ViewBag.SupplierItemId = new SelectList(db.SupplierItems, "Id", "Description", jobServices.SupplierItemId);

            return RedirectToAction("Index", new { serviceId = jobServices.Id });

        }

        public void updateJobDate(int mainId) {


            //update jobdate
            var main = db.JobMains.Where(j => mainId == j.Id).FirstOrDefault();

            //loop though all jobservices in the jobmain
            //to get the latest date
            foreach (var svc in db.JobServices.Where(s => s.JobMainId == main.Id))
            {
                //get least latest date
                if (DateTime.Compare(DateTime.Today, (DateTime)svc.DtStart) >= 0)   //if today is later than datestart, assign datestart to jobdate, 
                {

                    if (DateTime.Compare(DateTime.Today, (DateTime)svc.DtEnd) <= 0) //if today earlier than date end, assign jobdate today
                    {
                        //assign date today
                        main.JobDate = DateTime.Today;
                        db.Entry(main).State = EntityState.Modified;
                    }
                    else
                    {
                        //assign latest basin on today
                        main.JobDate = (DateTime)svc.DtStart;
                        db.Entry(main).State = EntityState.Modified;
                    }

                }
                else  //if today is earlier than datestart, assign datestart to jobdate, 
                {
                    //main.JobDate = (DateTime)svc.DtStart;
                    //db.Entry(main).State = EntityState.Modified;
                }
                //db.SaveChanges();
            }
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
            ViewBag.svcId = id;
            return View(jobServices);
        }


        public ActionResult JobSvcDelete(int? id) {

            JobServices jobServices = db.JobServices.Find(id);
            int jId = jobServices.JobMainId;
            db.JobServices.Remove(jobServices);
            db.SaveChanges();
            return RedirectToAction("Index", "JobOrder", new { mainid = jobServices.JobMainId});
        }

        public ActionResult notify() {
            DBClasses dbc = new DBClasses();
            dbc.addNotification("Job Order","Test");
            return RedirectToAction("Index", "JobOrder", new { sortid = 1 });
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
                svcpu.ClientName = svc.JobMain.Description;
                svcpu.ClientContact = svc.JobMain.CustContactNumber;
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

            return RedirectToAction("JobServicePickup", new { id = svcpu.Id });
        }


        public ActionResult JobServicePickup(int? id)
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
            ViewBag.svcId = jobServicePickup.JobServicesId;
            return View(jobServicePickup);
        }


        [HttpPost, ActionName("JobServicePickup")]
        public ActionResult JobServicePickup([Bind(Include = "Id,JobServicesId,JsDate,JsTime,JsLocation,ClientName,ClientContact,ProviderName,ProviderContact")] JobServicePickup jobServicePickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobServicePickup).State = EntityState.Modified;
                db.SaveChanges();

                int ij = db.JobServices.Find(jobServicePickup.JobServicesId).JobMainId;

                return RedirectToAction("Index", new { serviceid = jobServicePickup.JobServicesId });
            }
            //ViewBag.JobServicesId = new SelectList(db.JobServices, "Id", "Particulars", jobServicePickup.JobServicesId);

            return View(jobServicePickup);

        }


        public ActionResult JobServices(int? JobMainId, int? serviceId)
        {
            ViewBag.JobMainId = JobMainId;

            var Job = db.JobMains.Where(d => d.Id == JobMainId).FirstOrDefault();
            ViewBag.JobOrder = Job;

            var jobServices = db.JobServices.Include(j => j.JobMain).Include(j => j.Supplier).Include(j => j.Service).Include(j => j.SupplierItem).Include(j => j.JobServicePickups).Where(d => d.JobMainId == JobMainId);
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

            ViewBag.Itineraries = db.JobItineraries.Where(d => d.JobMainId == JobMainId).ToList();
            
            return View(jobServices.OrderBy(d => d.DtStart).ToList());

        }
        
        // GET: JobMains/Details/5
        public ActionResult BookingDetails(int? id, int? iType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobMain = db.JobMains.Find(id);
            if (jobMain == null)
            {
                return HttpNotFound();
            }

            ViewBag.Services = db.JobServices.Include(j => j.JobServicePickups).Where(j => j.JobMainId == jobMain.Id).OrderBy(s => s.DtStart);
            ViewBag.Itinerary = db.JobItineraries.Include(j => j.Destination).Where(j => j.JobMainId == jobMain.Id);
            ViewBag.Payments = db.JobPayments.Where(j => j.JobMainId == jobMain.Id);
            ViewBag.jNotes = db.JobNotes.Where(d => d.JobMainId == jobMain.Id).OrderBy(s => s.Sort);

            //Default form
            string sCompany = "AJ88 Car Rental Services";
            string sLine1 = "2nd Floor J. Sulit Bldg. Mac Arthur Highway, Matina Davao City ";
            string sLine2 = "Tel# (+63)822971831; (+63)9167558473; (+63)9330895358 ";
            string sLine3 = "Email: ajdavao88@gmail.com; Website: http://www.AJDavaoCarRental.com/";
            string sLine4 = "TIN: 414-880-772-001 (non-Vat)";
            string sLogo = "LOGO_AJRENTACAR.jpg";
            Bank bank = db.Banks.Find(5);

            if (jobMain.Branch.Name == "RealBreeze")
            {
                sCompany = "Real Breeze Travel & Tours - Davao City";
                sLine1 = "2nd Floor J. Sulit Bldg. Mac Arthur Highway, Matina Davao City ";
                sLine2 = "Tel# (+63)822971831; (+63)916-755-8473; (+63)933-089-5358 ";
                sLine3 = "Email: RealBreezeDavao@gmail.com; Website: http://www.realbreezedavaotours.com//";
                sLine4 = "TIN: 414-880-772-000 (non-Vat)";
                sLogo = "RealBreezeLogo01.png";
                bank = db.Banks.Find(6);
            }

            if (jobMain.Branch.Name == "AJ88")
            {
                sCompany = "AJ88 Car Rental Services";
                sLine1 = "2nd Floor J. Sulit Bldg. Mac Arthur Highway, Matina Davao City ";
                sLine2 = "Tel# (+63)822971831; (+63)9167558473; (+63)9330895358 ";
                sLine3 = "Email: ajdavao88@gmail.com; Website: http://www.AJDavaoCarRental.com/";
                sLine4 = "TIN: 414-880-772-001 (non-Vat)";
                sLogo = "LOGO_AJRENTACAR.jpg";
                bank = db.Banks.Find(5);
            }

            ViewBag.sCompany = sCompany;
            ViewBag.sLine1 = sLine1;
            ViewBag.sLine2 = sLine2;
            ViewBag.sLine3 = sLine3;
            ViewBag.sLine4 = sLine4;
            ViewBag.sLogo = sLogo;

            ViewBag.BankName = bank.BankName;
            ViewBag.BankBranch = bank.BankBranch;
            ViewBag.AccName = bank.AccntName;
            ViewBag.AccNum = bank.AccntNo;

            if (jobMain.JobStatusId == 1) //quotation
                return View("Details_Quote", jobMain);

            if (iType != null && (int)iType == 1) //Invoice
                return View("Details_Invoice", jobMain);

            return View(jobMain);
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
                Decimal quote = (svc.QuotedAmt == null ? 0 : (decimal)svc.QuotedAmt);

                svcpu = svc.JobServicePickups.FirstOrDefault();
                sData += "\nDate:" + ((DateTime)svc.DtStart).ToString("dd MMM yyyy (ddd)");
                sData += "\nTime&Location:" + svcpu.JsTime + " " + svcpu.JsLocation;
                sData += "\nGuest:" + svcpu.ClientName + " #" + svcpu.ClientContact;
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


        //web service call to send notification
        public void Notification(int id)
        {
            SMSWebService ws = new SMSWebService();
            ws.AddNotification(id);
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
                    where datediff(day, getdate(),a.DtEntered) > - 1

                    union

                    select 
                    'SalesLead Activity' as StatusCategory,
                    a.DtActivity dtTaken,
                    a.SalesLeadId refId,
                    c.Name + ' - ' + a.Particulars Details
                    from 
                    SalesActivities a
                    left join SalesLeads as s on s.Id = a.SalesLeadId 
                    left join Customers as c on c.Id = s.CustomerId 
                    where a.SalesActStatusId=2 AND datediff(day, getdate(),a.DtActivity) > -2

                    union

                    select 
                    'SalesStatus Activity' as StatusCategory,
                    a.DtStatus dtTaken,
                    a.SalesLeadId refId,
                    s.CustName + ' - ' + s.Details + ' - '+ sc.Name as Details
                    from 
                    SalesStatus a
                    left join SalesLeads as s on s.Id = a.SalesLeadId 
                    left join Customers as c on c.Id = s.CustomerId 
                    left join SalesStatusCodes as sc on sc.Id = a.SalesStatusCodeId 
                    where datediff(day, getdate(),a.DtStatus) > -1

                    union

                    select 
                    'New JobOrder' as StatusCategory,
                    j.JobDate as dtTaken,
                    j.Id as refId,
                    c.Name +' - '+ j.Description as Details
                    from 
                    JobMains j 
                    left join Customers as c on c.Id = j.CustomerId 
                    where datediff(day, getdate(),j.JobDate) > - 1

                    union

                    select 
                    'JobOrder Activity' as StatusCategory,
                    j.DtPerformed as dtTaken,
                    j.JobServicesId as refId,
                    cu.Name +' - '+ js.Particulars +' / '+ 
                    c.CatCode +' - '+ j.Remarks as Details
                    from 
                    JobActions j 
                    left join SrvActionItems as s on s.Id = j.SrvActionItemId 
                    left join SrvActionCodes as c on c.Id = s.SrvActionCodeId 
                    left join JobServices as js on js.Id = j.JobServicesId 
                    left join JobMains as m on m.Id = js.JobMainId 
                    left join Customers as cu on cu.Id = m.CustomerId 

                    where datediff(day, getdate(),j.DtPerformed) > - 1
                    Order by dtTaken
                    ;
                    ").ToList();

                return View(updates);
  
        }
        #endregion

        #region Payments
        
        // GET: JobPayments
        public ActionResult PaymentIndex()
        {
            var jobPayments = db.JobPayments.Include(j => j.JobMain).Include(j => j.Bank);
            return View(jobPayments.ToList());
        }

        public ActionResult PaymentAdvanceList()
        {
            var payments = db.JobPayments.Include(j => j.JobMain).Include(j => j.Bank)
                .Where(d => d.JobMain.JobStatusId != 4 && d.JobMain.JobStatusId != 5)
                .OrderBy(d => d.DtPayment);
            return View(payments);
        }

        public ActionResult Payments(int? id)
        {
            ViewBag.JobMainId = id;

            var Job = db.JobMains.Where(d => d.Id == id).FirstOrDefault();
            ViewBag.JobOrder = Job;
            ViewBag.mainid = id;

            var jobPayments = db.JobPayments.Include(j => j.JobMain).Include(j => j.Bank).Where(d => d.JobMainId == id);
            return View("Paymentindex", jobPayments.ToList());
        }
        // GET: JobPayments/Details/5
        public ActionResult PaymentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPayment jobPayment = db.JobPayments.Find(id);
            if (jobPayment == null)
            {
                return HttpNotFound();
            }
            return View(jobPayment);
        }

        // GET: JobPayments/Create , remarks = "Partial Payment" 
        public ActionResult PaymentCreate(int? JobMainId, string remarks)
        {
            Models.JobPayment jp = new JobPayment();
            jp.JobMainId = (int)JobMainId;
            jp.DtPayment = DateTime.Now;
            jp.Remarks = remarks;

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description");
            ViewBag.BankId = new SelectList(db.Banks, "Id", "BankName");

            return View(jp);


        }

        // GET: JobPayments/Create , remarks = "Partial Payment" 
        public ActionResult PaymentCreatePG(int? JobMainId, string remarks)
        {

            JobPayment jobPayment = new JobPayment();
            jobPayment.BankId = 4;
            jobPayment.DtPayment = DateTime.Now;
            jobPayment.JobMainId = (int)JobMainId;
            jobPayment.PaymentAmt = 0;
            jobPayment.Remarks = remarks;

            db.JobPayments.Add(jobPayment);
            db.SaveChanges();

            ViewBag.JobMainId = JobMainId;

            var Job = db.JobMains.Where(d => d.Id == JobMainId).FirstOrDefault();
            ViewBag.JobOrder = Job;

            var jobPayments = db.JobPayments.Include(j => j.JobMain).Include(j => j.Bank).Where(d => d.JobMainId == JobMainId);
            return View("Paymentindex", jobPayments.ToList());
        }

        
        // POST: JobPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentCreate([Bind(Include = "Id,JobMainId,DtPayment,PaymentAmt,Remarks,BankId")] JobPayment jobPayment)
        {
            if (ModelState.IsValid)
            {
                db.JobPayments.Add(jobPayment);
                db.SaveChanges();
                return RedirectToAction("Payments", new { id = jobPayment.JobMainId });
            }

            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobPayment.JobMainId);
            ViewBag.BankId = new SelectList(db.Banks, "Id", "BankName", jobPayment.BankId);
            return View(jobPayment);
        }

        // GET: JobPayments/Edit/5
        public ActionResult PaymentEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPayment jobPayment = db.JobPayments.Find(id);
            if (jobPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobPayment.JobMainId);
            ViewBag.BankId = new SelectList(db.Banks, "Id", "BankName", jobPayment.BankId);
            return View(jobPayment);
        }

        // POST: JobPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentEdit([Bind(Include = "Id,JobMainId,DtPayment,PaymentAmt,Remarks,BankId")] JobPayment jobPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Payments", new { id = jobPayment.JobMainId });
            }
            ViewBag.JobMainId = new SelectList(db.JobMains, "Id", "Description", jobPayment.JobMainId);
            ViewBag.BankId = new SelectList(db.Banks, "Id", "BankName", jobPayment.BankId);
            return View(jobPayment);
        }

        // GET: JobPayments/Delete/5
        public ActionResult PaymentDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPayment jobPayment = db.JobPayments.Find(id);
            if (jobPayment == null)
            {
                return HttpNotFound();
            }
            return View(jobPayment);
        }

        // POST: JobPayments/Delete/5
        [HttpPost, ActionName("PaymentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentDeleteConfirmed(int id)
        {
            JobPayment jobPayment = db.JobPayments.Find(id);
            int tmpId = jobPayment.JobMainId;
            db.JobPayments.Remove(jobPayment);
            db.SaveChanges();
            return RedirectToAction("Payments", new { id = tmpId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}