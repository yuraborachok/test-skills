﻿namespace TC.SkillsDatabase.BL
{
    using System;
    using AutoMapper;
    using Core.Models.DbModels;
    using Core.Models.DTO;

    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Location, LocationDto>().ReverseMap();
                cfg.CreateMap<Category, CategoryDto>().ReverseMap();
                cfg.CreateMap<Team, TeamDto>().ReverseMap();
                cfg.CreateMap<ResourceRole, ResourceRoleDto>().ReverseMap();
            });
        }
    }
}
