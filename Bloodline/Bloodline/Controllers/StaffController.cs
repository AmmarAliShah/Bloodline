using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloodline.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Bloodline.Controllers
{
    public class StaffController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDonors()
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    string dd = "";
                    var dn = (from us in db.donors
                              select us);
                    if (dn != null)
                    {
                        foreach (var item in dn)
                        {
                            dd += "<option value ='" + item.donorID + "'>" + item.donorName + "</option>";
                        }
                        return Json(new { data = dd }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception x)
            {
                return Json(new { status = "error", Data = x.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddReport(int donorID, string bg, string hmg, string bsl, string rbc, string wbc)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    report rpt = new report();
                    rpt.donorID = donorID;
                    rpt.bloodGroup = bg;
                    rpt.hmgLvl = hmg;
                    rpt.bsLvl = bsl;
                    rpt.rbc = rbc;
                    rpt.wbc = wbc;
                    db.reports.Add(rpt);
                    db.SaveChanges();
                    return Json(new { status = "success", Data = rpt.reportID }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception x)
            {

                return Json(new { status = "error", Data = x.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<donor> empList = db.donors.ToList<donor>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new donor());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.donors.Where(x => x.donorID == id).FirstOrDefault<donor>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(donor emp)
        {
            try {
                using (DBModel db = new DBModel())
                {
                    if (emp.donorID == 0)
                    {
                        db.donors.Add(emp);
                        db.SaveChanges();

                        login lg = new login();
                        lg.username = emp.donorName;
                        lg.password = emp.donorDOB;
                        lg.role = "donor";
                        lg.roleID = emp.donorID;
                        db.logins.Add(lg);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            catch (Exception x)
            {
                Console.WriteLine(x);
                return Json(new { status = "error", Data = x.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        /**
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                donor s = db.donors.Where(x => x.donorID == id).FirstOrDefault<donor>();
                db.donors.Remove(s);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    **/
    }
}