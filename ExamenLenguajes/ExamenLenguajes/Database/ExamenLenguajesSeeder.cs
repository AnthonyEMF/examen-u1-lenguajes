using ExamenLenguajes.Constants;
using ExamenLenguajes.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ExamenLenguajes.Database
{
	public class ExamenLenguajesSeeder
	{
		public static async Task LoadDataAsync(
			ExamenLenguajesContext context,
			ILoggerFactory loggerFactory,
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			try
			{
				await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
				//await LoadDepartmentsAsync(loggerFactory, context);
				//await LoadRequestsAsync(loggerFactory, context);
			}
			catch (Exception e)
			{
				var logger = loggerFactory.CreateLogger<ExamenLenguajesSeeder>();
				logger.LogError(e, "Error inicializando la data del API");
			}
		}

		public static async Task LoadRolesAndUsersAsync(
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ILoggerFactory loggerFactory)
		{
			try
			{
				if (!await roleManager.Roles.AnyAsync())
				{
					await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
					await roleManager.CreateAsync(new IdentityRole(RolesConstant.HUMAN_RESOURCES));
					await roleManager.CreateAsync(new IdentityRole(RolesConstant.EMPLOYEE));
				}

				if (!await userManager.Users.AnyAsync())
				{
					var userAdmin = new IdentityUser
					{
						Email = "admin@gmail.com",
						UserName = "admin@gmail.com",
					};

					var userHumanResources = new IdentityUser
					{
						Email = "hresources@gmail.com",
						UserName = "hresources@gmail.com",
					};

					var userEmployee = new IdentityUser
					{
						Email = "employee@gmail.com",
						UserName = "employee@gmail.com",
					};

					await userManager.CreateAsync(userAdmin, "Temporal01*");
					await userManager.CreateAsync(userHumanResources, "Temporal01*");
					await userManager.CreateAsync(userEmployee, "Temporal01*");

					await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
					await userManager.AddToRoleAsync(userHumanResources, RolesConstant.HUMAN_RESOURCES);
					await userManager.AddToRoleAsync(userEmployee, RolesConstant.EMPLOYEE);
				}
			}
			catch (Exception e)
			{
				var logger = loggerFactory.CreateLogger<ExamenLenguajesSeeder>();
				logger.LogError(e.Message);
			}
		}

		//public static async Task LoadDepartmentsAsync(ILoggerFactory loggerFactory, ExamenLenguajesContext context)
		//{
		//	try
		//	{
		//		var jsonFilePath = "SeedData/departments.json";
		//		var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
		//		var departments = JsonConvert.DeserializeObject<List<DepartmentEntity>>(jsonContent);

		//		if (!await context.Departments.AnyAsync())
		//		{
		//			var user = await context.Users.FirstOrDefaultAsync();

		//			for (int i = 0; i < departments.Count; i++)
		//			{
		//				departments[i].CreatedBy = user.Id;
		//				departments[i].CreatedDate = DateTime.Now;
		//				departments[i].UpdatedBy = user.Id;
		//				departments[i].UpdatedDate = DateTime.Now;
		//			}

		//			context.AddRange(departments);
		//			await context.SaveChangesAsync();
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		var logger = loggerFactory.CreateLogger<ExamenLenguajesSeeder>();
		//		logger.LogError(e, "Error al ejecutar el Seed de departamentos");
		//	}
		//}

		//public static async Task LoadRequestsAsync(ILoggerFactory loggerFactory, ExamenLenguajesContext context)
		//{
		//	try
		//	{
		//		var jsonFilePath = "SeedData/requests.json";
		//		var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
		//		var requests = JsonConvert.DeserializeObject<List<RequestEntity>>(jsonContent);

		//		if (!await context.Requests.AnyAsync())
		//		{
		//			var user = await context.Users.FirstOrDefaultAsync();

		//			for (int i = 0; i < requests.Count; i++)
		//			{
		//				requests[i].CreatedBy = user.Id;
		//				requests[i].CreatedDate = DateTime.Now;
		//				requests[i].UpdatedBy = user.Id;
		//				requests[i].UpdatedDate = DateTime.Now;
		//			}

		//			context.AddRange(requests);
		//			await context.SaveChangesAsync();
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		var logger = loggerFactory.CreateLogger<ExamenLenguajesSeeder>();
		//		logger.LogError(e, "Error al ejecutar el Seed de solicitudes");
		//	}
		//}
	}
}
