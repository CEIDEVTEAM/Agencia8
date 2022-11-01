using AutoMapper;
using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.ProjectionParam;
using BusinessLogic.Mappers;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static BusinessLogic.DTOs.Candidate.CandidateStepDataDTO;

namespace ServiceWebApi.Controllers
{
    [Route("api/projectionParam")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class ProjectionParamController : ControllerBase
    {
        public const string _application = "PROJECTION PARAM";
        private readonly IConfiguration _configuration;
        private readonly ProjectionParamMapper _mapper;

        public ProjectionParamController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new ProjectionParamMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectionParamDTO>>> ProjectionParamList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.ProjectionParamRepository.GetProjectionParams();

                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var projectionParams = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(projectionParams);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProjectionParamDTO>> Get(int id)
        {
            try
            {
                ProjectionParamLogicController lg = new ProjectionParamLogicController(_configuration, _application);
                return lg.GetProjectionParamById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("addProjectionParam")]
        public async Task<ActionResult<GenericResponse>> AddProjectionParam([FromBody] ProjectionParamDTO dto)
        {
            try
            {
                ProjectionParamLogicController lg = new ProjectionParamLogicController(_configuration, _application);
                return await lg.AddProjectionParam(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse>> EditProjectionParam([FromBody] ProjectionParamDTO dto)
        {
            try
            {
                ProjectionParamLogicController lg = new ProjectionParamLogicController(_configuration, _application);
                return await lg.EditProjectionParam(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("projectionParam")] //PARA CARGAR EL SELECT
        public async Task<ActionResult<List<ProjectionParamDTO>>> GetProjectionParam()
        {
            try
            {
                ProjectionParamLogicController lg = new ProjectionParamLogicController(_configuration, _application);
                return lg.GetProjectionParams();
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

    }
}
