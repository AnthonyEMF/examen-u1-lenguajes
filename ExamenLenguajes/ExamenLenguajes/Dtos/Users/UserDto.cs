using ExamenLenguajes.Dtos.Requests;

namespace ExamenLenguajes.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public virtual List<RequestDto> Requests { get; set; }
    }
}
