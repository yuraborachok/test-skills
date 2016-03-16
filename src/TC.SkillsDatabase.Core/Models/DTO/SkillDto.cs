namespace TC.SkillsDatabase.Core.Models.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SkillDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public bool IsLanguageSkill { get; set; }

        public string CategoryName { get; set; }
    }
}
