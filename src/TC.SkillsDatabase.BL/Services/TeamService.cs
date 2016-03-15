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

    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public IEnumerable<TeamDto> GetAll()
        {
            return Mapper.Map<List<TeamDto>>(this.teamRepository.GetAll());
        }

        public TeamDto GetById(int id)
        {
            return Mapper.Map<TeamDto>(this.teamRepository.GetAll().FirstOrDefault(s => s.Id == id));
        }

        public IServiceResult<TeamDto> Create(TeamDto teamDto)
        {
            var result = this.Validate(teamDto);

            if (result.IsValid)
            {
                var team = Mapper.Map<Team>(teamDto);
                this.teamRepository.Insert(team);

                result.Entity = Mapper.Map<TeamDto>(team);
            }

            return result;
        }

        public IServiceResult<TeamDto> Update(TeamDto teamDto)
        {
            var result = this.Validate(teamDto);

            if (result.IsValid)
            {
                var team = Mapper.Map<Team>(teamDto);
                this.teamRepository.Update(team);

                result.Entity = Mapper.Map<TeamDto>(team);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var team = this.teamRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (team != null)
            {
                this.teamRepository.Delete(team);
            }

            return true;
        }

        private IServiceResult<TeamDto> Validate(TeamDto teamDto)
        {
            var result = new ServiceResult<TeamDto>
            {
                Entity = teamDto
            };

            // Validate Team Name
            var team = this.teamRepository.GetAll().FirstOrDefault(u => u.Name == teamDto.Name && teamDto.Id != u.Id);
            if (team != null)
            {
                result.Errors.Add(new NotificationMessage("TeamName", string.Format(Resources.DublicateTeamName, teamDto.Name)));
            }

            return result;
        }
    }
}
