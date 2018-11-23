using JobsV1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsV1.Controllers
{
    public class ReportingController : Controller
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
        private DBClasses dbc = new DBClasses();

        // GET: Reporting
        public ActionResult Index(int? id, int? reportId)
        {
            if(id != null)
            {
                ViewBag.Id = id;
            }

            ViewBag.reportId = reportId != null ? reportId : 1;

            return View();
        }


        #region Joblisting

        public PartialViewResult JobListing(int? id,string sDate, string eDate, int? sortid, int? serviceId, int? mainid)
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
                            
                             
            IEnumerable<Models.JobMain> jobMains = db.JobMains
                .Include(j => j.Customer)
                .Include(j => j.Branch)
                .Include(j => j.JobStatus)
                .Include(j => j.JobThru)
                ;

            List<cjobCounter> jobActionCntr = getJobActionCount(jobMains.Select(d => d.Id).ToList());
            var data = new List<cJobOrder>();

            DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));

            ViewBag.today = today;
            today = today.Date;

            /*
            switch (sortid)
            {
                case 1: //OnGoing
                    jobMains = jobMains
                        .Where(d => (d.JobStatusId != JOBCLOSED || d.JobStatusId != JOBCANCELLED)).ToList()
                        .Where(p => DateTime.Compare(p.JobDate.Date, today.Date.AddDays(-60)) >= 0).ToList();   //get 1 month before all entries
                    break;
                case 2: //prev
                    jobMains = jobMains
                        .Where(d => (d.JobStatusId != JOBCLOSED || d.JobStatusId != JOBCANCELLED)).ToList()
                        .Where(p => DateTime.Compare(p.JobDate.Date, today.Date) < 0).ToList(); //get 1 month before all entries

                    break;
                case 3: //close
                    jobMains = jobMains
                        .Where(d => (d.JobStatusId == JOBCLOSED || d.JobStatusId == JOBCANCELLED)).ToList()
                        .Where(p => p.JobDate.Date.AddDays(60) > today.Date).ToList();

                    break;

                default:
                    jobMains = jobMains.ToList();
                    break;
            }
            */

            /*
             * Get date range 
             **/

            if (sDate != null || eDate != null )
            {
                DateTime startDateRange = DateTime.Parse(sDate.ToString());
                DateTime endDateRange = DateTime.Parse(eDate.ToString());
                
                jobMains = jobMains.Where(p => DateTime.Compare(p.JobDate.Date, startDateRange) >= 0 && DateTime.Compare(p.JobDate.Date, endDateRange) <= 0).ToList();
            }
           
            jobMains = jobMains.ToList();

            foreach (var main in jobMains)
            {
                cJobOrder joTmp = new cJobOrder();
                joTmp.Main = main;
                joTmp.Services = new List<cJobService>();
                joTmp.Main.AgreedAmt = 0;
                joTmp.Payment = 0;

                List<Models.JobServices> joSvc = db.JobServices.Where(d => d.JobMainId == main.Id).OrderBy(s => s.DtStart).ToList();
                foreach (var svc in joSvc)
                {
                    cJobService cjoTmp = new cJobService();
                    cjoTmp.Service = svc;

                    var ActionDone = db.JobActions.Where(d => d.JobServicesId == svc.Id).Select(s => s.SrvActionItemId);

                    cjoTmp.SvcActions = db.SrvActionItems.Where(d => d.ServicesId == svc.ServicesId && !ActionDone.Contains(d.Id)).Include(d => d.SrvActionCode);
                    cjoTmp.Actions = db.JobActions.Where(d => d.JobServicesId == svc.Id).Include(d => d.SrvActionItem);
                    cjoTmp.SvcItems = db.JobServiceItems.Where(d => d.JobServicesId == svc.Id).Include(d => d.InvItem);
                    cjoTmp.SupplierPos = db.SupplierPoDtls.Where(d => d.JobServicesId == svc.Id).Include(i => i.SupplierPoHdr);
                    joTmp.Main.AgreedAmt += svc.ActualAmt;

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

            /*
            switch (sortid)
            {
                case 1: //OnGoing
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBINQUIRY || d.Main.JobStatusId == JOBRESERVATION || d.Main.JobStatusId == JOBCONFIRMED)).ToList()
                       .Where(d => d.Main.JobDate.CompareTo(today.Date) >= 0).ToList();

                    break;
                case 2: //prev
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBINQUIRY || d.Main.JobStatusId == JOBRESERVATION || d.Main.JobStatusId == JOBCONFIRMED)).ToList()
                        .Where(p => DateTime.Compare(p.Main.JobDate.Date, today.Date) < 0).ToList();

                    break;
                case 3: //close
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBCLOSED || d.Main.JobStatusId == JOBCANCELLED)).ToList()
                        .Where(p => p.Main.JobDate.AddDays(60).Date > today.Date).ToList();
                    break;

                default:
                    data = (List<cJobOrder>)data.ToList();
                    break;
            }
            */

            if (sDate != null || eDate != null)
            {
                DateTime startDateRange = DateTime.Parse(sDate.ToString());
                DateTime endDateRange = DateTime.Parse(eDate.ToString());

                data = (List<cJobOrder>)data.Where(p => DateTime.Compare(p.Main.JobDate.Date, startDateRange) >= 0 && DateTime.Compare(p.Main.JobDate.Date, endDateRange) <= 0).ToList();
            }

            List<Customer> customers = db.Customers.ToList();
            ViewBag.companyList = customers;

            var jobmainId = serviceId != null ? db.JobServices.Find(serviceId).JobMainId : 0;
            jobmainId = mainid != null ? (int)mainid : jobmainId;

            ViewBag.mainId = jobmainId;
            ViewBag.sDate = sDate;
            ViewBag.eDate = eDate;
            
            if ( id != null )
            {
                return PartialView("JobListingPrint", data.Where(c=>c.Main.Id == id));
            }
            return PartialView("JobListingPrint", data.OrderByDescending(d => d.Main.JobDate));
            
        }


        public DateTime TempJobDate(int mainId)
        {
            //update jobdate
            var main = db.JobMains.Where(j => mainId == j.Id).FirstOrDefault();
            DateTime minDate = db.JobMains.Where(j => mainId == j.Id).FirstOrDefault().JobDate.Date;
            DateTime maxDate = new DateTime(1, 1, 1);

            DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
            today = today.Date;
            //loop though all jobservices in the jobmain
            //to get the latest date
            foreach (var svc in db.JobServices.Where(s => s.JobMainId == mainId).OrderBy(s => s.DtStart))
            {
                var svcDtStart = (DateTime)svc.DtStart;
                var svcDtEnd = (DateTime)svc.DtEnd;
                //get min date
                // minDate = (DateTime)svc.DtStart;
                if (DateTime.Compare(minDate, svcDtStart.Date) >= 0)
                {
                    minDate = svcDtStart.Date; //if minDate > Dtstart
                }

                if (DateTime.Compare(today, svcDtStart.Date) >= 0 && DateTime.Compare(today, svcDtEnd.Date) <= 0)
                {
                    minDate = today;
                    //skip
                }
                else
                {
                    if (DateTime.Compare(today, svcDtStart.Date) < 0 && DateTime.Compare(today, minDate) > 0)
                    {
                        minDate = svcDtStart.Date; //if Today > Dtstart
                    }
                }

                //get max date
                if (DateTime.Compare(maxDate, svcDtEnd.Date) <= 0)
                {
                    maxDate = svcDtEnd.Date;
                }
            }

            if (DateTime.Compare(today, minDate) == 0)
            {
                main.JobDate = minDate;
            }

            if (DateTime.Compare(today, maxDate) == 0)
            {
                main.JobDate = maxDate;
            }

            if (DateTime.Compare(today, minDate) < 0)
            {
                main.JobDate = minDate;
            }

            if (DateTime.Compare(today, minDate) > 0)
            {
                if (DateTime.Compare(today, maxDate) < 0)
                {
                    main.JobDate = today;
                }

                if (DateTime.Compare(today, maxDate) > 0)
                {
                    main.JobDate = minDate;
                }

            }

            if (minDate == new DateTime(9999, 12, 30))
            {

                minDate = db.JobMains.Where(j => mainId == j.Id).FirstOrDefault().JobDate;

            }
            //return main.JobDate;
            return minDate;
        }


        public List<cjobCounter> getJobActionCount(List<Int32> jobidlist)
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
            List<cjobCounter> jobcntr = db.Database.SqlQuery<cjobCounter>(sqlstr).Where(d => jobidlist.Contains(d.JobId)).ToList();
            return jobcntr;
        }


        public ActionResult JobListingPrint(int? id, string sDate, string eDate, int? sortid, int? serviceId, int? mainid)
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


            IEnumerable<Models.JobMain> jobMains = db.JobMains
                .Include(j => j.Customer)
                .Include(j => j.Branch)
                .Include(j => j.JobStatus)
                .Include(j => j.JobThru)
                ;

            List<cjobCounter> jobActionCntr = getJobActionCount(jobMains.Select(d => d.Id).ToList());
            var data = new List<cJobOrder>();

            DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));

            ViewBag.today = today;
            today = today.Date;

            /*
            switch (sortid)
            {
                case 1: //OnGoing
                    jobMains = jobMains
                        .Where(d => (d.JobStatusId != JOBCLOSED || d.JobStatusId != JOBCANCELLED)).ToList()
                        .Where(p => DateTime.Compare(p.JobDate.Date, today.Date.AddDays(-60)) >= 0).ToList();   //get 1 month before all entries
                    break;
                case 2: //prev
                    jobMains = jobMains
                        .Where(d => (d.JobStatusId != JOBCLOSED || d.JobStatusId != JOBCANCELLED)).ToList()
                        .Where(p => DateTime.Compare(p.JobDate.Date, today.Date) < 0).ToList(); //get 1 month before all entries

                    break;
                case 3: //close
                    jobMains = jobMains
                        .Where(d => (d.JobStatusId == JOBCLOSED || d.JobStatusId == JOBCANCELLED)).ToList()
                        .Where(p => p.JobDate.Date.AddDays(60) > today.Date).ToList();

                    break;

                default:
                    jobMains = jobMains.ToList();
                    break;
            }
            */

            /*
             * Get date range 
             **/

            if (sDate != null || eDate != null)
            {
                DateTime startDateRange = DateTime.Parse(sDate.ToString());
                DateTime endDateRange = DateTime.Parse(eDate.ToString());

                jobMains = jobMains.Where(p => DateTime.Compare(p.JobDate.Date, startDateRange) >= 0 && DateTime.Compare(p.JobDate.Date, endDateRange) <= 0).ToList();
            }

            jobMains = jobMains.ToList();

            foreach (var main in jobMains)
            {
                cJobOrder joTmp = new cJobOrder();
                joTmp.Main = main;
                joTmp.Services = new List<cJobService>();
                joTmp.Main.AgreedAmt = 0;
                joTmp.Payment = 0;

                List<Models.JobServices> joSvc = db.JobServices.Where(d => d.JobMainId == main.Id).OrderBy(s => s.DtStart).ToList();
                foreach (var svc in joSvc)
                {
                    cJobService cjoTmp = new cJobService();
                    cjoTmp.Service = svc;

                    var ActionDone = db.JobActions.Where(d => d.JobServicesId == svc.Id).Select(s => s.SrvActionItemId);

                    cjoTmp.SvcActions = db.SrvActionItems.Where(d => d.ServicesId == svc.ServicesId && !ActionDone.Contains(d.Id)).Include(d => d.SrvActionCode);
                    cjoTmp.Actions = db.JobActions.Where(d => d.JobServicesId == svc.Id).Include(d => d.SrvActionItem);
                    cjoTmp.SvcItems = db.JobServiceItems.Where(d => d.JobServicesId == svc.Id).Include(d => d.InvItem);
                    cjoTmp.SupplierPos = db.SupplierPoDtls.Where(d => d.JobServicesId == svc.Id).Include(i => i.SupplierPoHdr);
                    joTmp.Main.AgreedAmt += svc.ActualAmt;

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

            /*
            switch (sortid)
            {
                case 1: //OnGoing
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBINQUIRY || d.Main.JobStatusId == JOBRESERVATION || d.Main.JobStatusId == JOBCONFIRMED)).ToList()
                       .Where(d => d.Main.JobDate.CompareTo(today.Date) >= 0).ToList();

                    break;
                case 2: //prev
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBINQUIRY || d.Main.JobStatusId == JOBRESERVATION || d.Main.JobStatusId == JOBCONFIRMED)).ToList()
                        .Where(p => DateTime.Compare(p.Main.JobDate.Date, today.Date) < 0).ToList();

                    break;
                case 3: //close
                    data = (List<cJobOrder>)data
                        .Where(d => (d.Main.JobStatusId == JOBCLOSED || d.Main.JobStatusId == JOBCANCELLED)).ToList()
                        .Where(p => p.Main.JobDate.AddDays(60).Date > today.Date).ToList();
                    break;

                default:
                    data = (List<cJobOrder>)data.ToList();
                    break;
            }
            */

            if (sDate != null || eDate != null)
            {
                DateTime startDateRange = DateTime.Parse(sDate.ToString());
                DateTime endDateRange = DateTime.Parse(eDate.ToString());

                data = (List<cJobOrder>)data.Where(p => DateTime.Compare(p.Main.JobDate.Date, startDateRange) >= 0 && DateTime.Compare(p.Main.JobDate.Date, endDateRange) <= 0).ToList();
            }

            List<Customer> customers = db.Customers.ToList();
            ViewBag.companyList = customers;

            var jobmainId = serviceId != null ? db.JobServices.Find(serviceId).JobMainId : 0;
            jobmainId = mainid != null ? (int)mainid : jobmainId;

            ViewBag.mainId = jobmainId;
            ViewBag.sDate = sDate;
            ViewBag.eDate = eDate;

            if (id != null)
            {
                return View("JobListing", data.Where(c => c.Main.Id == id));
            }
            return View("JobListing", data.OrderByDescending(d => d.Main.JobDate));

        }

        #endregion 

        #region Payments Report


        public PartialViewResult Payments()
        {
            return PartialView(db.JobPayments.ToList());
        }

        public ActionResult PaymentsPrint()
        {
            return View("Payments", db.JobPayments.ToList());

            //return View(db.JobPayments.ToList());
        }

        #endregion
    }
}