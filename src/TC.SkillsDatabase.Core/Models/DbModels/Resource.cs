namespace TC.SkillsDatabase.Core.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Resource")]
    public partial class Resource
    {
        public Resource()
        {
            this.ResourceSkills = new HashSet<ResourceSkill>();
        }

        public int Id { get; set; }

        [StringLength(127)]
        public string Name { get; set; }

        public int TeamId { get; set; }

        public int LocationId { get; set; }

        public int ResourceRoleId { get; set; }

        [StringLength(127)]
        public string Manager { get; set; }

        public virtual Location Location { get; set; }

        public virtual ResourceRole ResourceRole { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<ResourceSkill> ResourceSkills { get; set; }
    }
}
