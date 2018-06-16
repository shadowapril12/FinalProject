using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using System.Data.Entity;

namespace FinalProject.Models
{
    public class Statistic
    {
        public string QuestionName { get; set; }

        public List<string> CountTime { get; set; }
    }
}