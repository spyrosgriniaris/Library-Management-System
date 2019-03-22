using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project2k18.Controllers
{
    public class maintenanceController : Controller
    {
        // GET: maintenance
        public ActionResult Index()
        {
            Models.login login = new Models.login();

            return View(login);
        }

        [HttpPost]
        public ActionResult Index(string choice, Models.login login)
        {

            if (choice == "1")
            {
                if (login.username == "admin" && login.password == "admin")
                {
                    return RedirectToAction("Index", "backup");
                }
                else
                {
                    return RedirectToAction("Index", "maintenance");
                }

            }
            else
            {
                if (login.username == "admin" && login.password == "admin")
                {
                    return RedirectToAction("Restore", "backup");
                }
                else
                {
                    return RedirectToAction("Index", "maintenance");
                }

            }

        }
    }
}