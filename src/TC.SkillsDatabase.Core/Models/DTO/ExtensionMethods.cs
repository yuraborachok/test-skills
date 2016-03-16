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
    }
}
