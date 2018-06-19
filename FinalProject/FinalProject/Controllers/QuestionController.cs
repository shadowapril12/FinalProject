using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class QuestionController : Controller
    {
        TestingContext db = new TestingContext();


        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        //Метод получения всех вопросов данной анкеты
        [HttpGet]
        public ActionResult GetQuestions(int? id)
        {
            ICollection<Question> questions = db.Questions.Include(q => q.Questionnaire).Where(q => q.QuestionnaireId == id).ToList();

            //Для привязки добавляемых вопросов к данной анкете, передаем в форму id данной анкеты
            ViewBag.QuestionnaireId = id;

            Questionnaire ques = db.Questionnaires.Find(id);

            ViewBag.QuestionnaireName = ques.Name;

            return View(questions);
        }

        //Вывод формы добавления анкеты
        [HttpPost]
        public ActionResult AddQuestionForm(Question question)
        {
            Question q = new Question();

            q.Questionnaire = db.Questionnaires.Where(p => p.Id == question.QuestionnaireId).FirstOrDefault();

            q.QuestionnaireId = q.Questionnaire.Id;

            return View(q);
        }

        //Добавление вопроса
        [HttpPost]
        public ActionResult AddQuestion(Question question)
        {
            db.Questions.Add(question);

            db.SaveChanges();

            ViewBag.QuestionnaireId = question.QuestionnaireId;

            return RedirectToAction("GetQuestions", "Question", new { id = question.QuestionnaireId});
        }

        [HttpPost]
        public ActionResult MyAction(string action, Question question)
        {
            if(action == "delete")
            {              
                return RedirectToAction("DeleteQuestion", "Question", new { id = question.Id });
            }
            else
            {
                int f = question.Id;
                return RedirectToAction("EditQuestionForm", "Question", new { id = question.Id });
            }
        }

        public ActionResult DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);

            int? QuestionnaireId = question.QuestionnaireId;

            List<Testing> t = db.Testings.Where(te => te.QuestionId == question.Id).ToList();

            List<Variant> v = db.Variants.Where(ve => ve.QuestionId == question.Id).ToList();

            Question q = db.Questions.Where(e => e.Id == id).FirstOrDefault();

            foreach(var el in t)
            {
                db.Testings.Remove(el);
                db.SaveChanges();
            }

            foreach(var el in v)
            {
                db.Variants.Remove(el);
                db.SaveChanges();
            }

            db.Questions.Remove(q);

            db.SaveChanges();

            return RedirectToAction("GetQuestions", "Question", new { id = QuestionnaireId });
            //if(t != null || v != null)
            //{
            //    return RedirectToAction("GetQuestions", "Question", new { id = QuestionnaireId });
            //}
            //else
            //{
            //    db.Questions.Remove(question);

            //    db.SaveChanges();

            //    return RedirectToAction("GetQuestions", "Question", new { id = QuestionnaireId });
            //}
        }

        public ActionResult EditQuestionForm(int id)
        {
            Question ques = db.Questions.Find(id);

            return View(ques);
        }

        [HttpPost]
        public ActionResult EditQuestion(Question q)
        {
            db.Entry(q).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("GetQuestions", "Question", new { id = q.QuestionnaireId });
        }
        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}