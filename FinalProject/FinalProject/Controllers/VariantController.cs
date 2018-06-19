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
        public ActionResult Index(int? id)
        {
            ICollection<Variant> variants = db.Variants.Include(v => v.Question).Where(v => v.QuestionId == id).ToList();

            Question q = db.Questions.Find(id);

            Questionnaire questionnaire = db.Questionnaires.Where(e => e.Id == q.QuestionnaireId).FirstOrDefault();

            ViewBag.QuestionnaireId = questionnaire.Id;

            ViewBag.QuestionFormulation = q.Formulation;

            ViewBag.QuestionIdentify = q.Id;

            return View(variants);
        }

        [HttpPost]
        public ActionResult AddVariantForm(int questionId)
        {
            Variant var = new Variant();
            Question ques = db.Questions.Find(questionId);
            var.Question = ques;
            var.QuestionId = ques.Id;

            return View(var);
        }

        [HttpPost]
        public ActionResult AddVariant(Variant var)
        {
            db.Variants.Add(var);

            Question q = db.Questions.Where(p => p.Id == var.QuestionId).FirstOrDefault();

            int questId = q.Id;

            db.SaveChanges();

            return RedirectToAction("Index", "Variant", new { id = questId } );
        }

        [HttpPost]
        public ActionResult MyAction(string action, Variant vari)
        {
            if(action == "delete")
            {
                return RedirectToAction("DeleteVariant", "Variant", new { id = vari.Id });
            }
            else
            {
                //Редактирование будет реализовано позже
                return RedirectToAction("EditVAriantForm", "Variant", new { id = vari.Id });
            }
        }

        public ActionResult EditVariantForm(int id)
        {
            Variant vari = db.Variants.Find(id);

            return View(vari);
        }

        [HttpPost]
        public ActionResult EditVariant(Variant variant)
        {
            Question q = db.Questions.Where(p => p.Id == variant.QuestionId).FirstOrDefault();

            db.Entry(variant).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Index", "Variant", new { id = q.Id});
        }

        public ActionResult DeleteVariant(int id)
        {
            Variant var = db.Variants.Find(id);

            List<Testing> t = db.Testings.Where(te => te.VariantId == var.Id).ToList();          

            int? quesId = var.QuestionId;

            foreach(var el in t)
            {
                db.Testings.Remove(el);
                db.SaveChanges();
            }

            db.Variants.Remove(var);
            db.SaveChanges();

            return RedirectToAction("Index", "Variant", new { id = quesId });
        }

        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}