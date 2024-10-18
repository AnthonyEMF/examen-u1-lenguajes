using AutoMapper;
using ExamenLenguajes.Database.Entities;
using ExamenLenguajes.Dtos.Departments;
using ExamenLenguajes.Dtos.Requests;
using ExamenLenguajes.Dtos.Users;

namespace ExamenLenguajes.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			MapsForUsers();
			MapsForRequests();
			MapsForDepartments();
		}

		private void MapsForUsers()
		{
			CreateMap<UserEntity, UserDto>();
			CreateMap<UserCreateDto, UserEntity>();
			CreateMap<UserEditDto, UserEntity>();
		}

		private void MapsForRequests()
		{
			CreateMap<RequestEntity, RequestDto>();
			CreateMap<RequestCreateDto, RequestEntity>();
			CreateMap<RequestEditDto, RequestEntity>();
		}

		private void MapsForDepartments()
		{
			CreateMap<DepartmentEntity, DepartmentDto>();
			CreateMap<DepartmentCreateDto, DepartmentEntity>();
			CreateMap<DepartmentEditDto, DepartmentEntity>();
		}
	}
}
