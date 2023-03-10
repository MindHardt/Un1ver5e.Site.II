using Serilog;

Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(config =>
	{
		config
		.AddJsonFile("appsettings.json", optional: true)
		.AddEnvironmentVariables()
		.AddUserSecrets(typeof(Program).Assembly);
	})
	.UseSerilog((ctx, logger) =>
	{
		logger
		.ReadFrom.Configuration(ctx.Configuration);
	})
	.ConfigureWebHostDefaults(webBuilder =>
	{
		webBuilder.UseStartup<Un1ver5e.Site.II.Startup>();
	})
	.Build()
	.Run();