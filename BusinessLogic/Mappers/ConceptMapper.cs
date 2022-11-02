using BusinessLogic.DTOs.Concept;
using DataAccess.Models;

namespace BusinessLogic.Mappers
{
    public class ConceptMapper
    {

        public Concept MapToEntity(ConceptDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new Concept()
            {
                ParamId = dto.ParamId,
                Value = dto.Value,
                PeriodId = dto.PeriodId,
                AddRow = dto.AddRow ?? DateTime.Now,
                UpdRow = dto.UpdRow,
            };
        }

        public ConceptDTO MapToObject(Concept entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new ConceptDTO()
            {
                Id = entity.Id,
                ParamId = entity.ParamId,
                Value = entity.Value,
                PeriodId = entity.PeriodId,
                AddRow = entity.AddRow,
                UpdRow = entity.UpdRow,
            };
        }

        public List<ConceptDTO> MapToObject(List<Concept> colEntity)
        {
            List<ConceptDTO> colObject = new List<ConceptDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public ConceptCompleteDTO MapToObject(VConcept entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new ConceptCompleteDTO()
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                Type = entity.Type,
                ParamId = entity.ParamId,
                Value = entity.Value,
                PeriodId = entity.PeriodId,
                AddRow = entity.AddRow,
                UpdRow = entity.UpdRow,
            };
        }

        public List<ConceptCompleteDTO> MapToObject(List<VConcept> colEntity)
        {
            List<ConceptCompleteDTO> colObject = new List<ConceptCompleteDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public Concept MapToEditEntity(ConceptDTO dto, Concept entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.ParamId = dto.ParamId;
            entity.Value = dto.Value;
            entity.PeriodId = dto.PeriodId;
            entity.UpdRow = dto.UpdRow;

            return entity;
        }

    }
}
