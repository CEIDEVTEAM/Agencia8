﻿using BusinessLogic.Controllers;
using BusinessLogic.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ServiceWebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public const string _application = "LOGIN";
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        [HttpPost("ValidateCredentials")]
        public async Task<ActionResult<AuthenticationResponse>> ValidateCredentials([FromBody] UserCredentials credentials)
        {
            try
            {
                UserLogicController lg = new UserLogicController(_configuration, _application);

                bool enabled = lg.ValidateCredentials(credentials);

                if (enabled)
                {
                    return await lg.BuildUserToken(credentials, _configuration["jwt_key"]);
                }
                else
                {
                    return BadRequest("Usuario y/o Contraseña Incorrectos, verifique.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("No es posible comunicarse con el proveedor.");
            }
        }

        [HttpPost("SetLogout")]
        public bool SetLogout([FromBody] AuthenticationResponse token)
        {
            try
            {
                UserLogicController lg = new UserLogicController(_configuration, _application);

                lg.SetLogout(token.Token);               

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}
