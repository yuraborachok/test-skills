namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface ISkillService
    {
        IEnumerable<SkillDto> GetAll();

        SkillDto GetById(int id);

        IServiceResult<SkillDto> Create(SkillDto resourceRoleDto);

        IServiceResult<SkillDto> Update(SkillDto resourceRoleDto);

        bool Delete(int id);
    }
}
