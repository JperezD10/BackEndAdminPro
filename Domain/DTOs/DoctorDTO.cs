using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class DoctorDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Image { get; set; }

        public int User { get; set; }
        public int Hospital { get; set; }
    }
}
