using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using System.Data.Entity;

namespace FinalProject.Controllers
{
    public class QuestionnaireController : Controller
    {
        //Контекст
        TestingContext db = new TestingContext();

        // GET: Questionnaire
        public ActionResult Index()
        {
            var questionnaires = db.Questionnaires;

            return View(questionnaires.ToList());
        }

        //Форма добавления анкеты
        public ActionResult AddQuestionnaireForm()
        {
            return View();
        }

        //Метод добавления анкеты
        [HttpPost]
        public ActionResult AddQuestionnaire(Questionnaire questionnaire)
        {
            if (questionnaire.Name != null)
            {
                db.Questionnaires.Add(questionnaire);

                db.SaveChanges();

                return RedirectToAction("Index", "Questionnaire");
            }
            else
            {
                return RedirectToAction("AddQuestionnaireForm", "Questionnaire");
            }

        }

        
        //Форма редактирования анкеты
        [HttpPost]
        public ActionResult EditQuestionnaireForm(Questionnaire questionnaire)
        {
            Questionnaire q = db.Questionnaires.Find(questionnaire.Id);

            return View(q);
        }

        //Метод редактирования анкеты
        [HttpPost]
        public ActionResult EditQuestionnaire(Questionnaire questionnaire)
        {
            db.Entry(questionnaire).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Index", "Questionnaire");
        }

        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
    


}