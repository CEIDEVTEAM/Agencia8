using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Period;
using DataAccess.Models;

namespace BusinessLogic.Mappers
{
    public class PeriodMapper
    {

        public Period MapToEntity(PeriodDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new Period()
            {
                Description = dto.Description,
                ReferenceDate = dto.ReferenceDate,
                ActiveFlag = dto.ActiveFlag,
                AddRow = dto.AddRow,
                UpdRow = dto.UpdRow,
            };
        }

        public PeriodDTO MapToObject(Period entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new PeriodDTO()
            {
                Id = entity.Id,
                Description = entity.Description,
                ReferenceDate = entity.ReferenceDate,
                ActiveFlag = entity.ActiveFlag,
                AddRow = entity.AddRow,
                UpdRow = entity.UpdRow,
            };
        }

        public List<PeriodDTO> MapToObject(List<Period> colEntity)
        {
            List<PeriodDTO> colObject = new List<PeriodDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public Period MapToEditEntity(ConceptDTO dto, Period entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Description = entity.Description;
            entity.ReferenceDate = entity.ReferenceDate;
            entity.ActiveFlag = entity.ActiveFlag;
            entity.UpdRow = entity.UpdRow;

            return entity;
        }

    }
}
