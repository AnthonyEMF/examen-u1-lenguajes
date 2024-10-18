using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenLenguajes.Dtos.Requests
{
    public class RequestCreateDto
    {
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(20)]
        public string RequestType { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Hora de Inicio")]
        public DateTime StartHour { get; set; }

        [Display(Name = "Hora de Fin")]
        public DateTime EndHour { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100)]
        public string Reason { get; set; }
    }
}
