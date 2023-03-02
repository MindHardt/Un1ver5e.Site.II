using ArkLens.Models.Snapshots;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Un1ver5e.Site.II.Models;

namespace Un1ver5e.Site.II.Data;

public class ApplicationDbContext : IdentityDbContext<SiteUser>
{
	public DbSet<CharacterSnapshot> Characters => Set<CharacterSnapshot>();

	private static bool firstInvokation = true;
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
		if (firstInvokation)
		{
			Database.Migrate();
			firstInvokation = false;
		}
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<SiteUser>()
			.HasMany(u => u.Characters)
			.WithOne()
			.OnDelete(DeleteBehavior.NoAction);

		builder.Entity<CharacterSnapshot>()
			.HasKey(cs => cs.Name);
		builder.Entity<CharacterSnapshot>();
	}
}