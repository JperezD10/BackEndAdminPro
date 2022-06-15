using Application.Interfaces;
using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class UserApplication : Application<User>, IUserApplication
    {
        private readonly IUserRepository _repository;
        public UserApplication(IUserRepository repository): base(repository)
        {
            _repository = repository;
        }
        public async Task<User> ValidateUserByEmail(string Email)
        {
            return await _repository.ValidateUserByEmail(Email);
        }
    }
}
