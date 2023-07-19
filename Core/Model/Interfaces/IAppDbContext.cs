using Microsoft.EntityFrameworkCore;

namespace Core.Model.Interfaces
{
	public interface IAppDbContext
	{
		public DbSet<User> Users { get; }
		public DbSet<Organization> Organizations { get; }

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
