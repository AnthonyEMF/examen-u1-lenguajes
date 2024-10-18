using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLenguajes.Database.Entities
{
    [Table("users", Schema = "dbo")]
    public class UserEntity : IdentityUser
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        [Column("fisrt_name")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        //[Display(Name = "Correo")]
        //[Required(ErrorMessage = "El {0} es requerido.")]
        //[StringLength(100)]
        //[Column("email")]
        //public string Email { get; set; }

        [Display(Name = "DNI")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(13)]
        [Column("dni")]
        public string DNI { get; set; }

        //[Display(Name = "Teléfono")]
        //[Required(ErrorMessage = "El {0} es requerido.")]
        //[StringLength(20)]
        //[Column("phone_number")]
        //public string PhoneNumber { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        [Column("position")]
        public string Position { get; set; }

        [Column("department_id")]
        public Guid DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentEntity Department { get; set; }

        public virtual ICollection<RequestEntity> Requests { get; set; }

        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set;}
    }
}
