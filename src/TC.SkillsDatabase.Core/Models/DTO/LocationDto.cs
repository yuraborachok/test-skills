namespace TC.SkillsDatabase.Core.Models.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LocationDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }
    }
}
