using ExamenLenguajes.Database.Entities;
using ExamenLenguajes.Dtos.Users;

namespace ExamenLenguajes.Dtos.Requests
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RequestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

        public virtual List<UserDto> Users { get; set; }
    }
}
