using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloodline.Models;
using System.Data.Entity;

namespace Bloodline.Controllers
{
    public class DonorController : Controller
    {
        // GET: Donor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(int id)
        {
            using (DBModel db = new DBModel())
            {
                var rep = (from us in db.reports
                           join dn in db.donors on us.donorID equals dn.donorID
                           where us.donorID == id
                           select new { us.reportID, dn.donorName, us.bloodGroup, us.hmgLvl, us.bsLvl, us.rbc, us.wbc });
                if (rep != null)
                {
                    return Json(new { data = rep.ToList() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
                }

            }
        }
    }
}