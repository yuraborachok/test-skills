﻿namespace TC.SkillsDatabase.Core.Models.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Name { get; set; }
    }
}