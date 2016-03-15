namespace TC.SkillsDatabase.Core.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SkillLevel")]
    public partial class SkillLevel
    {
        public SkillLevel()
        {
            this.ResourceSkills = new HashSet<ResourceSkill>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public bool IsForLanguageSkill { get; set; }

        public virtual ICollection<ResourceSkill> ResourceSkills { get; set; }
    }
}
