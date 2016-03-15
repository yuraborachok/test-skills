namespace TC.SkillsDatabase.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using Core.Models.DbModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class SkillsDatabaseContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        private static readonly Dictionary<Type, PropertyInfo> Properties = new Dictionary<Type, PropertyInfo>();

        public SkillsDatabaseContext()
            : base("name=SkillsDb")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<ResourceRole> ResourceRoles { get; set; }

        public virtual DbSet<ResourceSkill> ResourceSkills { get; set; }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<SkillLevel> SkillLevels { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public DbSet<T> GetPropertyByType<T>() where T : class
        {
            PropertyInfo property;
            if (!Properties.TryGetValue(typeof(T), out property))
            {
                property = this.GetType()
                               .GetProperties()
                               .SingleOrDefault(p => p.PropertyType == typeof(DbSet<T>));

                if (property == null)
                {
                    property = this.GetType()
                                   .GetProperties()
                                   .SingleOrDefault(p => p.PropertyType == typeof(IDbSet<T>));

                    if (property == null)
                    {
                        return null;
                    }
                }

                Properties.Add(typeof(T), property);
            }

            return (DbSet<T>)property.GetValue(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Skills)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Resources)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resource>()
                .HasMany(e => e.ResourceSkills)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceRole>()
                .HasMany(e => e.Resources)
                .WithRequired(e => e.ResourceRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Skill>()
                .HasMany(e => e.ResourceSkills)
                .WithRequired(e => e.Skill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SkillLevel>()
                .HasMany(e => e.ResourceSkills)
                .WithRequired(e => e.SkillLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Resources)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomUserLogin>().HasKey<int>(l => l.UserId);
            modelBuilder.Entity<CustomRole>().HasKey<int>(r => r.Id);
            modelBuilder.Entity<CustomUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
