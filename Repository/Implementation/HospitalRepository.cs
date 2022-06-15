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
    public class HospitalRepository : Repository<Hospital>, IHospitalRepository
    {
        private readonly IHospitalDbContext _context;

        public HospitalRepository(IHospitalDbContext context): base(context)
        {
            _context = context;
        }
        public async Task<bool> ExistsHospital(string Name)
        {
            return await _context.ExistsHospital(Name);
        }
    }
}
