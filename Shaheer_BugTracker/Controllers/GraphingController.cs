using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shaheer_BugTracker.Controllers
{
    public class GraphingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Graphing
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult ProduceChart1Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach(var priority in db.TicketPriorities.ToList())
            {
                data = new MorrisBarData();
                data.label = priority.PriorityName;
                data.value = db.Tickets.Where(t => t.TicketPriority.PriorityName
                == priority.PriorityName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart2Data()
        {
            var myData = new List<MorrisBarData>();
            foreach(var status in db.TicketStatuses.ToList())
            {
                myData.Add(new MorrisBarData
                {
                    label = status.StatusName,
                    value = db.Tickets.Where(t => t.TicketStatus.StatusName
                    == status.StatusName).Count()
                });
            }
            return Json(myData);
        }

        public JsonResult ProduceChart3Data()
        {
            var myData = new List<MorrisBarData>();
            foreach(var proj in db.Projects.ToList())
            {
                var projId = proj.Id;

                myData.Add(new MorrisBarData
                {
                    label = proj.ProjectName,
                    value = db.Projects.Where(p => p.Id == projId).SelectMany(p => p.Users).Count(),
                });
            }
            return Json(myData);
        }
    }
}