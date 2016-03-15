namespace TC.SkillsDatabase.Core.Models.DbModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ResourceSkill")]
    public partial class ResourceSkill
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }

        public int SkillId { get; set; }

        public int SkillLevelId { get; set; }

        public int SkillLevelValue { get; set; }

        public virtual Resource Resource { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual SkillLevel SkillLevel { get; set; }
    }
}
