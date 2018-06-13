using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Testing
    {
        public int Id { get; set; }

        public int NumberOfAnswer { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public int? QuestionId { get; set; }

        public Question Question { get; set; }

        public int? QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public int? VariantId { get; set; }

        public Variant Variant { get; set; }
    }
}