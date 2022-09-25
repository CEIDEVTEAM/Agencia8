using BusinessLogic.DTOs.Dependent;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class ExternalDependentMapper
    {
        public ExternalDependent MapToEntity(ExternalDependentDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new ExternalDependent()
            {
                Number = dto.Number,
                AgencyNumber = dto.Number,
                Name = dto.Name,
                LastName = dto.LastName,
                Neighborhood = dto.Neighborhood,
                Address = dto.Address,
                Condition = dto.Condition,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                AddRow = dto.AddRow,
                UpdRow = dto.UpdRow,
            };
        }


        public ExternalDependentDTO MapToObject(ExternalDependent ent)
        {
            if (ent == null)
                throw new Exception("No hay entidad para mapear");

            return new ExternalDependentDTO()
            {
                Number = ent.Number,
                AgencyNumber = ent.Number,
                Name = ent.Name,
                LastName = ent.LastName,
                Neighborhood = ent.Neighborhood,
                Address = ent.Address,
                Condition = ent.Condition,
                Latitude = ent.Latitude,
                Longitude = ent.Longitude,
                AddRow = ent.AddRow,
                UpdRow = ent.UpdRow,
            };
        }

        public List<ExternalDependentDTO> MapToObject(List<ExternalDependent> listEntity)
        {
            List<ExternalDependentDTO> listDTO = new List<ExternalDependentDTO>();
            listEntity.ForEach(x => { listDTO.Add(MapToObject(x)); });

            return listDTO;
        }

        public ExternalDependent MapToEditEntity(ExternalDependentDTO dto, ExternalDependent entity)
        {

            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Number = dto.Number;
            entity.AgencyNumber = dto.Number;
            entity.Name = dto.Name;
            entity.LastName = dto.LastName;
            entity.Neighborhood = dto.Neighborhood;
            entity.Address = dto.Address;
            entity.Condition = dto.Condition;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;
            entity.AddRow = dto.AddRow;
            entity.UpdRow = dto.UpdRow;


            return entity;

        }
    }
}
