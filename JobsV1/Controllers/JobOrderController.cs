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



namespace JobsV1.Controllers
{
    public class cJobOrder
    {
        [Key]
        public int Id { get; set; }
        public Models.JobMain Main { get; set; }
        public List<cJobService> Services { get; set; }
    }

    public class cJobService
    {
        [Key]
        public int Id { get; set; }
        public Models.JobServices Service { get; set; }
        public IQueryable<Models.JobAction> Actions { get; set; }
        public IQueryable<Models.SrvActionItem> SvcActions { get; set; }
        public IQueryable<Models.JobServiceItem> SvcItems { get; set; }
    }

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

                    cjoTmp.SvcActions = db.SrvActionItems.Where(d => d.ServicesId == svc.ServicesId).Include(d => d.SrvActionCode);
                    cjoTmp.Actions = db.JobActions.Where(d => d.JobServicesId == svc.Id).Include(d=>d.SrvActionItem);
                    cjoTmp.SvcItems = db.JobServiceItems.Where(d => d.JobServicesId == svc.Id).Include(d => d.InvItem);

                    joTmp.Services.Add(cjoTmp);
                }

                data.Add(joTmp);
            }

            return View(data);
        }

        public ActionResult ActionDone(int srvactionitemid, int svcid)
        {
            Models.JobAction newAction = new JobAction();
            newAction.DtPerformed = DateTime.Now;
            newAction.PerformedBy = "abel";
            newAction.Remarks = "Done";
            newAction.JobServicesId = svcid;
            newAction.SrvActionItemId = srvactionitemid;

            db.JobActions.Add(newAction);
            db.SaveChanges();

            return RedirectToAction("index");

        }

    }
}