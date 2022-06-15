using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HospitalServices
{
    public class HospitalService
    {
        public static Hospital convertDTO(HospitalDTO dTO, User user)
        {
            return new Hospital()
            {
                Image = dTO.Image,
                User = user,
                Name = dTO.Name,
            };
        }
    }
}
