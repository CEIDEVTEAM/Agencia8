using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ServiceWebApi.Controllers
{
    [ApiController]
    [Route("api/Test")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private Agencia_8Context _context;

        public TestController(ILogger<WeatherForecastController> logger, Agencia_8Context context)
        {
            _logger = logger;
            _context = context;
        }

        

        [HttpGet]
        public List<User> Get()
        {

            return _context.Users.Select(s=>s).ToList();
        }
    }
}