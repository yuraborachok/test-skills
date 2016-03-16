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

    public class ResourceRoleService : IResourceRoleService
    {
        private readonly IRepository<ResourceRole> resourceRoleRepository;

        public ResourceRoleService(IRepository<ResourceRole> resourceRoleRepository)
        {
            this.resourceRoleRepository = resourceRoleRepository;
        }

        public IEnumerable<ResourceRoleDto> GetAll()
        {
            return Mapper.Map<List<ResourceRoleDto>>(this.resourceRoleRepository.GetAll());
        }

        public ResourceRoleDto GetById(int id)
        {
            return Mapper.Map<ResourceRoleDto>(this.resourceRoleRepository.GetAll().FirstOrDefault(s => s.Id == id));
        }

        public IServiceResult<ResourceRoleDto> Create(ResourceRoleDto resourceRoleDto)
        {
            var result = this.Validate(resourceRoleDto);

            if (result.IsValid)
            {
                var resourceRole = Mapper.Map<ResourceRole>(resourceRoleDto);
                this.resourceRoleRepository.Insert(resourceRole);

                result.Entity = Mapper.Map<ResourceRoleDto>(resourceRole);
            }

            return result;
        }

        public IServiceResult<ResourceRoleDto> Update(ResourceRoleDto resourceRoleDto)
        {
            var result = this.Validate(resourceRoleDto);

            if (result.IsValid)
            {
                var resourceRole = Mapper.Map<ResourceRole>(resourceRoleDto);
                this.resourceRoleRepository.Update(resourceRole);

                result.Entity = Mapper.Map<ResourceRoleDto>(resourceRole);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var resourceRole = this.resourceRoleRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (resourceRole != null)
            {
                this.resourceRoleRepository.Delete(resourceRole);
            }

            return true;
        }

        private IServiceResult<ResourceRoleDto> Validate(ResourceRoleDto resourceRoleDto)
        {
            var result = new ServiceResult<ResourceRoleDto>
            {
                Entity = resourceRoleDto
            };

            // Validate ResourceRole Name
            var resourceRole = this.resourceRoleRepository.GetAll().FirstOrDefault(u => u.Name == resourceRoleDto.Name && resourceRoleDto.Id != u.Id);
            if (resourceRole != null)
            {
                result.Errors.Add(new NotificationMessage("ResourceRoleName", string.Format(Resources.DublicateResourceRoleName, resourceRoleDto.Name)));
            }

            return result;
        }
    }
}
