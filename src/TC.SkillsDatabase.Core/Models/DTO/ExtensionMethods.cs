namespace TC.SkillsDatabase.Core.Models.DTO
{
    using System;
    using AutoMapper;
    using DbModels;

    public static class ExtensionMethods
    {
        public static SkillDto ToDto(this Skill skill)
        {
            var skillDto = Mapper.Map<SkillDto>(skill);

            if (skill.Category != null)
            {
                skillDto.CategoryName = skill.Category.Name;
            }

            return skillDto;
        }

        public static ResourceDto ToDto(this Resource resource)
        {
            var resourceDto = Mapper.Map<ResourceDto>(resource);

            if (resource.Team != null)
            {
                resourceDto.TeamName = resource.Team.Name;
            }

            if (resource.ResourceRole != null)
            {
                resourceDto.ResourceRoleName = resource.ResourceRole.Name;
            }
            
            if (resource.Location != null)
            {
                resourceDto.LocationName = resource.Location.Name;
            }

            return resourceDto;
        }
    }
}
