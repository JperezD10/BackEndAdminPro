using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class UserService
    {
        public static User ConvertDTO(UserDTO dto)
        {
            return new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = EncriptadoService.ComputeSha256Hash(dto.Password),
                Google = dto.Google,
                Role = dto.Role,
            };
        }
    }
}
