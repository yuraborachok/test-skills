namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        UserDto GetById(int id);

        IServiceResult<UserDto> Create(UserDto userDto);

        IServiceResult<UserDto> Update(UserDto userDto);

        IServiceResult<UserDto> Delete(int id);
    }
}
