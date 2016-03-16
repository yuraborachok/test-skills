namespace TC.SkillsDatabase.Web.Models
{
    using System;
    using System.Data.Entity;
    using Core.Models.DbModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("SkillsDb")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<CustomUserRole>().ToTable("CustomUserRole");
            modelBuilder.Entity<CustomUserLogin>().ToTable("CustomUserLogin");
            modelBuilder.Entity<CustomRole>().ToTable("CustomRole");
            modelBuilder.Entity<CustomUserClaim>().ToTable("CustomUserClaim");

            modelBuilder.Entity<CustomUserLogin>().HasKey<int>(l => l.UserId);
            modelBuilder.Entity<CustomRole>().HasKey<int>(r => r.Id);
            modelBuilder.Entity<CustomUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }
    }
}