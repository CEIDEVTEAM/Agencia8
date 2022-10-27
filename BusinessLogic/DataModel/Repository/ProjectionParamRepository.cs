using BusinessLogic.DTOs.ProjectionParam;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DataModel.Repository
{
    public class ProjectionParamRepository
    {
        private readonly Agencia_8Context _context;
        private readonly ProjectionParamMapper _mapper;

        public ProjectionParamRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new ProjectionParamMapper();
        }

        #region ADD

        public decimal AddProjectionParam(ProjectionParamDTO dto)
        {
            ProjectionParam entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.ProjectionParam.AddAsync(entity);

            return entity.Id;
        }

        #endregion

        #region UPDATE

        public void UpdateProjectionParam(ProjectionParamDTO projectionParam)
        {
            ProjectionParam entity = _context.ProjectionParam.FirstOrDefault(x => x.Id == projectionParam.Id);

            entity = this._mapper.MapToEditEntity(projectionParam, entity);
            entity.UpdRow = DateTime.Now;

            _context.ProjectionParam.Update(entity);
        }


        #endregion

        #region DELETE


        #endregion

        #region ANY
        public bool ExistProjectionParamById(decimal id)
        {
            return _context.ProjectionParam.Any(x => x.Id == id);
        }

        public bool ExistProjectionParamByName(string name)
        {
            return _context.ProjectionParam.Any(x => x.Name == name);
        }
        

        #endregion

        #region GET

        public IQueryable<ProjectionParam> GetProjectionParams()
        {
            return _context.ProjectionParam.AsNoTracking().AsQueryable();
        }

        public ProjectionParamDTO GetProjectionParamById(decimal id)
        {
            var x = _context.ProjectionParam.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToObject(x);
        }


        #endregion

    }
}
