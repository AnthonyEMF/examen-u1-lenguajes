using ExamenLenguajes;
using ExamenLenguajes.Database;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

// Configuración para el Seeder
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var loggerFactory = services.GetRequiredService<ILoggerFactory>();

	try
	{
		var context = services.GetRequiredService<ExamenLenguajesContext>();
		var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

		await ExamenLenguajesSeeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);
	}
	catch (Exception e)
	{
		var logger = loggerFactory.CreateLogger<Program>();
		logger.LogError(e, "Error al ejecutar el Seed de datos");
	}

}

app.Run();
