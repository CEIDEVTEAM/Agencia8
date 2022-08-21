using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class LogMapper
    {

        public LtDependent MapToLogEntity(Dependent entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new LtDependent()
            {
                Number = entity.Number,
                Name = entity.Name,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                PersonalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                PersonalAddress = entity.PersonalAddress,
                Phone = entity.Phone,
                Condition = entity.Condition,
                PatentNamber = entity.PatentNamber,
                IdUser = entity.UpdUserId,
            };
        }

        public LtCandidate MapToLogEntity(Candidate entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new LtCandidate()
            {
                IdCandidate = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                PersonalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                PersonalAddress = entity.PersonalAddress,
                Phone = entity.Phone,
                Condition = entity.Condition,
                Status = entity.Status,
                Number = entity.Number,
                IdDecisionSupport = entity.IdDecisionSupport,
            };
        }

        public LtShopData MapToLogEntity(ShopData entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new LtShopData()
            {
                IdShopData = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                Address = entity.Address,
                Neighborhood = entity.Neighborhood,
                ShopType = entity.ShopType,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                NumberDependent = entity.NumberDependent,
                IdCandidate = entity.IdCandidate,
            };
        }


    }
}
