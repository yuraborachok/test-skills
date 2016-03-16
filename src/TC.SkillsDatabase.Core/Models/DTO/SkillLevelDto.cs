namespace TC.SkillsDatabase.Core.Models.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SkillLevelDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public bool IsForLanguageSkill { get; set; }
    }
}
