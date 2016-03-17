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

            this.SaveChanges(context);

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

            this.SaveChanges(context);

            userAdmin.Roles.Add(new CustomUserRole() { RoleId = roleAdministrator.Id, UserId = userAdmin.Id });
            userAdmin.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = userAdmin.FirstName, UserId = userAdmin.Id });

            userManager.Roles.Add(new CustomUserRole() { RoleId = roleManager.Id, UserId = userManager.Id });
            userManager.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = userManager.FirstName, UserId = userManager.Id });

            userUser.Roles.Add(new CustomUserRole() { RoleId = roleUser.Id, UserId = userUser.Id });
            userUser.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = userUser.FirstName, UserId = userUser.Id });

            this.SaveChanges(context);

            var team1 = new Team { Name = "Team 1" };
            var team2 = new Team { Name = "Team 2" };
            var team3 = new Team { Name = "Team 3" };

            context.Teams.AddOrUpdate(o => o.Name, team1, team2, team3);
            this.SaveChanges(context);

            var location1 = new Location { Name = "Belgium" };
            var location2 = new Location { Name = "United Kingdom" };
            var location3 = new Location { Name = "Ukraine" };

            context.Locations.AddOrUpdate(o => o.Name, location1, location2, location3);
            this.SaveChanges(context);

            var resourceRole1 = new ResourceRole { Name = "Developer" };
            var resourceRole2 = new ResourceRole { Name = "QA" };
            var resourceRole3 = new ResourceRole { Name = "Manager" };

            context.ResourceRoles.AddOrUpdate(o => o.Name, resourceRole1, resourceRole2, resourceRole3);
            this.SaveChanges(context);

            var skillLevel0 = new SkillLevel { Name = "0 - None", Description = "None Skill", Value = 0, IsForLanguageSkill = false };
            var skillLevel1 = new SkillLevel { Name = "1 - Low", Description = "Low Skill", Value = 1, IsForLanguageSkill = false };
            var skillLevel2 = new SkillLevel { Name = "2 - Low", Description = "Low Skill", Value = 2, IsForLanguageSkill = false };
            var skillLevel3 = new SkillLevel { Name = "3 - Intermediate", Description = "Intermediate Skill", Value = 3, IsForLanguageSkill = false };
            var skillLevel4 = new SkillLevel { Name = "4 - Upper Intermediate", Description = "Upper Intermediate Skill", Value = 4, IsForLanguageSkill = false };
            var skillLevel5 = new SkillLevel { Name = "5 - Advanced", Description = "Advanced Skill", Value = 5, IsForLanguageSkill = false };

            var skillLevelLow = new SkillLevel { Name = "C - Low", Description = "Low Skill", Value = 1, IsForLanguageSkill = true };
            var skillLevelMedium = new SkillLevel { Name = "B - Medium", Description = "Medium Skill", Value = 2, IsForLanguageSkill = true };
            var skillLevelHigh = new SkillLevel { Name = "A - High", Description = "High Skill", Value = 3, IsForLanguageSkill = true };

            context.SkillLevels.AddOrUpdate(o => o.Name, skillLevel0, skillLevel1, skillLevel2, skillLevel3, skillLevel4, skillLevel5, skillLevelLow, skillLevelMedium, skillLevelHigh);
            this.SaveChanges(context);

            var category1 = new Category { Name = "Language Skills" };
            var category2 = new Category { Name = "Management Skills" };
            var category3 = new Category { Name = "Other Skills" };

            context.Categories.AddOrUpdate(o => o.Name, category1, category2, category3);
            this.SaveChanges(context);

            var skill1 = new Skill { Name = "Presentations", CategoryId = category1.Id, Description = "Presentations", IsLanguageSkill = true };
            var skill2 = new Skill { Name = "Consultations", CategoryId = category1.Id, Description = "Consultations", IsLanguageSkill = true };
            var skill3 = new Skill { Name = "Verbal Communication", CategoryId = category1.Id, Description = "Verbal Communication", IsLanguageSkill = true };

            var skill4 = new Skill { Name = "Managing People", CategoryId = category2.Id, Description = "Managing People", IsLanguageSkill = false };
            var skill5 = new Skill { Name = "Team Communication", CategoryId = category2.Id, Description = "Team Communication", IsLanguageSkill = false };
            var skill6 = new Skill { Name = "Reporting", CategoryId = category2.Id, Description = "Reporting", IsLanguageSkill = false };

            context.Skills.AddOrUpdate(o => o.Name, skill1, skill2, skill3, skill4, skill5, skill6);
            this.SaveChanges(context);


            var resource1 = new Resource
                            {
                                Name = "Bill Bills",
                                LocationId = location1.Id,
                                Manager = "Manager Bob",
                                ResourceRoleId = resourceRole1.Id,
                                TeamId = team1.Id
                            };
            var resource2 = new Resource
                            {
                                Name = "John Johnson",
                                LocationId = location2.Id,
                                Manager = "Manager Bob",
                                ResourceRoleId = resourceRole1.Id,
                                TeamId = team1.Id
                            };
            var resource3 = new Resource
                            {
                                Name = "Jack Daniels",
                                LocationId = location1.Id,
                                Manager = "Manager Bob",
                                ResourceRoleId = resourceRole2.Id,
                                TeamId = team1.Id
                            };
            var resource4 = new Resource
                            {
                                Name = "Jim Beam",
                                LocationId = location2.Id,
                                Manager = "Manager Sam",
                                ResourceRoleId = resourceRole1.Id,
                                TeamId = team2.Id
                            };
            var resource5 = new Resource
                            {
                                Name = "Johnny Walker",
                                LocationId = location2.Id,
                                Manager = "Manager Sam",
                                ResourceRoleId = resourceRole1.Id,
                                TeamId = team2.Id
                            };
            var resource6 = new Resource
                            {
                                Name = "John Travolta",
                                LocationId = location1.Id,
                                Manager = "Manager Sam",
                                ResourceRoleId = resourceRole2.Id,
                                TeamId = team2.Id
                            };
            context.Resources.AddOrUpdate(o => o.Name, resource1, resource2, resource3, resource4, resource5, resource6);
            this.SaveChanges(context);

            var resourceSkill1 = new ResourceSkill
            {
                ResourceId = resource1.Id,
                SkillId = skill1.Id,
                SkillLevelId = skillLevel1.Id,
                SkillLevelValue = skillLevel1.Value,
            };

            var resourceSkill2 = new ResourceSkill
            {
                ResourceId = resource1.Id,
                SkillId = skill2.Id,
                SkillLevelId = skillLevel2.Id,
                SkillLevelValue = skillLevel2.Value,
            };

            var resourceSkill3 = new ResourceSkill
            {
                ResourceId = resource1.Id,
                SkillId = skill3.Id,
                SkillLevelId = skillLevel3.Id,
                SkillLevelValue = skillLevel3.Value,
            };

            context.ResourceSkills.AddOrUpdate(o => new { o.Skill, o.Resource }, resourceSkill1, resourceSkill2, resourceSkill3);
            this.SaveChanges(context);
        }

        private void SaveChanges(DbContext context)
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
