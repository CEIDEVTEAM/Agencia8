using AutoMapper;
using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Candidate;
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
    [Route("api/candidate")]
    [ApiController]    
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class CandidateController : ControllerBase
    {
        public const string _application = "CANDIDATE";
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
        private readonly CandidateMapper _mapper;

        public CandidateController(IConfiguration configuration)
        {
            this._configuration = configuration;
            //this._mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<Candidate, CandidateDTO>()));
            this._mapper = new CandidateMapper();

        }

        [HttpGet]
        public async Task<ActionResult<List<CandidateDTO>>> CandidateList([FromQuery] PaginationDTO dto)
        {
            using (var uow = new UnitOfWork(this._configuration, _application))
            {
                var queryable = uow.CandidateRepository.GetCandidates();

                await HttpContext.InsertHeaderPaginationParams(queryable);
                var candidates = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                return _mapper.MapToObject(candidates);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CandidateDTO>> Get(int id)
        {
            using (var uow = new UnitOfWork(this._configuration, _application))
            {
                var queryable = uow.CandidateRepository.GetCandidateCompleteDataById(id);

                return _mapper.MapToObject(queryable);
            }
        }

        [HttpPost("addCandidate")]
        public async Task<ActionResult<GenericResponse>> AddCandidate([FromBody] CandidateCreationFrontDTO dto)
        {
            try
            {
                CandidateLogicController lg = new CandidateLogicController(_configuration, _application);
                //EDU
                //FALTA OBTENER EL ID DEL USUARIO
                return await lg.AddCandidate(dto, 1);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse>> EditCandidate([FromBody] CandidateCreationFrontDTO dto)
        {
            try
            {
                CandidateLogicController lg = new CandidateLogicController(_configuration, _application);
                //EDU
                //FALTA OBTENER EL ID DEL USUARIO
                return await lg.EditCandidate(dto, 1);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
    }
}
