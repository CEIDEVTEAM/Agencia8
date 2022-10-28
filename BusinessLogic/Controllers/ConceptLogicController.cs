using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using Microsoft.Extensions.Configuration;
using BusinessLogic.DTOs.Concept;

namespace BusinessLogic.Controllers
{
    public class ConceptLogicController
    {
        private IConfiguration _configuration;
        private string _application;

        public ConceptLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
        }

        public async Task<GenericResponse> AddConcept(ConceptDTO dto)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    dto.PeriodId = uow.PeriodRepository.GetActivePeriod();

                    errors = Validations(dto, uow, true);

                    if (!errors.Any())
                    {
                        uow.ConceptRepository.AddConcept(dto);

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

        public async Task<GenericResponse> EditConcept(ConceptDTO dto)
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
                        uow.ConceptRepository.UpdateConcept(dto);

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

        public ConceptDTO GetConceptById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                return uow.ConceptRepository.GetConceptById(id);
            }
        }


        #region VALIDATIONS

        public List<string> Validations(ConceptDTO concept, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.ConceptRepository.ExistConceptById(concept.Id))
                colerrors.Add($"El parámetro: {concept.Id} no existe.");

            if (isAdd && uow.ConceptRepository.ExistConceptByParamAndPeriod(concept.ParamId, concept.PeriodId))
                colerrors.Add($"El parámetro ya está registrado para el periodo activo.");

            return colerrors;
        }

        #endregion

    }
}
