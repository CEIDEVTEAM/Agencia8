using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.User;
using DataAccess.Models;
using System.Globalization;

namespace BusinessLogic.Mappers
{
    public class UserMapper
    {

        public User MapToEntity(UserCreationDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new User()
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email,
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                IdRole = dto.IdRole,
            };
        }

        public UserCreationDTO MapToObject(User entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new UserCreationDTO()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
                Name = entity.Name,
                Address = entity.Address,
                Phone = entity.Phone,
                IdRole = entity.IdRole,
                Password = entity.Password,
            };
        }

        public UserDTO MapToObject(VUser entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new UserDTO()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
                Name = entity.Name,
                Address = entity.Address,
                Phone = entity.Phone,
                AddRow = entity.AddRow.ToString(),
                UpdRow = entity.UpdRow.ToString(),
                RoleName = entity.RoleName,
            };
        }

        public List<UserDTO> MapToObject(List<VUser> listEntity)
        {
            List<UserDTO> listDTO = new List<UserDTO>();
            listEntity.ForEach(x => { listDTO.Add(MapToObject(x)); });

            return listDTO;
        }

    }
}
