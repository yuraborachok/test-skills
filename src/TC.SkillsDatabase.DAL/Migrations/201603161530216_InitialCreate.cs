namespace TC.SkillsDatabase.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 127),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 127),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(),
                        IsLanguageSkill = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ResourceSkill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        SkillLevelId = c.Int(nullable: false),
                        SkillLevelValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resource", t => t.ResourceId)
                .ForeignKey("dbo.SkillLevel", t => t.SkillLevelId)
                .ForeignKey("dbo.Skill", t => t.SkillId)
                .Index(t => t.ResourceId)
                .Index(t => t.SkillId)
                .Index(t => t.SkillLevelId);
            
            CreateTable(
                "dbo.Resource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 127),
                        TeamId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        ResourceRoleId = c.Int(nullable: false),
                        Manager = c.String(maxLength: 127),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .ForeignKey("dbo.ResourceRole", t => t.ResourceRoleId)
                .ForeignKey("dbo.Team", t => t.TeamId)
                .Index(t => t.TeamId)
                .Index(t => t.LocationId)
                .Index(t => t.ResourceRoleId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 127),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResourceRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 127),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 127),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 127),
                        Description = c.String(),
                        Value = c.Int(nullable: false),
                        IsForLanguageSkill = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomUserRole",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.CustomRole", t => t.CustomRole_Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CustomRole_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 256),
                        LastName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomUserLogin",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomUserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.CustomUserLogin", "User_Id", "dbo.User");
            DropForeignKey("dbo.CustomUserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.CustomUserRole", "CustomRole_Id", "dbo.CustomRole");
            DropForeignKey("dbo.Skill", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.ResourceSkill", "SkillId", "dbo.Skill");
            DropForeignKey("dbo.ResourceSkill", "SkillLevelId", "dbo.SkillLevel");
            DropForeignKey("dbo.Resource", "TeamId", "dbo.Team");
            DropForeignKey("dbo.ResourceSkill", "ResourceId", "dbo.Resource");
            DropForeignKey("dbo.Resource", "ResourceRoleId", "dbo.ResourceRole");
            DropForeignKey("dbo.Resource", "LocationId", "dbo.Location");
            DropIndex("dbo.CustomUserLogin", new[] { "User_Id" });
            DropIndex("dbo.CustomUserClaim", new[] { "UserId" });
            DropIndex("dbo.CustomUserRole", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRole", new[] { "UserId" });
            DropIndex("dbo.Resource", new[] { "ResourceRoleId" });
            DropIndex("dbo.Resource", new[] { "LocationId" });
            DropIndex("dbo.Resource", new[] { "TeamId" });
            DropIndex("dbo.ResourceSkill", new[] { "SkillLevelId" });
            DropIndex("dbo.ResourceSkill", new[] { "SkillId" });
            DropIndex("dbo.ResourceSkill", new[] { "ResourceId" });
            DropIndex("dbo.Skill", new[] { "CategoryId" });
            DropTable("dbo.CustomUserLogin");
            DropTable("dbo.CustomUserClaim");
            DropTable("dbo.User");
            DropTable("dbo.CustomUserRole");
            DropTable("dbo.CustomRole");
            DropTable("dbo.SkillLevel");
            DropTable("dbo.Team");
            DropTable("dbo.ResourceRole");
            DropTable("dbo.Location");
            DropTable("dbo.Resource");
            DropTable("dbo.ResourceSkill");
            DropTable("dbo.Skill");
            DropTable("dbo.Category");
        }
    }
}
