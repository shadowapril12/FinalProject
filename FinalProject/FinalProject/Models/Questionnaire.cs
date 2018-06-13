using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Testing> Testings { get; set; }

        public Questionnaire()
        {
            Questions = new List<Question>();

            Testings = new List<Testing>();
        }
    }
}