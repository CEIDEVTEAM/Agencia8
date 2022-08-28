using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Controllers
{
    public class CandidateLogicController
    {
        private IConfiguration _configuration;
        private string _application;
        private CandidateMapper _mapper;

        public CandidateLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
            this._mapper = new CandidateMapper();
        }

        public async Task<GenericResponse> AddCandidate(CandidateCreationFrontDTO frontDto, int userId)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            CandidateCreationDTO dto = _mapper.MapToObject(frontDto);

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow, true);

                    if (!errors.Any())
                    {
                        uow.CandidateRepository.AddCandidate(dto, uow, userId);

                        if (dto.ShopData != null)
                        {
                            dto.ShopData.IdCandidate = dto.Id; //VER SI ES NECESARIO
                            uow.ShopDataRepository.AddShopData(dto.ShopData, uow, userId);
                        }

                        //EDU
                        //FALTA IMPLEMENTACION EN LA BASE
                        //dto.ContactPerson.IdCandidate = dto.Id; 
                        uow.ContactPersonRepository.AddContactPerson(dto.ContactPerson);

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

        public async Task<GenericResponse> EditCandidate(CandidateCreationDTO dto, decimal userId)
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
                        uow.CandidateRepository.UpdateCandidate(dto, uow, userId);

                        if (dto.ShopData != null)
                        {
                            dto.ShopData.IdCandidate = dto.Id;  //VER SI ES NECESARIO
                            uow.ShopDataRepository.UpdateShopData(dto.ShopData, uow, userId);
                        }

                        //EDU
                        //FALTA IMPLEMENTACION EN LA BASE
                        //dto.ContactPerson.IdCandidate = dto.Id; 
                        uow.ContactPersonRepository.UpdateContactPerson(dto.ContactPerson);

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

        #region VALIDATIONS

        public List<string> Validations(CandidateCreationDTO candidate, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.CandidateRepository.ExistCandidateById(candidate.Id))
                colerrors.Add($"El {candidate.Condition} no existe.");

            return colerrors;
        }

        #endregion

    }
}
