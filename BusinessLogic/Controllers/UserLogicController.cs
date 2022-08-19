
using AutoMapper;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class UserLogicController : ControllerBase
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
                        var user = _mapper.Map<User>(dto);

                        user.ActiveFlag = "S";
                        uow.UserRepository.AddUser(user);

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
                        User user = uow.UserRepository.GetUserById(dto.Id);

                        if (user != null)
                        {
                            user.Address = dto.Address;
                            user.Name = dto.Name;
                            user.Phone = dto.Phone;
                            user.Email = dto.Email;
                            user.IdRole = dto.IdRole;

                            uow.UserRepository.UpdateUser(user);

                            uow.SaveChanges();
                            uow.Commit();
                            successful = true;
                        }
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
                        User user = uow.UserRepository.GetUserById(userId);

                        if (user != null)
                        {
                            user.ActiveFlag = "N";
                            uow.UserRepository.UpdateUser(user);

                            uow.SaveChanges();
                            uow.Commit();
                            successful = true;
                        }
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

        #endregion

    }
}
