using DataAccess.Interfaces;
using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IUserDbContext _context;

        public UserRepository(IUserDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<User> LoginAsync(string Email, string Password)
        {
            return await _context.LoginAsync(Email, Password);
        }

        public async Task<User> ValidateUserByEmail(string Email)
        {
            return await _context.ValidateUserByEmail(Email);
        }
    }
}
