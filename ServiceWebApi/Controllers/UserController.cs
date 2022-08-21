using AutoMapper;
using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
namespace ServiceWebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public const string _application = "USER";
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<User, UserDTO>()));

        }

        [HttpGet("userList")]
        public async Task<ActionResult<List<UserDTO>>> UsersList([FromQuery] PaginationDTO dto)
        {
            using (var uow = new UnitOfWork(this._configuration, _application))
            {
                var queryable = uow.UserRepository.GetUsers();

                await HttpContext.InsertHeaderPaginationParams(queryable);
                var users = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                return _mapper.Map<List<UserDTO>>(users);
            }

        }



        //[HttpGet("usersTotalRecords")]
        //public async Task<ActionResult<int>> UserUrlTotalRecords([FromQuery] PaginationDTO dto)
        //{
        //    UserLogicController lg = new UserLogicController(_configuration, _application);
        //    var response = await lg.GetUsersTotalRecord(dto);
        //    return response;
        //}

        [HttpPost("addUser")]
        public async Task<ActionResult<GenericResponse>> AddUser([FromBody] UserCreationDTO dto)
        {
            try
            {
                UserLogicController lg = new UserLogicController(_configuration, _application);
                return await lg.AddUser(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("editUser")]
        public async Task<ActionResult<GenericResponse>> EditUser([FromBody] UserCreationDTO dto)
        {
            try
            {
                UserLogicController lg = new UserLogicController(_configuration, _application);
                return await lg.EditUser(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("deleteUser")]
        public async Task<ActionResult<GenericResponse>> DeleteUser([FromQuery] int userId)
        {
            try
            {
                UserLogicController lg = new UserLogicController(_configuration, _application);
                return await lg.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
    }
}
