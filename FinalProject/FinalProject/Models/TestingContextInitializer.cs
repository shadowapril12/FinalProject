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
            User user1 = new User() { Name = "Вася", Role = "Админ" };

            db.Users.Add(user1);

            db.SaveChanges();
        }
    }
}