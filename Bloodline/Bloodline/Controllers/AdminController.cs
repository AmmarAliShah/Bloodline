using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloodline.Models;
using System.Data.Entity;

namespace Bloodline.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Staff
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<staff> empList = db.staffs.ToList<staff>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new staff());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.staffs.Where(x => x.staffID == id).FirstOrDefault<staff>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(staff emp)
        {
            using (DBModel db = new DBModel())
    {
        if (emp.staffID == 0)
        {
            db.staffs.Add(emp);
            db.SaveChanges();

            login lg = new login();
            lg.username = emp.staffName;
            lg.password = emp.staffDOB;
            lg.role = "staff";
            lg.roleID = emp.staffID;
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                staff s = db.staffs.Where(x => x.staffID == id).FirstOrDefault<staff>();
                db.staffs.Remove(s);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddUser(int id)
{
    using (DBModel db = new DBModel())
    {
        staff s = db.staffs.Where(x => x.staffID == id).FirstOrDefault<staff>();
        db.staffs.Remove(s);
        db.SaveChanges();
        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
    }
}

        // Camp

        public ActionResult GetCamp()
        {
            using (DBModel db = new DBModel())
            {
                List<camp> empList = db.camps.ToList<camp>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddorEditCamp(int id = 0)
        {
            if (id == 0)
                return View(new camp());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.camps.Where(x => x.campID == id).FirstOrDefault<camp>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddorEditCamp(camp emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.campID == 0)
                {
                    db.camps.Add(emp);
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
        [HttpPost]
        public ActionResult DeleteCamp(int id)
        {
            using (DBModel db = new DBModel())
            {
                camp s = db.camps.Where(x => x.campID == id).FirstOrDefault<camp>();
                db.camps.Remove(s);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


        // Branch
        public ActionResult GetBranch()
        {
            using (DBModel db = new DBModel())
            {
                List<branch> empList = db.branches.ToList<branch>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddorEditBranch(int id = 0)
        {
            if (id == 0)
                return View(new branch());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.branches.Where(x => x.branchID == id).FirstOrDefault<branch>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddorEditBranch(branch emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.branchID == 0)
                {
                    db.branches.Add(emp);
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
        [HttpPost]
        public ActionResult DeleteBranch(int id)
        {
            using (DBModel db = new DBModel())
            {
                branch s = db.branches.Where(x => x.branchID == id).FirstOrDefault<branch>();
                db.branches.Remove(s);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}