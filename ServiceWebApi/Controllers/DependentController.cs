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
        private readonly ExternalDependentMapper _emapper;

        public DependentController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new DependentMapper();
            this._emapper = new ExternalDependentMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<DependentDTO>>> DependentList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.DependentRepository.GetDependents(dto.Search);
                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var dependents = await queryable.OrderBy(x => x.Number).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(dependents);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("externalDependent")]
        public async Task<ActionResult<List<ExternalDependentDTO>>> ExternalDependentList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.DependentRepository.GetExternalDependents(dto.Search);
                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var dependents = await queryable.OrderBy(x => x.Number).Paginate(dto).ToListAsync();
                    return _emapper.MapToObject(dependents);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
        [HttpGet("getExternalDependent/{id}")]
        public async Task<ActionResult<ExternalDependentDTO>> GetExternalDependent(string id)
        {
            try
            {
                ExternalDependentLogicController lg = new ExternalDependentLogicController(_configuration, _application);
                return lg.GetExternalDependentByKey(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPut("getExternalDependent/{id}")]
        public async Task<ActionResult<GenericResponse>> EditExternalDependent(string id,[FromBody] ExternalDependentDTO dto)
        {
            try
            {
                ExternalDependentLogicController lg = new ExternalDependentLogicController(_configuration, _application);
                return lg.EditExternalDependent(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("exCandidateDependent")]
        public async Task<ActionResult<List<ExCandidateDependetDTO>>> GetExCandidateDependent([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.DependentRepository.GetExCandidateDependents(dto.Search, dto.Filter);
                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var dependents = await queryable.OrderBy(x => x.PersonalDocument).Paginate(dto).ToListAsync();
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


        [HttpPost("processExternals")]
        public async Task<ActionResult<GenericResponse>> ProcessExternals()
        {
            try
            {
                ExternalDependentLogicController lg = new ExternalDependentLogicController(_configuration, _application);
                
                return await lg.ProccessExternalDependets();
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPut("{id:int}")]
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

        [HttpPost("deleteDependent/{id:int}")]
        public async Task<ActionResult<GenericResponse>> DeleteDependent(int id, [FromBody] DependentFactCreationFrontDTO dto)
        {
            try
            {
                DependentLogicController lg = new DependentLogicController(_configuration, _application);
                var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                return await lg.DeleteDependent(id, dto, userName);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

    }
}
