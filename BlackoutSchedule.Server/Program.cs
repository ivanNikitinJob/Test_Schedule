using BlackoutSchedule.Server;
using Database;
using EFCore.AutomaticMigrations;
using Microsoft.EntityFrameworkCore;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddDbContext<EFContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:BlackoutDatabase"], providerOptions =>
        {
            providerOptions.EnableRetryOnFailure();
        });
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    InjectionProgramExtension.ConfigureRepositories(builder.Services);
    InjectionProgramExtension.ConfigureServices(builder.Services);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder => builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
    });

    var app = builder.Build();

    await using AsyncServiceScope serviceScope = app.Services.CreateAsyncScope();

    await using EFContext? dbContext = serviceScope.ServiceProvider.GetService<EFContext>();

    if (dbContext is not null)
    {
        await dbContext.MigrateToLatestVersionAsync(
            new DbMigrationsOptions
            {
#if DEBUG
                AutomaticMigrationDataLossAllowed = true /*test only*/
#else
                AutomaticMigrationDataLossAllowed = false
#endif
            });
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    Console.WriteLine("Shut down complete");
}