namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface IResourceRoleService
    {
        IEnumerable<ResourceRoleDto> GetAll();
         
        ResourceRoleDto GetById(int id);

        IServiceResult<ResourceRoleDto> Create(ResourceRoleDto resourceRoleDto);

        IServiceResult<ResourceRoleDto> Update(ResourceRoleDto resourceRoleDto);

        bool Delete(int id);
    }
}
