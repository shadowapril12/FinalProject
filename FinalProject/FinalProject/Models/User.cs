using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public ICollection<Testing> Testings { get; set; }

        public User()
        {
            Testings = new List<Testing>();
        }
    }
}