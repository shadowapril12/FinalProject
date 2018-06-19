using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FinalProject.Models;
using Newtonsoft.Json;

namespace FinalProject.Controllers
{
    public class TestingController : Controller
    {
        TestingContext db = new TestingContext();

        // GET: Testing
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StartTesting(int Id, int userId)
        {     
            User user = db.Users.Find(userId);

            Questionnaire testingQuestionnaire = db.Questionnaires.Find(Id);         

            int count = 0;

            return RedirectToAction("ChangeQuestion", "Testing", new { _userId = user.Id, _questionnaireId = testingQuestionnaire.Id, i = count });
        }

        public ActionResult ChangeQuestion(int _userId, int _questionnaireId, int i)
        {
            int numb = i;

            List<Question> questions = db.Questions.Where(q => q.QuestionnaireId == _questionnaireId).ToList();

            while(i < questions.Count)
            {
                Question question = questions[i];
                
                List<Variant> variants = db.Variants.Where(v => v.QuestionId == question.Id).ToList();

                string variantsSerialized = JsonConvert.SerializeObject(variants);

                return RedirectToAction("ShowTestingForm", "Testing", new { userId = _userId, questionnaireId = _questionnaireId,
                    questionId = question.Id, _variants = variantsSerialized, counter = i });
            }

            return RedirectToAction("ShowEndTesting", "Testing", new { questionnaireId = _questionnaireId });
        }

        public ActionResult ShowTestingForm(int userId, int questionnaireId, int questionId, string _variants, int counter )
        {
            User user = db.Users.Find(userId);

            Questionnaire testingQuestionnaire = db.Questionnaires.Find(questionnaireId);

            Question quest = db.Questions.Find(questionId);

            List<Variant> variants = JsonConvert.DeserializeObject<List<Variant>>(_variants);

            Testing test = new Testing();

            test.User = user;
            test.UserId = user.Id;

            test.Questionnaire = testingQuestionnaire;
            test.QuestionnaireId = testingQuestionnaire.Id;

            test.Question = quest;
            test.QuestionId = quest.Id;

            test.Numb = counter;

            ViewBag.QuestionFormulation = test.Question.Formulation;
            ViewBag.QuestionnaireName = test.Questionnaire.Name;
            ViewBag.Variants = variants;

            return View(test);
        }

        [HttpPost]
        public ActionResult AddTesting(Testing test)
        {
            int? vId = test.VariantId;
            db.Testings.Add(test);

            int counter = test.Numb;
            Questionnaire q = db.Questionnaires.Find(test.QuestionnaireId);

            int qId = q.Id;

            int? userId = test.UserId;

            db.SaveChanges();

            counter++;

            return RedirectToAction("ChangeQuestion", "Testing", new { _userId = userId, _questionnaireId = qId, i = counter });
        }
      

        public ActionResult ShowEndTesting(int questionnaireId)
        {
            ViewBag.Id = questionnaireId;
            return View();
        }

        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}