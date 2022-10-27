using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.ProjectionParam;
using BusinessLogic.DTOs.ShopData;
using CommonSolution.Constants;
using DataAccess.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class ProjectionParamMapper
    {

        public ProjectionParam MapToEntity(ProjectionParamDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new ProjectionParam()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                Usage = dto.Usage,
                ActualDefaultValue = dto.ActualDefaultValue,
                AddRow = dto.AddRow,
                UpdRow = dto.UpdRow
            };
        }

        public ProjectionParamDTO MapToObject(ProjectionParam entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new ProjectionParamDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                Usage = entity.Usage,
                ActualDefaultValue = entity.ActualDefaultValue,
                AddRow = entity.AddRow,
                UpdRow = entity.UpdRow
            };
        }

        public List<ProjectionParamDTO> MapToObject(List<ProjectionParam> colEntity)
        {
            List<ProjectionParamDTO> colObject = new List<ProjectionParamDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public ProjectionParam MapToEditEntity(ProjectionParamDTO dto, ProjectionParam entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Type = dto.Type;
            entity.Usage = dto.Usage;
            entity.ActualDefaultValue = dto.ActualDefaultValue;
            entity.UpdRow = dto.UpdRow;

            return entity;
        }

    }
}
