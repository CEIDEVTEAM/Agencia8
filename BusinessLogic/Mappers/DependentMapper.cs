using BusinessLogic.DTOs.Dependent;
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

    }
}
