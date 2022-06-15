using Abstractions;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IHospitalDbContext: IDbContext<Hospital>
    {
        Task<bool> ExistsHospital(string Name);
    }
}
