namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;

    public interface ILocationService
    {
        IEnumerable<LocationDto> GetAll();

        LocationDto GetById(int id);

        LocationDto Create(LocationDto locationDto);

        LocationDto Update(LocationDto locationDto);

        void Delete(int id);
    }
}
