using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FinalProject.Models
{
    public class TestingContextInitializer : DropCreateDatabaseAlways<TestingContext>
    {
        protected override void Seed(TestingContext db)
        {
            Role role1 = new Role { RoleType = "admin" };
            Role role2 = new Role { RoleType = "user" };

            User user1 = new User();
            user1.Login = "Вася";
            user1.Role = role1;
            user1.RoleId = role1.Id;

            User user2 = new User();
            user2.Login = "Петя";
            user2.Role = role2;
            user2.RoleId = role2.Id;

            db.Roles.Add(role1);
            db.Roles.Add(role2);

            db.Users.Add(user1);
            db.Users.Add(user2);

            db.SaveChanges();
        }
    }
}