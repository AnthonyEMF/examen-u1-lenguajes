using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenLenguajes.Dtos.Users
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(13)]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        public string Position { get; set; }

        public Guid DepartmentId { get; set; }

    }
}
