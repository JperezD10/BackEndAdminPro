using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DoctorServices
{
    public class DoctorService
    {
        public static Doctor convertDTO(DoctorDTO dto, User user, Hospital hospital)
        {
            return new Doctor()
            {
                Hospital = hospital,
                User = user,
                Image = dto.Image,
                Name = dto.Name,
            };
        }
    }
}
