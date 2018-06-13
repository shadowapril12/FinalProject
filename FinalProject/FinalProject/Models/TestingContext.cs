using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FinalProject.Models
{
    public class TestingContext : DbContext
    {
        static TestingContext()
        {
            Database.SetInitializer<TestingContext>(new TestingContextInitializer());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Variant> Variants { get; set; }

        public DbSet<Testing> Testings { get; set; }
    }
}