using System.ComponentModel.DataAnnotations;

namespace ExamenLenguajes.Dtos.Requests
{
    public class RequestEditDto
    {
		[StringLength(20)]
		public string Status { get; set; }
	}
}
