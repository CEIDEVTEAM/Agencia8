using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.ShopData;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class DependentMapper
    {

        public Dependent MapToEntity(DependentCreationDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new Dependent()
            {
                Number = dto.Number,
                Name = dto.Name,
                LastName = dto.Name,
                BirthDate = dto.BirthDate,
                PersonalDocument = dto.PersonalDocument,
                Gender = dto.Gender,
                MaritalStatus = dto.MaritalStatus,
                PersonalAddress = dto.PersonalAddress,
                Phone = dto.Phone,
                Condition = dto.Condition,
                PatentNamber = dto.PatentNamber
            };
        }

        public DependentDTO MapToObject(VDependent entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new DependentDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                PersonalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                Condition = entity.Condition,
                PersonalAddress = entity.PersonalAddress,
                Phone = entity.Phone,
                AddRow = entity.AddRow,
                Number = entity.Number,
                PatentNamber = entity.PatentNamber,
                IdShopData = entity.IdShopData,
                NameShopData = entity.NameShopData,
                PhoneShopData = entity.PhoneShopData,
                Address = entity.Address,
                Neighborhood = entity.Neighborhood,
                ShopType = entity.ShopType,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                NameContactPerson = entity.NameContactPerson,
                LastNameContactPerson = entity.LastNameContactPerson,
                PhoneContactPerson = entity.PhoneContactPerson,
                Bond = entity.Bond
            };
        }

        public DependentCreationFrontDTO MapToEditObject(VDependent entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new DependentCreationFrontDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate.ToString(),
                PersonalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                PersonalAddress = entity.PersonalAddress,
                Phone = entity.Phone,
                Number = entity.Number,
                PatentNamber = entity.PatentNamber,
                Condition = entity.Condition,
                IdShopData = entity.IdShopData,
                NameShopData = entity.NameShopData,
                PhoneShopData = entity.PhoneShopData,
                Address = entity.Address,
                Neighborhood = entity.Neighborhood,
                ShopType = entity.ShopType,
                Latitude = entity.Latitude == null ? null : double.Parse(entity.Latitude),
                Longitude = entity.Longitude == null ? null : double.Parse(entity.Longitude),
                NameContactPerson = entity.NameContactPerson,
                LastNameContactPerson = entity.LastNameContactPerson,
                PhoneContactPerson = entity.PhoneContactPerson,
                Bond = entity.Bond,
                IdContactPerson = (decimal)entity.IdContactPerson
            };
        }

        public List<DependentDTO> MapToObject(List<VDependent> colEntity)
        {
            List<DependentDTO> col = new List<DependentDTO>();

            colEntity.ForEach(x => col.Add(MapToObject(x)));

            return col;
        }

        public Dependent MapToEditEntity(DependentCreationDTO dto, Dependent entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Name = dto.Name;
            entity.LastName = dto.Name;
            entity.BirthDate = dto.BirthDate;
            entity.PersonalDocument = dto.PersonalDocument;
            entity.Gender = dto.Gender;
            entity.MaritalStatus = dto.MaritalStatus;
            entity.PersonalAddress = dto.PersonalAddress;
            entity.Phone = dto.Phone;
            entity.Condition = dto.Condition;
            entity.PatentNamber = dto.PatentNamber;

            return entity;
        }
        public DependentCreationDTO MapToObject(DependentCreationFrontDTO frontDTO)
        {
            if (frontDTO == null)
                throw new Exception("No hay objeto/entidad para mapear");

            return new DependentCreationDTO
            {
                Id = frontDTO.Id ?? 0,
                Name = frontDTO.Name,
                LastName = frontDTO.LastName,
                BirthDate = DateTime.Parse(frontDTO.BirthDate),
                PersonalDocument = frontDTO.PersonalDocument,
                Gender = frontDTO.Gender,
                MaritalStatus = frontDTO.MaritalStatus,
                PersonalAddress = frontDTO.PersonalAddress,
                Phone = frontDTO.Phone,
                Condition = frontDTO.Condition,
                Number = frontDTO.Number,

                ContactPerson = new ContactPersonCreationDTO
                {
                    Id = frontDTO.IdContactPerson,
                    Name = frontDTO.NameContactPerson,
                    LastName = frontDTO.LastNameContactPerson,
                    Phone = frontDTO.PhoneContactPerson,
                    Bond = frontDTO.Bond
                },

                ShopData = string.IsNullOrEmpty(frontDTO.NameShopData) ? null : new ShopDataCreationDTO
                {
                    Id = frontDTO.IdShopData ?? 0,
                    Name = frontDTO.NameShopData,
                    Phone = frontDTO.PhoneShopData,
                    Address = frontDTO.Address,
                    Neighborhood = frontDTO.Neighborhood,
                    ShopType = frontDTO.ShopType,
                    Latitude = frontDTO.Latitude.ToString(),
                    Longitude = frontDTO.Longitude.ToString(),
                }
            };
        }
    }
}
