using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloodline.Models;

namespace Bloodline.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string userName, string password)
        {

            using (DBModel db = new DBModel())
            {

                var check = (from us in db.logins
                             where us.username == userName && us.password == password
                             select us).FirstOrDefault();
                if (check != null)
                {
                    return Json(new JsonResult()
                    {
                        Data = check
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new JsonResult()
                    {
                        Data = ""
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}