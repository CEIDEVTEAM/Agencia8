using BusinessLogic.DTOs.DecisionParam;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class DecisionParamMapper
    {
        public DecisionParam MapToEntity(DecisionParamCreationDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new DecisionParam()
            {
                Name = dto.Name,
                Description = dto.Description,
                Value = dto.Value,
                ActiveFlag = dto.ActiveFlag,
            };
        }
        public DecisionParamDTO MapToObject(DecisionParam entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new DecisionParamDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Value = entity.Value,
                ActiveFlag = entity.ActiveFlag,
            };
        }

        public DecisionParam MapToEditEntity(DecisionParamCreationDTO dto, DecisionParam entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Value = dto.Value;
            entity.ActiveFlag = dto.ActiveFlag;

            return entity;
        }
    }
}
