using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.Raspadita;
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
    [Route("api/raspadita")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class RaspaditaController : ControllerBase
    {
        public const string _application = "RASPADITA";
        private readonly IConfiguration _configuration;
        private readonly RaspaditaMapper _mapper;

        public RaspaditaController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new RaspaditaMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<RaspaditaDTO>>> ConceptList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.RaspaditaRepository.GetRaspaditas(uow);

                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var raspaditas = await queryable.OrderBy(x => x.Id).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(raspaditas);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RaspaditaDTO>> Get(int id)
        {
            try
            {
                RaspaditaLogicController lg = new RaspaditaLogicController(_configuration, _application);
                return lg.GetRaspaditaById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenericResponse>> EditRaspadita([FromBody] RaspaditaDTO dto)
        {
            try
            {
                RaspaditaLogicController lg = new RaspaditaLogicController(_configuration, _application);
                return await lg.EditRaspadita(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

    }
}
