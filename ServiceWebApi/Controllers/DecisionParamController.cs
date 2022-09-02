using AutoMapper;
using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.DecisionParam;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
namespace ServiceWebApi.Controllers
{
    [Route("api/decitionParam")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class DecisionParamController : ControllerBase
    {
        public const string _application = "DECISION_PARAMS";
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DecisionParamController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<DecisionParam, DecisionParamDTO>()));
        }

        [HttpGet]
        public async Task<ActionResult<List<DecisionParamDTO>>> DecisionParamList([FromQuery] PaginationDTO dto)
        {
            using (var uow = new UnitOfWork(this._configuration, _application))
            {
                var queryable = uow.DecisionParamRepository.GetDecisionParams(dto.Search);

                await HttpContext.InsertHeaderPaginationParams(queryable);
                var decisionParams = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                return _mapper.Map<List<DecisionParamDTO>>(decisionParams);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DecisionParamDTO>> Get(int id)
        {
            try
            {
                DecisionParamLogicController lg = new DecisionParamLogicController(_configuration, _application);
                return lg.GetParamById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse>> EditDecisionParam(int id, [FromBody] DecisionParamCreationDTO dto)
        {
            try
            {
                DecisionParamLogicController lg = new DecisionParamLogicController(_configuration, _application);
                return await lg.EditDecisionParam(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
    }
}
