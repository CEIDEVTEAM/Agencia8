using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Controllers
{
    public class DependentLogicController
    {
        private IConfiguration _configuration;
        private string _application;
        private DependentMapper _mapper;

        public DependentLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
            this._mapper = new DependentMapper();
        }

        public async Task<GenericResponse> AddDependent(DependentCreationFrontDTO frontDto, int userId)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            DependentCreationDTO dto = _mapper.MapToObject(frontDto);

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow, true);

                    if (!errors.Any())
                    {
                        decimal idDependent = uow.DependentRepository.AddDependent(dto, uow, userId);

                        if (dto.ShopData != null)
                        {
                            dto.ShopData.NumberDependent = idDependent;
                            uow.ShopDataRepository.AddShopData(dto.ShopData, uow, userId);
                        }

                        dto.ContactPerson.IdDependent = idDependent; 
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

        public async Task<GenericResponse> EditDependent(DependentCreationFrontDTO frontDto, decimal userId)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            DependentCreationDTO dto = _mapper.MapToObject(frontDto);

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    errors = Validations(dto, uow);

                    if (!errors.Any())
                    {
                        uow.DependentRepository.UpdateDependent(dto, uow, userId);

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

        public async Task<GenericResponse> DeleteDependent(decimal number, decimal userId)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    if (uow.DependentRepository.ExistDependentByNumber(number))
                    {
                        uow.DependentRepository.DeleteDependent(number, uow, userId);

                        if (uow.ShopDataRepository.ExistShopDataByNumber(number))
                            uow.ShopDataRepository.DeleteShopData(number, uow, userId);

                        if (uow.ContactPersonRepository.ExistContactPersonByNumber(number))
                            uow.ContactPersonRepository.DeleteContactPerson(number);

                        uow.SaveChanges();
                        uow.Commit();
                        successful = true;
                    }
                    else
                        errors.Add($"Sub agente o corredor no existe.");
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

        public List<string> Validations(DependentCreationDTO dependent, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.DependentRepository.ExistDependentByNumber(dependent.Number))
                colerrors.Add($"El {dependent.Condition} no existe.");

            if (isAdd && uow.DependentRepository.ExistDependentByNumber(dependent.Number))
                colerrors.Add($"El {dependent.Condition} ya está registrado.");

            return colerrors;
        }

        #endregion

    }
}
