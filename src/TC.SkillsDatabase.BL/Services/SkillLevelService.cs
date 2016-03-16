namespace TC.SkillsDatabase.BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Core.Models.DbModels;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using DAL;
    using Interfaces;

    public class SkillLevelService : ISkillLevelService
    {
        private readonly IRepository<SkillLevel> skillLevelRepository;

        public SkillLevelService(IRepository<SkillLevel> skillLevelRepository)
        {
            this.skillLevelRepository = skillLevelRepository;
        }

        public IEnumerable<SkillLevelDto> GetAll()
        {
            return Mapper.Map<List<SkillLevelDto>>(this.skillLevelRepository.GetAll());
        }

        public SkillLevelDto GetById(int id)
        {
            return Mapper.Map<SkillLevelDto>(this.skillLevelRepository.GetAll().FirstOrDefault(s => s.Id == id));
        }

        public IServiceResult<SkillLevelDto> Create(SkillLevelDto skillLevelDto)
        {
            var result = this.Validate(skillLevelDto);

            if (result.IsValid)
            {
                var skillLevel = Mapper.Map<SkillLevel>(skillLevelDto);
                this.skillLevelRepository.Insert(skillLevel);

                result.Entity = Mapper.Map<SkillLevelDto>(skillLevel);
            }

            return result;
        }

        public IServiceResult<SkillLevelDto> Update(SkillLevelDto skillLevelDto)
        {
            var result = this.Validate(skillLevelDto);

            if (result.IsValid)
            {
                var skillLevel = Mapper.Map<SkillLevel>(skillLevelDto);
                this.skillLevelRepository.Update(skillLevel);

                result.Entity = Mapper.Map<SkillLevelDto>(skillLevel);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var skillLevel = this.skillLevelRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (skillLevel != null)
            {
                this.skillLevelRepository.Delete(skillLevel);
            }

            return true;
        }

        private IServiceResult<SkillLevelDto> Validate(SkillLevelDto skillLevelDto)
        {
            var result = new ServiceResult<SkillLevelDto>
            {
                Entity = skillLevelDto
            };

            // Validate SkillLevel Name
            var skillLevel = this.skillLevelRepository.GetAll().FirstOrDefault(u => u.Name == skillLevelDto.Name && skillLevelDto.Id != u.Id);
            if (skillLevel != null)
            {
                result.Errors.Add(new NotificationMessage("SkillLevelName", string.Format(Resources.DuplicateSkillLevelName, skillLevelDto.Name)));
            }

            return result;
        }
    }
}
