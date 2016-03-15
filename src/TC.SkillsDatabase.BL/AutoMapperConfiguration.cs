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

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Location, LocationDto>().ReverseMap();
            });
        }
    }
}
