using AutoMapper;
using ExamenLenguajes.Constants;
using ExamenLenguajes.Database;
using ExamenLenguajes.Database.Entities;
using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Dtos.Users;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamenLenguajes.Services
{
	public class UsersService : IUsersService
	{
		private readonly ExamenLenguajesContext _context;
		private readonly IAuditService _auditService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		private readonly int PAGE_SIZE;

		public UsersService(
            ExamenLenguajesContext context,
			IAuditService auditService,
            IMapper mapper,
            ILogger<UsersService> logger,
            IConfiguration configuration)
        {
			this._context = context;
			this._auditService = auditService;
			this._mapper = mapper;
			this._logger = logger;
			PAGE_SIZE = configuration.GetValue<int>("PageSize");
		}

		public async Task<ResponseDto<PaginationDto<List<UserDto>>>> GetAllUsersAsync(string searchTerm = "", int page = 1)
		{
			int startIndex = (page - 1) * PAGE_SIZE;

			var usersEntityQuery = _context.Users.Include(u => u.Requests).AsQueryable();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				usersEntityQuery = usersEntityQuery
					.Where(u => (u.FirstName + " " + u.LastName + " " + u.Email)
					.ToLower().Contains(searchTerm.ToLower()));
			}

			int totalUsers = await usersEntityQuery.CountAsync();
			int totalPages = (int)Math.Ceiling((double)totalUsers / PAGE_SIZE);

			var usersEntity = await usersEntityQuery
				.OrderByDescending(e => e.CreatedDate)
				.Skip(startIndex)
				.Take(PAGE_SIZE)
				.ToListAsync();

			var usersDto = _mapper.Map<List<UserDto>>(usersEntity);

			return new ResponseDto<PaginationDto<List<UserDto>>>
			{
				StatusCode = 200,
				Status = true,
				Message = MessagesConstant.RECORDS_FOUND,
				Data = new PaginationDto<List<UserDto>>
				{
					CurrentPage = page,
					PageSize = PAGE_SIZE,
					TotalItems = totalUsers,
					TotalPages = totalPages,
					Items = usersDto,
					HasPreviousPage = page > 1,
					HasNextPage = page < totalPages,
				}
			};
		}

		public async Task<ResponseDto<UserDto>> GetUserByIdAsync(string id)
		{
			var userEntity = await _context.Users.Include(u => u.Requests).FirstOrDefaultAsync(u => u.Id == id);
			if (userEntity == null)
			{
				return new ResponseDto<UserDto>
				{
					StatusCode = 404,
					Status = false,
					Message = MessagesConstant.RECORD_NOT_FOUND,
					Data = null
				};
			}

			var userDto = _mapper.Map<UserDto>(userEntity);
			return new ResponseDto<UserDto>
			{
				StatusCode = 200,
				Status = true,
				Message = MessagesConstant.RECORD_FOUND,
				Data = userDto
			};
		}

		public async Task<ResponseDto<UserDto>> CreateAsync(UserCreateDto dto)
		{
			var userEntity = _mapper.Map<UserEntity>(dto);
			userEntity.Id = Guid.NewGuid().ToString();
			userEntity.UserName = dto.Email;
			userEntity.NormalizedUserName = dto.Email.ToUpper();
			userEntity.NormalizedEmail = dto.Email.ToUpper();
			userEntity.CreatedDate = DateTime.Now;
			//userEntity.CreatedBy = _auditService.GetUserId();


			var result = await _context.Users.AddAsync(userEntity);
			await _context.SaveChangesAsync();

			var userDto = _mapper.Map<UserDto>(result.Entity);
			return new ResponseDto<UserDto>
			{
				StatusCode = 201,
				Status = true,
				Message = MessagesConstant.CREATE_SUCCESS,
				Data = userDto
			};
		}

		public async Task<ResponseDto<UserDto>> EditAsync(UserEditDto dto, string id)
		{
			var userEntity = await _context.Users.FindAsync(id);
			if (userEntity == null)
			{
				return new ResponseDto<UserDto>
				{
					StatusCode = 404,
					Status = false,
					Message = MessagesConstant.RECORD_NOT_FOUND,
					Data = null
				};
			}

			// Actualizar propiedades
			userEntity.FirstName = dto.FirstName;
			userEntity.LastName = dto.LastName;
			userEntity.Email = dto.Email;
			userEntity.Password = dto.Password;
			userEntity.PhoneNumber = dto.PhoneNumber;
			userEntity.DNI = dto.DNI;
			userEntity.Position = dto.Position;
			userEntity.DepartmentId = dto.DepartmentId;
			userEntity.UpdatedDate = DateTime.Now;
			//userEntity.UpdatedBy = _auditService.GetUserId();

			await _context.SaveChangesAsync();

			var userDto = _mapper.Map<UserDto>(userEntity);
			return new ResponseDto<UserDto>
			{
				StatusCode = 200,
				Status = true,
				Message = MessagesConstant.UPDATE_SUCCESS,
				Data = userDto
			};
		}

		public async Task<ResponseDto<UserDto>> DeleteAsync(string id)
		{
			var userEntity = await _context.Users.FindAsync(id);
			if (userEntity == null)
			{
				return new ResponseDto<UserDto>
				{
					StatusCode = 404,
					Status = false,
					Message = MessagesConstant.RECORD_NOT_FOUND,
					Data = null
				};
			}

			_context.Users.Remove(userEntity);
			await _context.SaveChangesAsync();

			return new ResponseDto<UserDto>
			{
				StatusCode = 200,
				Status = true,
				Message = MessagesConstant.DELETE_SUCCESS,
				Data = null
			};
		}
	}
}
