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

        public IServiceResult<LocationDto> Create(LocationDto locationDto)
        {
            var result = this.Validate(locationDto);

            if (result.IsValid)
            {
                var location = Mapper.Map<Location>(locationDto);
                this.locationRepository.Insert(location);

                result.Entity = Mapper.Map<LocationDto>(location);
            }

            return result;
        }

        public IServiceResult<LocationDto> Update(LocationDto locationDto)
        {
            var result = this.Validate(locationDto);

            if (result.IsValid)
            {
                var location = Mapper.Map<Location>(locationDto);
                this.locationRepository.Update(location);

                result.Entity = Mapper.Map<LocationDto>(location);
            }

            return result;
        }

        public bool Delete(int id)
        {
            var location = this.locationRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (location != null)
            {
                this.locationRepository.Delete(location);
            }

            return true;
        }

        private IServiceResult<LocationDto> Validate(LocationDto locationDto)
        {
            var result = new ServiceResult<LocationDto>
            {
                Entity = locationDto
            };

            // Validate Location Name
            var location = this.locationRepository.GetAll().FirstOrDefault(u => u.Name == locationDto.Name && locationDto.Id != u.Id);
            if (location != null)
            {
                result.Errors.Add(new NotificationMessage("LocationName", string.Format(Resources.DublicateLocationName, locationDto.Name)));
            }

            return result;
        }
    }
}
