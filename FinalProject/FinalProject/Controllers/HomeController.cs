using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        TestingContext db = new TestingContext();

        public ActionResult Index()
        {
            IEnumerable<User> users = db.Users;

            IEnumerable<Questionnaire> questionnaires = db.Questionnaires.ToList();

            ViewBag.Questionnaires = questionnaires;

            ViewBag.Users = users;

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}