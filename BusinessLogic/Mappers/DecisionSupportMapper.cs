using BusinessLogic.DTOs.Candidate;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class DecisionSupportMapper
    {
        public DecisionSupport MapToEntity(DecisionSupportDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new DecisionSupport()
            {
                Description = dto.Description,
                RecomendedDecision = dto.RecomendedDecision,
            };
        }

        public DecisionSupportDTO MapToObject(DecisionSupport entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new DecisionSupportDTO()
            {
                Description = entity.Description,
                RecomendedDecision = entity.RecomendedDecision,
            };
        }
    }
}
