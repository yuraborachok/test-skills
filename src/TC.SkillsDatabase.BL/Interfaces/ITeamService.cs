namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface ITeamService
    {
        IEnumerable<TeamDto> GetAll();
         
        TeamDto GetById(int id);

        IServiceResult<TeamDto> Create(TeamDto teamDto);

        IServiceResult<TeamDto> Update(TeamDto teamDto);

        bool Delete(int id);
    }
}
