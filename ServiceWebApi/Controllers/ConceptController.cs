using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Concept;
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
    [Route("api/concept")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class ConceptController : ControllerBase
    {
        public const string _application = "CONCEPT";
        private readonly IConfiguration _configuration;
        private readonly ConceptMapper _mapper;

        public ConceptController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new ConceptMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<ConceptDTO>>> ConceptList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.ConceptRepository.GetConcepts();

                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var concepts = await queryable.OrderBy(x => x.ParamId).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(concepts);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ConceptDTO>> Get(int id)
        {
            try
            {
                ConceptLogicController lg = new ConceptLogicController(_configuration, _application);
                return lg.GetConceptById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("addConcept")]
        public async Task<ActionResult<GenericResponse>> AddConcept([FromBody] ConceptDTO dto)
        {
            try
            {
                ConceptLogicController lg = new ConceptLogicController(_configuration, _application);
                return await lg.AddConcept(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse>> EditConcept([FromBody] ConceptDTO dto)
        {
            try
            {
                ConceptLogicController lg = new ConceptLogicController(_configuration, _application);
                return await lg.EditConcept(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

    }
}
