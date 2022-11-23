using AutoMapper;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using CommonSolution.Encrypt;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;

namespace BusinessLogic.Controllers
{
    public class UserLogicController
    {
        private IConfiguration _configuration;
        private string _application;
        private readonly IMapper _mapper;

        public UserLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
            this._mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<User, UserCreationDTO>().ReverseMap()));
        }

        public async Task<GenericResponse> AddUser(UserCreationDTO dto)
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
                        dto.Password = Encrypt.GetSHA256(dto.Password);
                        uow.UserRepository.AddUser(dto);

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

        public async Task<GenericResponse> EditUser(UserCreationDTO dto)
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
                        uow.UserRepository.UpdateUser(dto);

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

        public async Task<GenericResponse> DeleteUser(int userId)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    if (uow.UserRepository.ExistUsuarioById(userId))
                    {
                        uow.UserRepository.DeleteUser(userId);

                        uow.SaveChanges();
                        uow.Commit();
                        successful = true;
                    }
                    else
                        errors.Add("El usuario no existe.");
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

        public List<string> Validations(UserCreationDTO user, UnitOfWork uow, bool isAdd = false)
        {
            List<string> colerrors = new List<string>();

            if (!isAdd && !uow.UserRepository.ExistUsuarioById(user.Id))
                colerrors.Add("El usuario no existe.");

            if (isAdd && uow.UserRepository.ExistUsuarioByUserName(user.UserName))
                colerrors.Add("El nombre de usuario ya está registrado.");

            if (!uow.UserRepository.ExistUserRole(user.IdRole ?? -1))
                colerrors.Add("Debe seleccionar un rol de usuario válido.");

            return colerrors;
        }

        public UserCreationDTO GetUserById(int id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                return uow.UserRepository.GetUserById(id);
            }
        }

        #endregion

    }
}
