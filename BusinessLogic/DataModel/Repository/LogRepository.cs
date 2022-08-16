using BusinessLogic.DTOs.User;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repository
{
    public class LogRepository
    {
        private readonly Agencia_8Context _context;

        public LogRepository(Agencia_8Context context)
        {
            this._context = context;
        }

        public void LogAuthentication(UserDTO user, JwtSecurityToken token, string action)
        {
            LtAuthentication entity = new LtAuthentication();

            entity.UserId = user.Id;
            entity.Action = action;
            entity.AddRow = DateTime.Now;
            entity.Token = new JwtSecurityTokenHandler().WriteToken(token);

            _context.LtAuthentication.Add(entity);
            _context.SaveChanges();
        }
    }
}
