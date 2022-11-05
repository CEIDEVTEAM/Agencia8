using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Raspadita;
using DataAccess.Models;

namespace BusinessLogic.Mappers
{
    public class RaspaditaMapper
    {

        public Raspadita MapToEntity(RaspaditaDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new Raspadita()
            {
                Aciertos = dto.Aciertos,
                Agencia = dto.Agencia,
                Apuestas = dto.Apuestas,
                Partida = dto.Partida,
                PeriodId = dto.PeriodId,
                Utilidad = dto.Utilidad
            };
        }

        public RaspaditaDTO MapToObject(Raspadita entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new RaspaditaDTO()
            {
                Id = entity.Id,
                Aciertos = entity.Aciertos,
                Agencia = entity.Agencia,
                Apuestas = entity.Apuestas,
                Partida = entity.Partida,
                PeriodId = entity.PeriodId,
                Utilidad = entity.Utilidad,
                AddRow = entity.AddRow,
                UpdRow = entity.UpdRow,
            };
        }

        public List<RaspaditaDTO> MapToObject(List<Raspadita> colEntity)
        {
            List<RaspaditaDTO> colObject = new List<RaspaditaDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public Raspadita MapToEditEntity(RaspaditaDTO dto, Raspadita entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Aciertos = dto.Aciertos;
            entity.Agencia = dto.Agencia;
            entity.Apuestas = dto.Apuestas;
            entity.Partida = dto.Partida;
            entity.PeriodId = dto.PeriodId;
            entity.Utilidad = dto.Utilidad;
            entity.UpdRow = dto.UpdRow;

            return entity;
        }

    }
}
