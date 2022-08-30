using AutoMapper;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Utils;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public UserRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<User, UserDTO>()));
        }

        public bool ValidateCredentials(UserCredentials credentials)
        {
            return _context.Users.Any(x => x.UserName == credentials.User && x.Password == credentials.Password && x.ActiveFlag == "S");
        }

        public UserDTO GetUserByUserName(string userName)
        {
            var x = _context.Users.FirstOrDefault(x => x.UserName == userName);
            return _mapper.Map<UserDTO>(x);
        }

        public UserDTO GetUserWhitResourcesByUserName(string userName)
        {
            UserDTO user = _mapper.Map<UserDTO>(_context.Users.FirstOrDefault(x => x.UserName == userName));

            if (user != null && user.IdRole != null)
            {
                user.RoleName = _context.Role.FirstOrDefault(x => x.Id == user.IdRole)?.Name;
                user.Resources = GetResourcesByRole(user.IdRole ?? -1);
            }

            return user;
        }

        public async void AddUser(User user)
        {
            user.AddRow = DateTime.Now;
            await _context.Users.AddAsync(user);
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

        public IQueryable<User> GetUsers()
        {
            return _context.Users.Where(x => x.ActiveFlag == "S").AsQueryable();
        }
      
        public User GetUserById(decimal userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
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

        public void UpdateUser(User user)
        {
            user.UpdRow = DateTime.Now;
            _context.Users.Update(user);
        }
    }
}
