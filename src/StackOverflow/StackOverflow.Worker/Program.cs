using Autofac.Extensions.DependencyInjection;
using Autofac;
using Serilog.Events;
using Serilog;
using StackOverflow.EmailWorker;
using StackOverflow.Application;
using StackOverflow.Infrastructure;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Options;
using StackOverflow.Infrastructure.Email;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Application.Utilities;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var migrationAssembly = typeof(Worker).Assembly.FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .UseSerilog()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterInstance<IConfiguration>(configuration);
            //builder.Register(ctx =>
            //{
            //    var config = ctx.Resolve<IConfiguration>();
            //    return Options.Create(config.GetSection("Smtp").Get<Smtp>());
            //}).As<IOptions<Smtp>>();

            builder.Register(ctx =>
            {
                var config = ctx.Resolve<IConfiguration>();
                var smtpOptions = config.GetSection("Smtp").Get<Smtp>() ?? new Smtp(); // Ensure it's not nullable
                return Options.Create(smtpOptions);
            }).As<IOptions<Smtp>>();


            builder.RegisterModule(new WorkerModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule(connectionString, migrationAssembly));
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();

    Log.Information("Application Starting up");
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}