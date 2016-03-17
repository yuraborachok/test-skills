namespace TC.SkillsDatabase.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;

    public class ResourceSearchViewModel
    {
        public string ResourceSearchText { get; set; }

        public List<ResourceDto> Resources { get; set; }
    }
}