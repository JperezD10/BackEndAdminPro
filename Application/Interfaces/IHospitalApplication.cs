using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHospitalApplication: IApplication<Hospital>
    {
        Task<bool> ExistsHospital(string Name);
    }
}
