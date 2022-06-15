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
    public class HospitalDbContext : DbContext<Hospital>, IHospitalDbContext
    {
        public HospitalDbContext(ApiDbContext context): base(context)
        {

        }
        public async Task<bool> ExistsHospital(string Name)
        {
            return await _items.Where(h => h.Name == Name).AnyAsync();
        }
    }
}
