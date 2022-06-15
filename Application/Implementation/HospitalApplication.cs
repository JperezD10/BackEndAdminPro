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
    public class HospitalApplication : Application<Hospital>, IHospitalApplication
    {
        private readonly IHospitalRepository _repository;
        public HospitalApplication(IHospitalRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<bool> ExistsHospital(string Name)
        {
            return await _repository.ExistsHospital(Name);
        }
    }
}
