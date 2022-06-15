using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> ValidateUserByEmail(string Email);

        Task<User> LoginAsync(string Email, string Password);
    }
}
