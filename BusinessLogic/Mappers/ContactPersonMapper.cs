using BusinessLogic.DTOs.ContactPerson;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class ContactPersonMapper
    {
        public ContactPerson MapToEntity(ContactPersonCreationDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new ContactPerson()
            {
                IdCandidate = dto.IdCandidate,
                IdDependent = dto.IdDependent,
                Name = dto.Name,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Bond = dto.Bond,
            };
        }

        public ContactPersonCreationDTO MapToObject(ContactPerson entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new ContactPersonCreationDTO()
            {
                IdCandidate = entity.IdCandidate,
                IdDependent = entity.IdDependent,
                Name = entity.Name,
                LastName = entity.LastName,
                Phone = entity.Phone,
                Bond = entity.Bond,
            };
        }

        public ContactPerson MapToEditEntity(ContactPersonCreationDTO dto, ContactPerson entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Name = dto.Name;
            entity.LastName = dto.LastName;
            entity.Phone = dto.Phone;
            entity.Bond = dto.Bond;

            return entity;
        }
    }
}
