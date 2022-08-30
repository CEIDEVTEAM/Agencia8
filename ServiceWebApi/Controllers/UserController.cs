using AutoMapper;
using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Mappers;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
namespace ServiceWebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        public const string _application = "USER";
        private readonly IConfiguration _configuration;
        private readonly UserMapper _mapper;
        readonly ITokenAcquisition tokenAcquisition;

        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new UserMapper();
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationDTO dto)
        {
            using (var uow = new UnitOfWork(this._configuration, _application))
            {
                var queryable = uow.UserRepository.GetUsers(dto.Search);
                var value = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;
                UserDTO userDto = uow.UserRepository.GetUserByUserName(value);
                
                await HttpContext.InsertHeaderPaginationParams(queryable);
                var users = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                List<UserDTO> resp = _mapper.MapToObject(users);

                return resp;
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserCreationDTO>> Get(int id)
        {
            using (var uow = new UnitOfWork(this._configuration, _application))
            {
                var queryable = uow.UserRepository.GetUserById(id);

                return _mapper.MapToObject(queryable);
            }
        }


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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse>> EditUser(int id, [FromBody] UserCreationDTO dto)
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GenericResponse>> DeleteUser(int id)
        {
            try
            {
                UserLogicController lg = new UserLogicController(_configuration, _application);
                return await lg.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
    }
}
