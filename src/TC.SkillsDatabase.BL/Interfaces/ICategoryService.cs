namespace TC.SkillsDatabase.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Models.DTO;
    using Core.Results;

    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();

        CategoryDto GetById(int id);

        IServiceResult<CategoryDto> Create(CategoryDto categoryDto);

        IServiceResult<CategoryDto> Update(CategoryDto categoryDto);

        bool Delete(int id);
    }
}
