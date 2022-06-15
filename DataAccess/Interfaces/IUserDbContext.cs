using Abstractions;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserDbContext: IDbContext<User>
    {
        Task<User> ValidateUserByEmail(string Email);
        Task<User> LoginAsync (string Email, string Password);
    }
}
