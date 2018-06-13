using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class VariantController : Controller
    {
        TestingContext db = new TestingContext();

        // GET: Variant
        //public ActionResult Index(int? id)
        //{
        //    ICollection<Variant> variants = db.Variants.Include(v => v.Question).ToList();

        //    Question q = db.Questions.Find(id);

        //    ViewBag.QuestionFormulation = q.Formulation;

        //    ViewBag.QuestionIdentify = q.

        //    return View(variants);
        //}

        //[HttpPost]
        //public ActionResult AddVariantForm()
        //{

        //}

        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}