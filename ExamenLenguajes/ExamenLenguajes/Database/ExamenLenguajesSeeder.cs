using ExamenLenguajes.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
				// Importante el orden de ejecución
				await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
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
				//if (!await roleManager.Roles.AnyAsync())
				//{
				//	await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
				//	await roleManager.CreateAsync(new IdentityRole(RolesConstant.AUTHOR));
				//	await roleManager.CreateAsync(new IdentityRole(RolesConstant.USER));
				//}

				//if (!await userManager.Users.AnyAsync())
				//{
				//	var userAdmin = new IdentityUser
				//	{
				//		Email = "admin@blogunah.edu",
				//		UserName = "admin@blogunah.edu",
				//	};

				//	var userAuthor = new IdentityUser
				//	{
				//		Email = "author@blogunah.edu",
				//		UserName = "author@blogunah.edu",
				//	};

				//	var normalUser = new IdentityUser
				//	{
				//		Email = "user@blogunah.edu",
				//		UserName = "user@blogunah.edu",
				//	};

				//	await userManager.CreateAsync(userAdmin, "Temporal01*");
				//	await userManager.CreateAsync(userAuthor, "Temporal01*");
				//	await userManager.CreateAsync(normalUser, "Temporal01*");

				//	await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
				//	await userManager.AddToRoleAsync(userAuthor, RolesConstant.AUTHOR);
				//	await userManager.AddToRoleAsync(normalUser, RolesConstant.USER);
				//}
			}
			catch (Exception e)
			{
				var logger = loggerFactory.CreateLogger<ExamenLenguajesSeeder>();
				logger.LogError(e.Message);
			}
		}
	}
}
