using AutoMapper;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Mappers;
using BusinessLogic.Utils;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repository
{
    public class UserRepository
    {
        private readonly Agencia_8Context _context;
        private readonly UserMapper _mapper;

        public UserRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new UserMapper();
        }

        public bool ValidateCredentials(UserCredentials credentials)
        {
            return _context.Users.Any(x => x.UserName == credentials.User && x.Password == credentials.Password && x.ActiveFlag == "S");
        }

        public UserDTO GetUserByUserName(string userName)
        {
            var x = _context.Users.FirstOrDefault(x => x.UserName == userName);
            return _mapper.MapToObject(x);
        }

        public UserDTO GetUserWhitResourcesByUserName(string userName)
        {
            UserDTO user = _mapper.MapToObject(_context.Users.FirstOrDefault(x => x.UserName == userName));

            if (user != null && user.IdRole != null)
            {
                user.RoleName = _context.Role.FirstOrDefault(x => x.Id == user.IdRole)?.Name;
                user.Resources = GetResourcesByRole(user.IdRole ?? -1);
            }

            return user;
        }      

        public async void AddUser(UserCreationDTO obj)
        {
            User entity = _mapper.MapToEntity(obj);

            entity.ActiveFlag = "S";
            entity.AddRow = DateTime.Now;
            await _context.Users.AddAsync(entity);
        }

        public List<string> GetResourcesByRole(decimal roleId)
        {
            List<string> resources = new List<string>();

            resources = (from role in _context.Role
                         where role.Id == roleId
                         join rolePerm in _context.PermissionRole on role.Id equals rolePerm.IdRole
                         join perm in _context.Permission on rolePerm.IdPermission equals perm.Id
                         select new { resource = perm.Feature })
                         .Select(s => s.resource).ToList();

            return resources;
        }

        public IQueryable<VUser> GetUsers(string search)
        {
            return _context.VUsers.AsNoTracking().Where(x=>x.Name.ToLower().Contains(search.ToLower())).AsQueryable();
        }
      
        public UserCreationDTO GetUserById(decimal userId)
        {
           var x = _context.Users.FirstOrDefault(x => x.Id == userId);
            return _mapper.MapToEditObject(x);
        }

        public bool ExistUsuarioByUserName(string userName)
        {
            return _context.Users.Any(x => x.UserName == userName && x.ActiveFlag == "S");
        }
        public bool ExistUsuarioById(decimal userId)
        {
            return _context.Users.Any(x => x.Id == userId && x.ActiveFlag == "S");
        }

        public bool ExistUserRole(decimal roleId)
        {
            return _context.Role.Any(x => x.Id == roleId);
        }

        public void UpdateUser(UserCreationDTO dto)
        {
            User entity = _context.Users.FirstOrDefault(x => x.Id == dto.Id);
            entity = this._mapper.MapToEditEntity(dto, entity);

            entity.UpdRow = DateTime.Now;
            _context.Users.Update(entity);
        }

        public void DeleteUser(decimal userId)
        {
            User entity = _context.Users.FirstOrDefault(x => x.Id == userId);

            entity.ActiveFlag = "N";
            entity.UpdRow = DateTime.Now;
            _context.Users.Update(entity);
        }
    }
}
