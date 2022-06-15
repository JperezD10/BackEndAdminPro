using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Doctor: Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Image { get; set; }

        public User User { get; set; }
        public Hospital Hospital { get; set; }

    }
}
