using AutoMapper;
using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Mappers;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
namespace ServiceWebApi.Controllers
{
    [Route("api/dependent")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class DependentController : ControllerBase
    {
        public const string _application = "DEPENDENT";
        private readonly IConfiguration _configuration;
        private readonly DependentMapper _mapper;

        public DependentController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet("dependentList")]
        public async Task<ActionResult<List<DependentDTO>>> DependentList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.DependentRepository.GetDependents();
                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var dependents = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(dependents);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DependentCreationFrontDTO>> Get(int id)
        {
            try
            {
                DependentLogicController lg = new DependentLogicController(_configuration, _application);
                return lg.GetDependentById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }


        [HttpPost("addDependent")]
        public async Task<ActionResult<GenericResponse>> AddDependent([FromBody] DependentCreationFrontDTO dto)
        {
            try
            {
                DependentLogicController lg = new DependentLogicController(_configuration, _application);
                var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                return await lg.AddDependent(dto, userName);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("editDependent")]
        public async Task<ActionResult<GenericResponse>> EditDependent([FromBody] DependentCreationFrontDTO dto)
        {
            try
            {
                DependentLogicController lg = new DependentLogicController(_configuration, _application);
                var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                return await lg.EditDependent(dto, userName);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpDelete("deleteDependent")]
        public async Task<ActionResult<GenericResponse>> DeleteDependent([FromQuery] int dependentId, DependentFactCreationFrontDTO dto)
        {
            try
            {
                DependentLogicController lg = new DependentLogicController(_configuration, _application);
                var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                return await lg.DeleteDependent(dependentId, dto, userName);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
    }
}
