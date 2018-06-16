using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FinalProject.Models
{
    public class Variant
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string AnswerFormulation { get; set; }

        public int? QuestionId { get; set; }

        public Question Question { get; set; }

        [JsonIgnore]
        public ICollection<Testing> Testings { get; set; }

        public Variant()
        {
            Testings = new List<Testing>();
        }
    }
}