namespace TC.SkillsDatabase.BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Core.Models.DbModels;
    using Core.Models.DTO;
    using DAL;
    using Interfaces;

    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public IEnumerable<LocationDto> GetAll()
        {
            return Mapper.Map<List<LocationDto>>(this.locationRepository.GetAll());
        }

        public LocationDto GetById(int id)
        {
            return Mapper.Map<LocationDto>(this.locationRepository.GetAll().FirstOrDefault(s => s.Id == id));
        }

        public LocationDto Create(LocationDto locationDto)
        {
            var location = Mapper.Map<Location>(locationDto);
            this.locationRepository.Insert(location);
            var result = Mapper.Map<LocationDto>(location);

            return result;
        }

        public LocationDto Update(LocationDto locationDto)
        {
            var location = Mapper.Map<Location>(locationDto);
            this.locationRepository.Update(location);
            var result = Mapper.Map<LocationDto>(location);

            return result;
        }

        public void Delete(int id)
        {
            var category = this.locationRepository.GetAll().FirstOrDefault(s => s.Id == id);
            this.locationRepository.Delete(category);
        }
    }
}
