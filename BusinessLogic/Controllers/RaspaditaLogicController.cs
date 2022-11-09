using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using Microsoft.Extensions.Configuration;
using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Raspadita;

namespace BusinessLogic.Controllers
{
    public class RaspaditaLogicController
    {
        private IConfiguration _configuration;
        private string _application;

        public RaspaditaLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
        }

        public async Task<GenericResponse> AddRaspadita(RaspaditaDTO dto)
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
                        uow.RaspaditaRepository.AddRaspadita(dto);

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

        public async Task<GenericResponse> EditRaspadita(RaspaditaDTO dto)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow);

                    dto.Partida = (uow.ConceptRepository.GetProjectionParamValueByName("Tasa Gastos Administrativos Raspadita") * dto.Apuestas) / 100;

                    if (!errors.Any())
                    {
                        uow.RaspaditaRepository.UpdateRaspadita(dto);

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

        public RaspaditaDTO GetRaspaditaById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                return uow.RaspaditaRepository.GetRaspaditaById(id);
            }
        }


        #region VALIDATIONS

        public List<string> Validations(RaspaditaDTO raspadita, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.RaspaditaRepository.ExistRaspaditaById(raspadita.Id))
                colerrors.Add($"La raspadita: {raspadita.Id} no existe.");

            return colerrors;
        }

        #endregion

    }
}
