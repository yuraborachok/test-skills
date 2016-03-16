namespace TC.SkillsDatabase.BL
{
    using System;
    using AutoMapper;
    using Core.Models.DbModels;
    using Core.Models.DTO;

    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<CategoryDto, Category>();

            Mapper.CreateMap<ResourceRole, ResourceRoleDto>();
            Mapper.CreateMap<ResourceRoleDto, ResourceRole>();

            Mapper.CreateMap<Team, TeamDto>();
            Mapper.CreateMap<TeamDto, Team>();
        }
    }
}
