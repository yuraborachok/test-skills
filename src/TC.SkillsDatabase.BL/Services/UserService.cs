namespace TC.SkillsDatabase.BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Core.Models.DbModels;
    using Core.Models.DTO;
    using Core.Results;
    using DAL;
    using Interfaces;

    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return Mapper.Map<List<UserDto>>(this.userRepository.GetAll().OrderBy(u => u.FirstName).ThenBy(u => u.LastName));
        }

        public UserDto GetById(int id)
        {
            return Mapper.Map<UserDto>(this.userRepository.GetAll().FirstOrDefault(s => s.Id == id));
        }

        public IServiceResult<UserDto> Create(UserDto userDto)
        {
            var result = this.Validate(userDto);
            if (result.IsValid)
            {
                var user = Mapper.Map<User>(userDto);
                this.userRepository.Insert(user);

                result.Entity = Mapper.Map<UserDto>(user);
            }

            return result;
        }

        public IServiceResult<UserDto> Update(UserDto userDto)
        {
            var result = this.Validate(userDto);
            if (result.IsValid)
            {
                var user = Mapper.Map<User>(userDto);
                this.userRepository.Update(user, u => u.FirstName, u => u.LastName);

                result.Entity = Mapper.Map<UserDto>(user);
            }

            return result;
        }

        public IServiceResult<UserDto> Delete(int id)
        {
            return new ServiceResult<UserDto>();
        }

        private ServiceResult<UserDto> Validate(UserDto userDto)
        {
            var result = new ServiceResult<UserDto>
            {
                Entity = userDto
            };

            return result;
        }
    }
}
