namespace TC.SkillsDatabase.Core.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DbModels;

    public class ResourceDto
    {
        public int Id { get; set; }

        [StringLength(127)]
        public string Name { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string LocationName { get; set; }

        public string ResourceRoleName { get; set; }

        [Required]
        public int LocationId { get; set; }
        
        [Required]
        public int ResourceRoleId { get; set; }

        [StringLength(127)]
        public string Manager { get; set; }
    }
}
