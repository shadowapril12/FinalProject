using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using System.Data.Entity;

namespace FinalProject.Models
{
    public class Counter
    {
        public Question Quest { get; set; }

        public List<int?> VariantsId { get; set; }
    }
}