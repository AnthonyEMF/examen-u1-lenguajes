using ExamenLenguajes.Database.Configuration;
using ExamenLenguajes.Database.Entities;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamenLenguajes.Database
{
	public class ExamenLenguajesContext : IdentityDbContext<IdentityUser>
	{
		private readonly IAuditService _auditService;

		public ExamenLenguajesContext(DbContextOptions options, IAuditService auditService) : base(options)
        {
			this._auditService = auditService;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Guardar por defecto en el Schema de security
			modelBuilder.HasDefaultSchema("security");

			// Asignar nombre especifico al crear tablas
			modelBuilder.Entity<IdentityUser>().ToTable("users");
			modelBuilder.Entity<IdentityRole>().ToTable("roles");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");

			// Configurations
			modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
			modelBuilder.ApplyConfiguration(new RequestConfiguration());

			// Set FKs OnRestrict
			var eTypes = modelBuilder.Model.GetEntityTypes();
			foreach (var type in eTypes)
			{
				var foreignKeys = type.GetForeignKeys();
				foreach (var foreignKey in foreignKeys)
				{
					foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
				}
			}
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker
				.Entries()
				.Where(e => e.Entity is BaseEntity && (
					e.State == EntityState.Added ||
					e.State == EntityState.Modified
				));

			foreach (var entry in entries)
			{
				var entity = entry.Entity as BaseEntity;
				if (entity != null)
				{
					if (entry.State == EntityState.Added)
					{
						entity.CreatedBy = _auditService.GetUserId();
						entity.CreatedDate = DateTime.Now;
					}
					else
					{
						entity.UpdatedBy = _auditService.GetUserId();
						entity.UpdatedDate = DateTime.Now;
					}
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		public DbSet<DepartmentEntity> Departments { get; set; }
		public DbSet<RequestEntity> Requests { get; set; }
		public DbSet<UserEntity> Users { get; set; }
	}
}
