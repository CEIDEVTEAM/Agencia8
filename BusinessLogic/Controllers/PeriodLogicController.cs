using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using Microsoft.Extensions.Configuration;
using BusinessLogic.DTOs.Concept;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs.Period;
using BusinessLogic.DTOs.ProjectionParam;
using CommonSolution.Constants;
using BusinessLogic.DTOs.Raspadita;

namespace BusinessLogic.Controllers
{
    public class PeriodLogicController
    {
        private IConfiguration _configuration;
        private string _application;

        public PeriodLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
        }

        public async Task<GenericResponse> AddPeriod(PeriodDTO dto)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow, true); // EDU! => => Definir las validaciones de periodo

                    if (!errors.Any())
                    {
                        PeriodDTO activePeriod = uow.PeriodRepository.GetActivePeriodObject();

                        if (activePeriod != null)
                        {
                            activePeriod.ActiveFlag = "N";
                            uow.PeriodRepository.UpdatePeriod(activePeriod);
                        }

                        dto.ActiveFlag = "S";

                        uow.PeriodRepository.AddPeriod(uow, dto);

                        uow.SaveChanges();
                        uow.Commit();
                        successful = true;
                    }


                    AddConcepts(uow, dto.Id);
                    AddRaspaditas(uow, dto.Id);

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

        public void AddConcepts(UnitOfWork uow, decimal periodId)
        {
            List<ProjectionParamDTO> colParams = uow.ProjectionParamRepository.GetProjectionParamsDefoult();

            foreach (ProjectionParamDTO item in colParams)
            {
                ConceptDTO concept = new ConceptDTO();
                concept.ParamId = item.Id;
                concept.Value = item.ActualDefaultValue ?? 0;
                concept.PeriodId = periodId;

                uow.ConceptRepository.AddConcept(concept);
            }

            uow.SaveChanges();
        }

        public void AddRaspaditas(UnitOfWork uow, decimal periodId)
        {
            List<string> colAgencias = new List<string>();
            Type type = typeof(CAgencias);
            colAgencias = type.GetFields().Select(x => x.GetValue(null).ToString()).ToList();

            foreach (string agencia in colAgencias)
            {
                RaspaditaDTO raspadita = new RaspaditaDTO();
                raspadita.Agencia = agencia;
                raspadita.Aciertos = 0;
                raspadita.Apuestas = 0;
                raspadita.Utilidad = 0;
                raspadita.Partida = 0;
                raspadita.PeriodId = periodId;

                uow.RaspaditaRepository.AddRaspadita(raspadita);
            }

            uow.SaveChanges();
        }


        #region VALIDATIONS

        public List<string> Validations(PeriodDTO concept, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            //if (!isAdd && !uow.ConceptRepository.ExistConceptById(concept.Id))
            //    colerrors.Add($"El parámetro: {concept.Id} no existe.");

            //if (isAdd && uow.ConceptRepository.ExistConceptByParamAndPeriod(concept.ParamId, concept.PeriodId))
            //    colerrors.Add($"El parámetro ya está registrado para el periodo activo.");

            return colerrors;
        }

        #endregion

    }
}
