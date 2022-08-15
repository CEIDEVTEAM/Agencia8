using AutoMapper;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class UserLogicController
    {
        private IConfiguration _configuration;
        private string _application;
        

        public UserLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;            
        }
        

        public bool ValidateCredentials(UserCredentials credentials)
        {
            using (var uow = new UnitOfWork(this._configuration, this._application))
            {
                return uow.UserRepository.ValidateCredentials(credentials);
            }
        }

        public async Task<AuthenticationResponse> BuildUserToken(UserCredentials credentials, string configuration)
        {

            using (var uow = new UnitOfWork(this._configuration, this._application))
            {
                var name = uow.UserRepository.GetCompleteNameByUser(credentials.User);
                var role = uow.UserRepository.GetRoleByUser(credentials.User);

                var claims = new List<Claim>()
                {
                    new Claim("userName", credentials.User),
                    new Claim("name", name),
                    new Claim("role", role)
                };

                var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration));
                var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(1);

                var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                    expires: expiration, signingCredentials: creds);

                return new AuthenticationResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };

            }
        }
    }
}
