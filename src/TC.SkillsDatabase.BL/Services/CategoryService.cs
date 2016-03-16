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

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return Mapper.Map<List<CategoryDto>>(this.categoryRepository.GetAll());
        }

        public CategoryDto GetById(int id)
        {
            return Mapper.Map<CategoryDto>(this.categoryRepository.GetAll().FirstOrDefault(s => s.Id == id));
        }

        public IServiceResult<CategoryDto> Create(CategoryDto categoryDto)
        {
            var result = this.Validate(categoryDto);

            if (result.IsValid)
            {
                var category = Mapper.Map<Category>(categoryDto);
                this.categoryRepository.Insert(category);

                result.Entity = Mapper.Map<CategoryDto>(category);
            }

            return result;
        }

        public IServiceResult<CategoryDto> Update(CategoryDto categoryDto)
        {
            var result = this.Validate(categoryDto);

            if (result.IsValid)
            {
                var category = Mapper.Map<Category>(categoryDto);
                this.categoryRepository.Update(category);

                result.Entity = Mapper.Map<CategoryDto>(category);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var category = this.categoryRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (category != null)
            {
                this.categoryRepository.Delete(category);
            }

            return true;
        }

        private IServiceResult<CategoryDto> Validate(CategoryDto categoryDto)
        {
            var result = new ServiceResult<CategoryDto>
            {
                Entity = categoryDto
            };

            // Validate Category Name
            var category = this.categoryRepository.GetAll().FirstOrDefault(u => u.Name == categoryDto.Name && categoryDto.Id != u.Id);
            if (category != null)
            {
                result.Errors.Add(new NotificationMessage("CategoryName", string.Format(Resources.DublicateCategoryName, categoryDto.Name)));
            }

            return result;
        }
    }
}
