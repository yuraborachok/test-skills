namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface IResourceService
    {
        IEnumerable<ResourceDto> GetAll();

        ResourceDto GetById(int id);

        IServiceResult<ResourceDto> Create(ResourceDto resourceDto);

        IServiceResult<ResourceDto> Update(ResourceDto resourceDto);

        bool Delete(int id);
    }
}
