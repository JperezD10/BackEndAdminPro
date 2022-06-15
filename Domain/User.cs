using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User: Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Image { get; set; }

        [Required]
        [DefaultValue("")]
        public string Role { get; set; }

        public bool Google { get; set; }

    }
}