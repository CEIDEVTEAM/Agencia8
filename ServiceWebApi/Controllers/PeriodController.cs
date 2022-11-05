using BusinessLogic.Controllers;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.Period;
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
    [Route("api/period")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
    public class PeriodController : ControllerBase
    {
        public const string _application = "PERIOD";
        private readonly IConfiguration _configuration;
        private readonly PeriodMapper _mapper;

        public PeriodController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mapper = new PeriodMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<PeriodDTO>>> PeriodList([FromQuery] PaginationDTO dto)
        {
            try
            {
                using (var uow = new UnitOfWork(this._configuration, _application))
                {
                    var queryable = uow.PeriodRepository.GetPeriods();

                    await HttpContext.InsertHeaderPaginationParams(queryable);
                    var periods = await queryable.OrderBy(x => x.Id).Paginate(dto).ToListAsync();
                    return _mapper.MapToObject(periods);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }


        [HttpPost("addPeriod")]
        public async Task<ActionResult<GenericResponse>> AddPeriod([FromBody] PeriodDTO dto)
        {
            try
            {
                PeriodLogicController lg = new PeriodLogicController(_configuration, _application);
                return await lg.AddPeriod(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

    }
}
