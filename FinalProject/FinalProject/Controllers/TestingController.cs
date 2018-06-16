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

        [HttpGet]
        public ActionResult Analize(int questionnaireId)
        {
            List<Testing> tests = db.Testings.Where(t => t.QuestionnaireId == questionnaireId).ToList();
            List<Question> questions = db.Questions.Where(q => q.QuestionnaireId == questionnaireId).ToList();

            List<Counter> counter = new List<Counter>();


            foreach(var question in questions)
            {

                List<int?> searchId = new List<int?>();

                Counter c = new Counter();

                c.VariantsId = new List<int?>();

                foreach (var test in tests)
                {
                    if (test.QuestionId == question.Id)
                    {
                        searchId.Add(test.VariantId);
                    }
                }
                c.Quest = question;
                c.VariantsId = searchId;
                counter.Add(c);
            }

            foreach(var j in counter)
            {
                string g = j.Quest.Formulation;

                foreach(var t in j.VariantsId)
                {
                    int? f = t;
                }
            }
        
            List<Statistic> stat = new List<Statistic>();

            foreach (Counter finalCounter in counter)
            {
                Statistic s = new Statistic();

                s.QuestionName = finalCounter.Quest.Formulation;

                s.CountTime = new List<string>();

                foreach (var variant in finalCounter.VariantsId.Distinct())
                {
                    Variant var = db.Variants.Where(v => v.Id == variant).FirstOrDefault();

                    string g = $"{var.AnswerFormulation} - {finalCounter.VariantsId.Where(x => x == variant).Count()} человек(а)";

                    s.CountTime.Add(g);

                }

                stat.Add(s);
            }

            ViewBag.St = stat;

            return View(stat);
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