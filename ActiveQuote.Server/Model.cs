namespace ActiveQuote.Server
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BloggingContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }

        public string DbPath { get; }

        public BloggingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ActiveQuoteDB.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

    }

    public class Quote
    {
        public string InsurersName { get; set; } // The name of the insurance provider
        public int CostPerMonth { get; set; }    // The cost per month for the insurance
        public int LengthOfPolicy { get; set; }  // The length of the policy in years
    }
}
