using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class UserDbContext : DbContext<User>, IUserDbContext
    {
        public UserDbContext(ApiDbContext context): base(context)
        {
        }
        public async Task<User> ValidateUserByEmail(string Email)
        {
            return await _items.Where(x => x.Email == Email).FirstOrDefaultAsync();
        }
    }
}
