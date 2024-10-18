using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLenguajes.Database.Entities
{
    [Table("requests", Schema = "dbo")]
    public class RequestEntity : BaseEntity
    {
        [Column("employee_id")]
        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual UserEntity Employee {  get; set; }

        [Display(Name = "Tipo de Solicitud")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(20)]
        [Column("request_type")]
        public string RequestType { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de Fin")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Hora de Inicio")]
        [Column("start_hour")]
        public DateTime StartHour { get; set; }

        [Display(Name = "Hora de Fin")]
        [Column("end_hour")]
        public DateTime EndHour { get; set; }

        [Display(Name = "Motivo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100)]
        [Column("reason")]
        public string Reason { get; set; }

        [Display(Name = "Estado")]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set; }
    }
}
