namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface ISkillLevelService
    {
        IEnumerable<SkillLevelDto> GetAll();

        SkillLevelDto GetById(int id);

        IServiceResult<SkillLevelDto> Create(SkillLevelDto resourceRoleDto);

        IServiceResult<SkillLevelDto> Update(SkillLevelDto resourceRoleDto);

        bool Delete(int id);
    }
}
