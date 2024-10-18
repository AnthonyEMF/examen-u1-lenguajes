using AutoMapper;
using ExamenLenguajes.Constants;
using ExamenLenguajes.Database;
using ExamenLenguajes.Database.Entities;
using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Dtos.Requests;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamenLenguajes.Services
{
	public class RequestsService : IRequestsService
	{
        private readonly ExamenLenguajesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RequestEntity> _logger;
        private readonly IConfiguration _configuration;
        private readonly int PAGE_SIZE;

        public RequestsService(
            ExamenLenguajesContext context,
            IMapper mapper,
            ILogger<RequestEntity> logger,
            IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
            this._configuration = configuration;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<RequestDto>>>> GetAllRequestsAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var requestsEntityQuery = _context.Requests
                .Include(e => e.Employee)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                requestsEntityQuery = requestsEntityQuery
                    .Where(e => e.Employee.FirstName.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalRequests = await requestsEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRequests / PAGE_SIZE);

            var requestsEntity = await requestsEntityQuery
                .OrderByDescending(e => e).Skip(startIndex).Take(PAGE_SIZE).ToListAsync();

            var requestsDto = _mapper.Map<List<RequestDto>>(requestsEntity);

            return new ResponseDto<PaginationDto<List<RequestDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<RequestDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalRequests,
                    TotalPages = totalPages,
                    Items = requestsDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<RequestDto>> GetRequestByIdAsync(Guid id)
        {
            var requestsEntity = await _context.Requests
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (requestsEntity is null)
            {
                return new ResponseDto<RequestDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND
                };
            }

            var requestDto = _mapper.Map<RequestDto>(requestsEntity);

            return new ResponseDto<RequestDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = requestDto
            };
        }

        public async Task<ResponseDto<RequestDto>> CreateRequestAsync(RequestCreateDto dto)
        {
            try
            {
                var existingEmployee = await _context.Users.FindAsync(dto.EmployeeId);
                if (existingEmployee is null)
                {
                    return new ResponseDto<RequestDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"EmployeeId: {MessagesConstant.RECORD_NOT_FOUND}"
                    };
                }

                var requestEntity = _mapper.Map<RequestEntity>(dto);

                _context.Requests.Add(requestEntity);
                await _context.SaveChangesAsync();

                var requestDto = _mapper.Map<RequestDto>(requestEntity);

                return new ResponseDto<RequestDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = MessagesConstant.CREATE_SUCCESS,
                    Data = requestDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessagesConstant.CREATE_ERROR);
                return new ResponseDto<RequestDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = MessagesConstant.CREATE_ERROR
                };
            }
        }

        public async Task<ResponseDto<RequestDto>> EditRequestAsync(RequestEditDto dto, Guid id)
        {
            try
            {
                var requestEntity = await _context.Requests
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(e => e.Id == id);

                if (requestEntity is null)
                {
                    return new ResponseDto<RequestDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = MessagesConstant.RECORD_NOT_FOUND
                    };
                }

                requestEntity.Status = dto.Status;

                _context.Requests.Update(requestEntity);
                await _context.SaveChangesAsync();

                var eventDto = _mapper.Map<RequestDto>(requestEntity);

                return new ResponseDto<RequestDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = MessagesConstant.UPDATE_SUCCESS,
                    Data = eventDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessagesConstant.UPDATE_ERROR);
                return new ResponseDto<RequestDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = MessagesConstant.UPDATE_ERROR
                };
            }
        }

        public async Task<ResponseDto<RequestDto>> DeleteRequestAsync(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var requestEntity = await _context.Requests
                        .Include(e => e.Employee)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (requestEntity is null)
                    {
                        return new ResponseDto<RequestDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = MessagesConstant.RECORD_NOT_FOUND
                        };
                    }

                    _context.Requests.Remove(requestEntity);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new ResponseDto<RequestDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.DELETE_SUCCESS
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, MessagesConstant.DELETE_ERROR);
                    return new ResponseDto<RequestDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.DELETE_ERROR
                    };
                }
            }
        }
    }
}
