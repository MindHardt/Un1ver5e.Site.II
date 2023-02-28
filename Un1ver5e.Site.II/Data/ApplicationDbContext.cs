using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Un1ver5e.Site.II.Data;

public class ApplicationDbContext : IdentityDbContext
{
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
}