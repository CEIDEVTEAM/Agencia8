using BusinessLogic.DTOs.User;
using BusinessLogic.Mappers;
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
        private readonly LogMapper _mapper;

        public LogRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new LogMapper();
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

        public void LogDependent(Dependent dependent, decimal userId, string action)
        {
            LtDependent entity = _mapper.MapToLogEntity(dependent);

            entity.AddRow = DateTime.Now;
            entity.Action = action;
            entity.IdUser = userId;

            _context.LtDependent.Add(entity);
            _context.SaveChanges();
        }

        public void LogCandidate(Candidate candidate, decimal userId, string action)
        {
            LtCandidate entity = _mapper.MapToLogEntity(candidate);

            entity.AddRow = DateTime.Now;
            entity.Action = action;
            entity.IdUser = userId;

            _context.LtCandidate.Add(entity);
            _context.SaveChanges();
        }

        public void LogShopData(ShopData shopData, decimal userId, string action)
        {
            LtShopData entity = _mapper.MapToLogEntity(shopData);

            entity.AddRow = DateTime.Now;
            entity.Action = action;
            entity.IdUser = userId;

            _context.LtShopData.Add(entity);
            _context.SaveChanges();
        }


    }
}
