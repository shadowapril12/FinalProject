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

        public ActionResult Index(int userId)
        {
            User user = db.Users.Find(userId);

            List<Questionnaire> questionnaires = db.Questionnaires.ToList();

            ViewBag.Questionnaires = questionnaires;

            ViewBag.User = user;

            return View();
        }

        public ActionResult GuestIndex(int userId)
        {
            User user = db.Users.Find(userId);

            List<Questionnaire> questionnaires = db.Questionnaires.ToList();

            ViewBag.Questionnaires = questionnaires;

            ViewBag.User = user;

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}