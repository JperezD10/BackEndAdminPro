using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public bool Login { get; set; }
        public string Error { get; set; }
        public User User { get; set; }
    }
}
