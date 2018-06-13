using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Formulation { get; set; }

        public int? QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public ICollection<Variant> Variants { get; set; }

        public ICollection<Testing> Testings { get; set; }

        public Question()
        {
            Variants = new List<Variant>();

            Testings = new List<Testing>();
        }
    }
}