using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
			builder.Property(x => x.Surname).HasMaxLength(64).IsRequired();
			builder.Property(x => x.MiddleName).HasMaxLength(64).IsRequired(false);
			builder.Property(x => x.PhoneNumber).HasMaxLength(64).IsRequired();
			builder.Property(x => x.Email).HasMaxLength(64).IsRequired();
		}
	}
}
