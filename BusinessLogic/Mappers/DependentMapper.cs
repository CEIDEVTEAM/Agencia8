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

        public DependentCreationDTO MapToObject(Dependent entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new DependentCreationDTO()
            {
                Number = entity.Number,
                Name = entity.Name,
                LastName = entity.Name,
                BirthDate = entity.BirthDate,
                PersonalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                PersonalAddress = entity.PersonalAddress,
                Phone = entity.Phone,
                Condition = entity.Condition,
                PatentNamber = entity.PatentNamber
            };
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
