using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLenguajes.Database.Entities
{
    [Table("departments", Schema = "dbo")]
    public class DepartmentEntity : BaseEntity
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} del departamento es requerido.")]
        [StringLength(50)]
        [Column("department")]
        public string Name { get; set; }

        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set; }
    }
}
