
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
    public  class UsersManagmentLogicController : ControllerBase
    {
        private IConfiguration _configuration;
        private string _application;
        private readonly IMapper _mapper;
        
        public UsersManagmentLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
            this._mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<User, UserDTO>()));
        }

        public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery] PaginationDTO dto)
        {
            using (var uow = new UnitOfWork(this._configuration, this._application))
            {
                var queryable = uow.UserRepository.GetUsers();
                //await HttpContext.InsertHeaderPaginationParams(queryable);
                var users = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                return _mapper.Map<List<UserDTO>>(users);
            }
        }
        public async Task<ActionResult<int>> GetUsersTotalRecord([FromQuery] PaginationDTO dto)
        {
            using (var uow = new UnitOfWork(this._configuration, this._application))
            {
                var queryable = uow.UserRepository.GetUsers();
                int qty = await queryable.CountAsync();

                return qty;
            }
        }
      
    }
}
