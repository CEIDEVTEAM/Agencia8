using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using BusinessLogic.Utils;
using CommonSolution.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json.Linq;
using static BusinessLogic.DTOs.Candidate.CandidateStepDataDTO;
using Elasticsearch.Net;
using DataAccess.Models;
using BusinessLogic.DTOs.ProjectionParam;

namespace BusinessLogic.Controllers
{
    public class ProjectionParamLogicController
    {
        private IConfiguration _configuration;
        private string _application;

        public ProjectionParamLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
        }

        public async Task<GenericResponse> AddProjectionParam(ProjectionParamDTO dto)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow, true);

                    if (!errors.Any())
                    {
                        uow.ProjectionParamRepository.AddProjectionParam(dto);

                        uow.SaveChanges();
                        uow.Commit();
                        successful = true;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add("Error al comunicarse con la base de datos");
                    uow.Rollback();
                }
            }

            return new GenericResponse()
            {
                Errors = errors,
                Successful = successful
            };

        }

        public async Task<GenericResponse> EditProjectionParam(ProjectionParamDTO dto)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow);

                    if (!errors.Any())
                    {
                        uow.ProjectionParamRepository.UpdateProjectionParam(dto);

                        uow.SaveChanges();
                        uow.Commit();
                        successful = true;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add("Error al comunicarse con la base de datos");
                    uow.Rollback();
                }
            }

            return new GenericResponse()
            {
                Errors = errors,
                Successful = successful
            };

        }

        public ProjectionParamDTO GetProjectionParamById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                return uow.ProjectionParamRepository.GetProjectionParamById(id);
            }
        }


        #region VALIDATIONS

        public List<string> Validations(ProjectionParamDTO projectionParam, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.ProjectionParamRepository.ExistProjectionParamById(projectionParam.Id))
                colerrors.Add($"El parámetro: {projectionParam.Name} no existe.");

            if (isAdd && uow.ProjectionParamRepository.ExistProjectionParamByName(projectionParam.Name))
                colerrors.Add($"El parámetro: {projectionParam.Name} ya está registrado.");

            return colerrors;
        }

        #endregion

    }
}
