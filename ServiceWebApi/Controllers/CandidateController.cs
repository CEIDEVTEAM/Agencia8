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
        private readonly CandidateMapper _mapper;

        public CandidateController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new CandidateMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<CandidateDTO>>> CandidateList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.CandidateRepository.GetCandidates(dto.Search);

                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var candidates = await queryable.OrderBy(x => x.Name).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(candidates);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CandidateCreationFrontDTO>> Get(int id)
        {
            try
            {
                CandidateLogicController lg = new CandidateLogicController(_configuration, _application);
                return lg.GetUserById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("addCandidate")]
        public async Task<ActionResult<GenericResponse>> AddCandidate([FromBody] CandidateCreationFrontDTO dto)
        {
            try
            {
                CandidateLogicController lg = new CandidateLogicController(_configuration, _application);
                var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                return await lg.AddCandidate(dto, userName);
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
                var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                return await lg.EditCandidate(dto, userName);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }
    }
}
