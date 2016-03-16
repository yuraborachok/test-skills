namespace TC.SkillsDatabase.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Security.Claims;
    using System.Text;
    using Core.Models.DbModels;

    internal sealed class Configuration : DbMigrationsConfiguration<SkillsDatabaseContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SkillsDatabaseContext context)
        {

            var roleAdministrator = new CustomRole("Admin");
            var roleManager = new CustomRole("Manager");
            var roleUser = new CustomRole("User");
            context.Roles.AddOrUpdate(r => new { r.Name }, roleAdministrator, roleManager, roleUser);
            SaveChanges(context);

            // Password: 1!Admin
            var userAdmin = new User
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                PasswordHash = "AESYKkF8s3NAgXiek9wk4t9lB00NmO42tYR5wlOr8CAVDeAmITX37yjJ9V3UZQumbA==",
                SecurityStamp = "5672c730-46f6-4e17-a0c4-1a653a041690",
                UserName = "admin@admin.com",
                EmailConfirmed = true
            };

            var userManager = new User
            {
                FirstName = "manager",
                LastName = "manager",
                Email = "manager@manager.com",
                PasswordHash = "AESYKkF8s3NAgXiek9wk4t9lB00NmO42tYR5wlOr8CAVDeAmITX37yjJ9V3UZQumbA==",
                SecurityStamp = "5672c730-46f6-4e17-a0c4-1a653a041690",
                UserName = "manager@manager.com",
                EmailConfirmed = true
            };

            var userUser = new User
            {
                FirstName = "user",
                LastName = "user",
                Email = "user@user.com",
                PasswordHash = "AESYKkF8s3NAgXiek9wk4t9lB00NmO42tYR5wlOr8CAVDeAmITX37yjJ9V3UZQumbA==",
                SecurityStamp = "5672c730-46f6-4e17-a0c4-1a653a041690",
                UserName = "user@user.com",
                EmailConfirmed = true
            };

            context.Users.AddOrUpdate(u => new { u.Email }, userAdmin, userManager, userUser);

            SaveChanges(context);

            userAdmin.Roles.Add(new CustomUserRole() { RoleId = roleAdministrator.Id, UserId = userAdmin.Id });
            userAdmin.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = userAdmin.FirstName, UserId = userAdmin.Id });

            userManager.Roles.Add(new CustomUserRole() { RoleId = roleManager.Id, UserId = userManager.Id });
            userManager.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = userManager.FirstName, UserId = userManager.Id });

            userUser.Roles.Add(new CustomUserRole() { RoleId = roleUser.Id, UserId = userUser.Id });
            userUser.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = userUser.FirstName, UserId = userUser.Id });

            SaveChanges(context);
        }

        private static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                // Add the original exception as the innerException
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
            }
        }
    }
}
