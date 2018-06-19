using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class AnalizeController : Controller
    {
        TestingContext db = new TestingContext();

        [HttpGet]
        public ActionResult Analize(int questionnaireId)
        {
            List<Testing> tests = db.Testings.Where(t => t.QuestionnaireId == questionnaireId).ToList();
            List<Question> questions = db.Questions.Where(q => q.QuestionnaireId == questionnaireId).ToList();

            List<Counter> counter = new List<Counter>();


            foreach (var question in questions)
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

        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}