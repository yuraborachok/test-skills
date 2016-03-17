namespace TC.SkillsDatabase.BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.Internal;
    using AutoMapper.QueryableExtensions;
    using Core.Models.DbModels;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using DAL;
    using Interfaces;

    public class ResourceService : IResourceService
    {
        private readonly IRepository<Resource> resourceRepository;

        public ResourceService(IRepository<Resource> resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        public IEnumerable<ResourceDto> GetAll()
        {
            return Mapper.Map<List<ResourceDto>>(this.resourceRepository.GetAll());
        }

        public IEnumerable<ResourceDto> GetAll(string resourceText)
        {
            var result = new List<ResourceDto>();
            IQueryable<Resource> resources;
            if (!string.IsNullOrEmpty(resourceText))
            {
                resources = this.resourceRepository.GetAll()
                                .Where(r => r.Team.Name.Contains(resourceText) || r.Name.Contains(resourceText) || r.Manager.Contains(resourceText) || r.ResourceRole.Name.Contains(resourceText))
                                .Include(r => r.Team).Include(r => r.ResourceRole).Include(r => r.Location);
            }
            else
            {
                resources = this.resourceRepository.GetAll().Include(r => r.Team).Include(r => r.ResourceRole).Include(r => r.Location);
            }

            foreach (var resource in resources)
            {
                result.Add(resource.ToDto());
            }

            return result;
        }

        public ResourceDto GetById(int id)
        {
            var resource = this.resourceRepository.GetAll().Where(r => r.Id == id).Include(r => r.Location).Include(r => r.ResourceRole).Include(r => r.Team).FirstOrDefault();

            return resource == null
                       ? null
                       : resource.ToDto();
        }

        public IServiceResult<ResourceDto> Create(ResourceDto resourceDto)
        {
            var result = this.Validate(resourceDto);

            if (result.IsValid)
            {
                var resource = Mapper.Map<Resource>(resourceDto);
                this.resourceRepository.Insert(resource);

                result.Entity = Mapper.Map<ResourceDto>(resource);
            }

            return result;
        }

        public IServiceResult<ResourceDto> Update(ResourceDto resourceDto)
        {
            var result = this.Validate(resourceDto);

            if (result.IsValid)
            {
                var resource = Mapper.Map<Resource>(resourceDto);
                this.resourceRepository.Update(resource);

                result.Entity = Mapper.Map<ResourceDto>(resource);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var resource = this.resourceRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (resource != null)
            {
                this.resourceRepository.Delete(resource);
            }

            return true;
        }

        private IServiceResult<ResourceDto> Validate(ResourceDto resourceDto)
        {
            var result = new ServiceResult<ResourceDto>
            {
                Entity = resourceDto
            };

            // Validate Resource Name
            var resource = this.resourceRepository.GetAll().FirstOrDefault(u => u.Name == resourceDto.Name && resourceDto.Id != u.Id);
            if (resource != null)
            {
                result.Errors.Add(new NotificationMessage("ResourceName", string.Format(Resources.DublicateResourceName, resourceDto.Name)));
            }

            return result;
        }
    }
}
