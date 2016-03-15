namespace TC.SkillsDatabase.Core.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            this.Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
