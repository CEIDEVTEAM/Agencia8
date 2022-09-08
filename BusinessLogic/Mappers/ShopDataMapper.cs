using BusinessLogic.DTOs.ShopData;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class ShopDataMapper
    {
        public ShopData MapToEntity(ShopDataCreationDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new ShopData()
            {
                Id = dto.Id,
                Name = dto.Name,
                Phone = dto.Phone,
                Address = dto.Address,
                Neighborhood = dto.Neighborhood,
                ShopType = dto.ShopType,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IdDependent = dto.IdDependent,
                IdCandidate = dto.IdCandidate,
            };
        }

        public ShopDataCreationDTO MapToObject(ShopData entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new ShopDataCreationDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                Address = entity.Address,
                Neighborhood = entity.Neighborhood,
                ShopType = entity.ShopType,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                IdDependent = entity.IdDependent,
                IdCandidate = entity.IdCandidate,
            };
        }

        public ShopData MapToEditEntity(ShopDataCreationDTO dto, ShopData entity)
        {
            if (dto == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Name = dto.Name;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;
            entity.Neighborhood = dto.Neighborhood;
            entity.ShopType = dto.ShopType;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;

            if (dto.IdDependent != null)
                entity.IdDependent = dto.IdDependent; 
            
            if (dto.IdCandidate != null)
                entity.IdCandidate = dto.IdCandidate;

            return entity;
        }
    }
}
