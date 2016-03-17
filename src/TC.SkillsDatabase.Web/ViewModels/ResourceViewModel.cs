namespace TC.SkillsDatabase.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;

    public class ResourceViewModel : ResourceDto
    {
        public IEnumerable<TeamDto> Teams { get; set; }

        public IEnumerable<LocationDto> Locations { get; set; }

        public IEnumerable<ResourceRoleDto> ResourceRoles { get; set; }
    }
}