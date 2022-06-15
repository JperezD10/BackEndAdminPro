using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthServices
{
    public interface ITokenHandlerService
    {
        string GenerateToken(ITokenParameters tokenParameters);
    }
}
