using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface ITokenParameters
    {
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Id { get; set; }
    }
}
