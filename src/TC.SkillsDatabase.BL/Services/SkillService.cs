namespace TC.SkillsDatabase.BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using Core.Models.DbModels;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using DAL;
    using Interfaces;

    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;

        public SkillService(IRepository<Skill> skillRepository)
        {
            this.skillRepository = skillRepository;
        }

        public IEnumerable<SkillDto> GetAll()
        {
            return Mapper.Map<List<SkillDto>>(this.skillRepository.GetAll());
        }

        public SkillDto GetById(int id)
        {
            var skill = this.skillRepository.GetAll().Where(s => s.Id == id).Include(u => u.Category).FirstOrDefault();

            return skill == null
                       ? null
                       : skill.ToDto();
        }

        public IServiceResult<SkillDto> Create(SkillDto skillDto)
        {
            var result = this.Validate(skillDto);

            if (result.IsValid)
            {
                var skill = Mapper.Map<Skill>(skillDto);
                this.skillRepository.Insert(skill);

                result.Entity = Mapper.Map<SkillDto>(skill);
            }

            return result;
        }

        public IServiceResult<SkillDto> Update(SkillDto skillDto)
        {
            var result = this.Validate(skillDto);

            if (result.IsValid)
            {
                var skill = Mapper.Map<Skill>(skillDto);
                this.skillRepository.Update(skill);

                result.Entity = Mapper.Map<SkillDto>(skill);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var skill = this.skillRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (skill != null)
            {
                this.skillRepository.Delete(skill);
            }

            return true;
        }

        private IServiceResult<SkillDto> Validate(SkillDto skillDto)
        {
            var result = new ServiceResult<SkillDto>
            {
                Entity = skillDto
            };

            // Validate Skill Name
            var skill = this.skillRepository.GetAll().FirstOrDefault(u => u.Name == skillDto.Name && skillDto.Id != u.Id);
            if (skill != null)
            {
                result.Errors.Add(new NotificationMessage("SkillName", string.Format(Resources.DuplicateSkillName, skillDto.Name)));
            }

            return result;
        }
    }
}
