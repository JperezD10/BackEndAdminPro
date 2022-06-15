using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserApplication: IApplication<User>
    {
        Task<User> ValidateUserByEmail(string Email);
        Task<User> LoginAsync(string Email, string Password);
    }
}
