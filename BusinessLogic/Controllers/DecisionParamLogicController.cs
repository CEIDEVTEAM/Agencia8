using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.DecisionParam;
using BusinessLogic.DTOs.Generals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Controllers
{
    public class DecisionParamLogicController
    {
        private IConfiguration _configuration;
        private string _application;

        public DecisionParamLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
        }

        public async Task<GenericResponse> EditDecisionParam(DecisionParamCreationDTO dto)
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
                        uow.DecisionParamRepository.UpdateDecisionParam(dto, uow);

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

        public DecisionParamDTO GetParamById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                return uow.DecisionParamRepository.GetMappedDecisionParamById(id);
            }
        }

        #region VALIDATIONS

        public List<string> Validations(DecisionParamCreationDTO decisionParam, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.DecisionParamRepository.ExistDecisionParamById(decisionParam.Id))
                colerrors.Add($"El parámetro de decisión no existe.");

            return colerrors;
        }
       


        #endregion

    }
}
