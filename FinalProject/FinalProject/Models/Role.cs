using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleType { get; set; }

        ICollection<User> Users { get; set; }

        public Role()
        {
            List<User> Users = new List<User>();
        }
    }
}