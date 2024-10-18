using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenLenguajes.Dtos.Departments
{
    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "El {0} del departamento es requerido.")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
