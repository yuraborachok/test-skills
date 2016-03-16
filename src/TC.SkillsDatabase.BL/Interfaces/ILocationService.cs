namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface ILocationService
    {
        IEnumerable<LocationDto> GetAll();

        LocationDto GetById(int id);

        IServiceResult<LocationDto> Create(LocationDto locationDto);

        IServiceResult<LocationDto> Update(LocationDto locationDto);

        bool Delete(int id);
    }
}
