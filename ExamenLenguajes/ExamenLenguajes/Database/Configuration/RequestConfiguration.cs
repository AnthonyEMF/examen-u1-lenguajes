﻿using ExamenLenguajes.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamenLenguajes.Database.Configuration
{
	public class RequestConfiguration : IEntityTypeConfiguration<RequestEntity>
	{
		public void Configure(EntityTypeBuilder<RequestEntity> builder)
		{
			builder.HasOne(e => e.CreatedByUser)
				.WithMany()
				.HasForeignKey(e => e.CreatedBy)
				.HasPrincipalKey(e => e.Id);

			builder.HasOne(e => e.UpdatedByUser)
				.WithMany()
				.HasForeignKey(e => e.UpdatedBy)
				.HasPrincipalKey(e => e.Id);
		}
	}
}
