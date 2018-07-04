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
    public class ItemSchedule
    {
        public int ItemId { get; set; }
        public Models.InvItem Item { get; set; }
        public List<ItemDay> DayStatus { get; set; }
    }
    public class ItemDay
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public int status { get; set; }
    }

    public class cItemSchedule
    {
        public int ItemId { get; set; }
        public int JobId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DtStart { get; set; }
        public DateTime DtEnd { get; set; }
    }

    public class InvItemsController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: InvItems
        public ActionResult Index()
        {
            return View(db.InvItems.ToList());
        }

        public ActionResult ItemSchedules()
        {
            #region get itemJobs
            string SqlStr = @"
select  b.InvItemId, c.JobMainId, c.Id ServiceId, c.Particulars, c.DtStart, c.DtEnd from 
InvItems a
left outer join JobServiceItems b on b.InvItemId = a.Id 
left outer join JobServices c on b.JobServicesId = c.Id
;";
            List<cItemSchedule> itemJobs = db.Database.SqlQuery<cItemSchedule>(SqlStr).ToList();

            //cItemSchedule
            #endregion

            int NoOfDays = 10;
            List<ItemSchedule> ItemSched = new List<ItemSchedule>();

            var InvItems = db.InvItems.ToList();
            var ItemId = db.InvItems.Select(s => s.Id).ToList();


            foreach ( var d in InvItems)
            {
                ItemSchedule ItemTmp = new ItemSchedule();

                ItemTmp.ItemId = d.Id;
                ItemTmp.Item = d;

                //var JobServiceList = itemJobs.Where(d=>d.ItemId == d.)


                foreach ( var jsItem in JobServiceList )
                {

                }

                //foreach()

                

                ItemSched.Add(ItemTmp);
            }

            //Models.InvItem item =
            return View();
        }


        

        // GET: InvItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvItem invItem = db.InvItems.Find(id);
            if (invItem == null)
            {
                return HttpNotFound();
            }
            return View(invItem);
        }

        // GET: InvItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemCode,Description,Remarks,ImgPath")] InvItem invItem)
        {
            if (ModelState.IsValid)
            {
                db.InvItems.Add(invItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invItem);
        }

        // GET: InvItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvItem invItem = db.InvItems.Find(id);
            if (invItem == null)
            {
                return HttpNotFound();
            }
            return View(invItem);
        }

        // POST: InvItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemCode,Description,Remarks,ImgPath")] InvItem invItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invItem);
        }

        // GET: InvItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvItem invItem = db.InvItems.Find(id);
            if (invItem == null)
            {
                return HttpNotFound();
            }
            return View(invItem);
        }

        // POST: InvItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvItem invItem = db.InvItems.Find(id);
            db.InvItems.Remove(invItem);
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
