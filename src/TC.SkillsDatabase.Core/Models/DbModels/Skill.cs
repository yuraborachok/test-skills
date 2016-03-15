namespace TC.SkillsDatabase.Core.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Skill")]
    public partial class Skill
    {
        public Skill()
        {
            this.ResourceSkills = new HashSet<ResourceSkill>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public bool IsLanguageSkill { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ResourceSkill> ResourceSkills { get; set; }
    }
}
