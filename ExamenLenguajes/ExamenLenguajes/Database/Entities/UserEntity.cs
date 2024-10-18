using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLenguajes.Database.Entities
{
    [Table("users", Schema = "security")]
    public class UserEntity : IdentityUser
    {
		[Key]
		public override string Id { get; set; } = Guid.NewGuid().ToString();

		[StringLength(450)]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(450)]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Display(Name = "DNI")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(13)]
        [Column("dni")]
        public string DNI { get; set; }

		[Display(Name = "Contraseña")]
		[Required(ErrorMessage = "La {0} es requerido.")]
		[StringLength(13)]
		[Column("password")]
		public string Password { get; set; }

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
