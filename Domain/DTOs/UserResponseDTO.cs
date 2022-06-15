using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserResponseDTO
    {
        public List<User> users { get; set; }
        public int totalUsers { get; set; }
    }
}
