using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FinalProject.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Question> Questions { get; set; }

        [JsonIgnore]
        public ICollection<Testing> Testings { get; set; }

        public Questionnaire()
        {
            Questions = new List<Question>();

            Testings = new List<Testing>();
        }
    }
}