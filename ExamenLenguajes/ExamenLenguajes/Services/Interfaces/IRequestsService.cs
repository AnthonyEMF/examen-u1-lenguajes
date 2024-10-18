using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Dtos.Requests;

namespace ExamenLenguajes.Services.Interfaces
{
    public interface IRequestsService
    {
        Task<ResponseDto<PaginationDto<List<RequestDto>>>> GetAllRequestsAsync(string searchTerm = "", int page = 1);
        Task<ResponseDto<RequestDto>> GetRequestByIdAsync(Guid id);
        Task<ResponseDto<RequestDto>> CreateRequestAsync(RequestCreateDto dto);
        Task<ResponseDto<RequestDto>> EditRequestAsync(RequestEditDto dto, Guid id);
        Task<ResponseDto<RequestDto>> DeleteRequestAsync(Guid id);
    }
}
