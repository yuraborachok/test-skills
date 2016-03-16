namespace TC.SkillsDatabase.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;

    public class SkillViewModel : SkillDto
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}