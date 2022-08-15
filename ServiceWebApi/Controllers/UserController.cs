using BusinessLogic.Controllers;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace ServiceWebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public const string _application = "USER";
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        [HttpGet("userList")]        
        public async Task<ActionResult<List<UserDTO>>> UsersList([FromQuery] PaginationDTO dto)
        {
            UsersManagmentLogicController lg = new UsersManagmentLogicController(_configuration, _application);
            var response =  await lg.GetUsers(dto);
            return response;
        }

        

        [HttpGet("usersTotalRecords")]
        public async Task<ActionResult<int>> UserUrlTotalRecords([FromQuery] PaginationDTO dto)
        {
            UsersManagmentLogicController lg = new UsersManagmentLogicController(_configuration, _application);
            var response = await lg.GetUsersTotalRecord(dto);
            return response;
        }
    }
}
