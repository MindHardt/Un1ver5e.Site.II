using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Un1ver5e.Site.II.Areas.Identity;
using Un1ver5e.Site.II.Data;
using Un1ver5e.Site.II.Services;

namespace Un1ver5e.Site.II;

public class Startup
{
	private readonly IConfiguration _cfg;
	private readonly IWebHostEnvironment _env;

	public Startup(IConfiguration cfg, IWebHostEnvironment env)
	{
		_cfg = cfg;
		_env = env;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		const string ConnStrName = "DefaultConnection";

		var connectionString = _cfg.GetConnectionString(ConnStrName) ?? throw new InvalidOperationException($"Connection string '{ConnStrName}' not found.");
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(connectionString));

		services.AddDatabaseDeveloperPageExceptionFilter();
		services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = /*true*/ false)
			.AddEntityFrameworkStores<ApplicationDbContext>();

		services.AddRazorPages();
		services.AddServerSideBlazor();
		services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
		services.AddSingleton<WeatherForecastService>();

		services.AddAuthentication()
			.AddGoogle(options =>
			{
				IConfigurationSection googleCfg =
				_cfg.GetSection("Authentication:Google");

				options.ClientId = googleCfg["ClientId"]!;
				options.ClientSecret = googleCfg["ClientSecret"]!;	
			});
	}

	public void Configure(IApplicationBuilder app)
	{
		if (_env.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();
		app.UseAuthentication();

		app.UseEndpoints(ep =>
		{
			ep.MapControllers();
			ep.MapBlazorHub();
			ep.MapFallbackToPage("/_Host");
		});

	}
}
