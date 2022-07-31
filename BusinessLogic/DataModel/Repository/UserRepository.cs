using AutoMapper;
using BusinessLogic.DTOs.User;
using DataAccess.Context;
using DataAccess.Models;
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
            return this._context.Users.Any(x => x.UserName == credentials.User && x.Password == credentials.Password);
        }

        public string GetCompleteNameByUser(string userName)
        {
            string completeName = "";

            UserDTO user = this._mapper.Map<UserDTO>(this._context.Users.FirstOrDefault(x => x.UserName == userName));

            if (user != null)
                completeName = $"{user.Name}";

            return completeName;
        }

        public string GetRoleByUser(string userName)
        {
            string role = "";

            UserDTO user = this._mapper.Map<UserDTO>(this._context.Users.FirstOrDefault(x => x.UserName == userName));

            if (user != null && user.IdRole != null)
                role = this._context.Role.FirstOrDefault(x => x.Id == user.IdRole)?.Name;

            return role;
        }
    }
}
