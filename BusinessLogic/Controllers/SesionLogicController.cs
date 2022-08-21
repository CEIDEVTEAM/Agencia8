using AutoMapper;
using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommonSolution.Constants;
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
    public class SesionLogicController
    {
        private IConfiguration _configuration;
        private string _application;
        

        public SesionLogicController(IConfiguration configuration, string application)
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
                UserDTO user = uow.UserRepository.GetUserWhitResourcesByUserName(credentials.User);

                var claims = new List<Claim>()
                {
                    new Claim("userName", credentials.User),
                    new Claim("name", user.Name),
                    new Claim("role", user.RoleName)
                };

                if (user.Resources.Any())
                {
                    int resCount = 1;
                    user.Resources.ForEach(x =>
                    {
                        claims.Add(new Claim($"resource{resCount}", x));
                        resCount++;
                    });
                }

                var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration));
                var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(20);

                var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                    expires: expiration, signingCredentials: creds);

                uow.LogRepository.LogAuthentication(user, token, CLog.login);

                return new AuthenticationResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };

            }
        }

        public bool SetLogout(string strToken)
        {
            using (var uow = new UnitOfWork(this._configuration, this._application))
            {
                JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(strToken);

                string userName = token.Claims.FirstOrDefault(x => x.Type == "userName").Value;

                UserDTO user = uow.UserRepository.GetUserByUserName(userName);

                uow.LogRepository.LogAuthentication(user, token, CLog.logout);

            }

            return true;
        }
    }
}
