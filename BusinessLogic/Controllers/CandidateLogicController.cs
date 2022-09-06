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

        public async Task<GenericResponse> AddCandidate(CandidateCreationFrontDTO frontDto, string userName)
        {
            List<string> errors = new List<string>();
            bool successful = false;
            decimal idCandidate = -1;
            decimal userId = -1;

            CandidateCreationDTO dto = _mapper.MapToObject(frontDto);

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                userId = uow.UserRepository.GetUserByUserName(userName).Id;

                try
                {
                    errors = Validations(dto, uow, true);

                    if (!errors.Any())
                    {
                        idCandidate = uow.CandidateRepository.AddCandidate(dto, uow, userId);

                        if (dto.ShopData != null)
                        {
                            dto.ShopData.IdCandidate = idCandidate;
                            uow.ShopDataRepository.AddShopData(dto.ShopData, uow, userId);
                            frontDto.id = idCandidate;
                            this.DecisionSupportResult(frontDto, uow, userId);
                        }

                        dto.ContactPerson.IdCandidate = idCandidate;
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

        public async Task<GenericResponse> EditCandidate(CandidateCreationFrontDTO frontDto, string userName)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            CandidateCreationDTO dto = _mapper.MapToObject(frontDto);

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                decimal userId = uow.UserRepository.GetUserByUserName(userName).Id;

                try
                {
                    errors = Validations(dto, uow);

                    if (!errors.Any())
                    {
                        uow.CandidateRepository.UpdateCandidate(dto, uow, userId);

                        if (dto.ShopData != null)
                        {
                            uow.ShopDataRepository.UpdateShopData(dto.ShopData, uow, userId);
                        }

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

        public ActionResult<CandidateStepDataDTO> GetStepsById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                CandidateStepDataDTO dto = new CandidateStepDataDTO();

                dto.colProcedureStep = uow.CandidateRepository.GetCandidateStepsById(id);
                dto.candidate = uow.CandidateRepository.GetCandidateCreationById(id);
                dto.colStepTypes = new List<stepType>();

                Dictionary<int, string> colStepType = new Dictionary<int, string>();

                if (!dto.colProcedureStep.Any())
                {
                    colStepType = Enum.GetValues(typeof(InitialStepTypes)).Cast<InitialStepTypes>().ToDictionary(t => (int)t, t => t.ToString());
                }
                else
                {
                    colStepType = Enum.GetValues(typeof(StepTypes)).Cast<StepTypes>().ToDictionary(t => (int)t, t => t.ToString());

                    if (dto.colProcedureStep.Count == colStepType.Count)
                    {
                        colStepType = Enum.GetValues(typeof(FinalStepTypes)).Cast<FinalStepTypes>().ToDictionary(t => (int)t, t => t.ToString());
                    }
                }

                foreach (var item in colStepType)
                {
                    if (!dto.colProcedureStep.Any(a => a.StepType == item.Value))
                    {
                        dto.colStepTypes.Add(new stepType { id = item.Value, name = item.Value });
                    }
                }

                return dto;
            }
        }

        public void DecisionSupportResult(CandidateCreationFrontDTO candidate, UnitOfWork uow, decimal userId)
        {
            DecisionSupportDTO dto = new DecisionSupportDTO();
            List<DistanceResponseDTO> getDistances = uow.DependentRepository.GetDependentsWithUbications();
            GeoCoordinate candidateLocation = new GeoCoordinate((double)candidate.latitude, (double)candidate.longitude);

            double minDistance = double.MaxValue;
            double minDistanceNeighborhood = uow.DecisionParamRepository.GetDecisionParamByNeighborhood(candidate.neighborhood);

            foreach (DistanceResponseDTO item in getDistances)
            {
                GeoCoordinate currentPoint = new GeoCoordinate(double.Parse(item.Latitude), double.Parse(item.Longitude));
                double distance = DistanceExtensions.GetDistance(candidateLocation, currentPoint);

                if (distance < minDistance)
                    minDistance = distance;
            }
            if (minDistance < minDistanceNeighborhood)
            {
                dto.RecomendedDecision = "No recomendando";
                dto.Description = $"La distancia minima para el barrio {candidate.neighborhood} es {minDistanceNeighborhood}," +
                    $" el comercio mas cercano se encuentra a {minDistance}";
            }
            else
            {
                dto.RecomendedDecision = "Se recomienda";
                dto.Description = $"La distancia minima para el barrio {candidate.neighborhood} es {minDistanceNeighborhood}," +
                    $" el comercio mas cercano se encuentra a {minDistance}";
            }

            dto.Date = DateTime.Now;

            decimal idDecision = uow.DecisionSupportRepository.AddDecision(dto, uow);

            CandidateCreationDTO upd = _mapper.MapToObject(candidate);
            upd.IdDecisionSupport = idDecision;
            uow.CandidateRepository.UpdateCandidate(upd, uow, userId);

        }

        public ActionResult<GenericResponse> AddCandidateStep(ProcedureStepDTO dto, string userName)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                decimal userId = uow.UserRepository.GetUserByUserName(userName).Id;

                try
                {
                    //errors = Validations(dto, uow, true);

                    //if (!errors.Any())
                    //{
                    dto.UpdUser = userId;
                    if (dto.StepType == "DECLINADO")
                    {
                        CandidateCreationDTO candidate = uow.CandidateRepository.GetCandidateById((decimal)dto.IdCandidate);
                        candidate.Status = CStatus.declined;

                        uow.CandidateRepository.UpdateCandidate(candidate, uow, userId);
                    }
                    else if (dto.StepType == "ACEPTADO")
                    {
                        CandidateCreationFrontDTO candidate = uow.CandidateRepository.GetCandidateCreationById((decimal)dto.IdCandidate);
                        DependentCreationDTO dependent = _mapper.MapToDependentObject(candidate);
                        DependentLogicController lgDep = new DependentLogicController(_configuration, _application);

                        errors = lgDep.AddDependent(candidate, uow, userId);

                        CandidateCreationDTO candiateUpd = uow.CandidateRepository.GetCandidateById((decimal)dto.IdCandidate);
                        candiateUpd.Status = CStatus.accepted;
                        uow.CandidateRepository.UpdateCandidate(candiateUpd, uow, userId);
                    }

                    uow.CandidateRepository.AddCandidateStep(dto);

                    if (!errors.Any())
                    {
                        uow.SaveChanges();
                        uow.Commit();
                        successful = true;
                    }
                    else
                    {
                        throw new Exception("");
                    }
                    //}
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

        public CandidateCreationFrontDTO GetCandidateById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                return uow.CandidateRepository.GetCandidateCreationById(id);
            }
        }

        public ActionResult<RecomendedDecisionDTO> RecomendedDecision(int id)
        {
            RecomendedDecisionDTO dto = new RecomendedDecisionDTO();

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                CandidateCreationFrontDTO candidate = uow.CandidateRepository.GetCandidateCreationById(id);
                DecisionSupportDTO recDec = uow.DecisionSupportRepository.GetRecomendedDecision(candidate.idDecisionSupport ?? -1);

                dto.Neighborhood = candidate.neighborhood;
                dto.RecomendedDecision = recDec.RecomendedDecision;
                dto.Description = recDec.Description;

                dto.NeighborhoodPotential = uow.DecisionParamRepository.GetPotentialByNeighborhood(candidate.neighborhood);
                dto.ShopCoordinates = uow.DependentRepository.GetDependentsWithUbications();
                dto.AgencyShops = uow.DependentRepository.GetShopCountByNeighborhood(candidate.neighborhood);
                dto.ExternalAgencyShops = uow.DependentRepository.GetExternalShopCountByNeighborhood(candidate.neighborhood);
            }

            return dto;
        }

        #region VALIDATIONS

        public List<string> Validations(CandidateCreationDTO candidate, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (isAdd && uow.CandidateRepository.ExistCandidateByDocument(candidate.PersonalDocument))
                colerrors.Add($"El documento de identidad ya está registrado.");

            if (!isAdd && !uow.CandidateRepository.ExistCandidateById(candidate.Id))
                colerrors.Add($"El {candidate.Condition} no existe.");

            return colerrors;
        }

        #endregion

    }
}
