using CleanArchitecture.Core;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Models;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.WebApi.Extensions;
using CleanArchitecture.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");//burasý daha önce yorumdaydý 2 satýr
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSwaggerExtension();
builder.Services.AddControllers();
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddTransient<IAidOfferService, AidOfferService>();
builder.Services.AddTransient<IAidRequestService, AidRequestService>();
builder.Services.AddTransient<IAidPointService, AidPointService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<AidPointService>();// location için bu sýnýf kullanýldý
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAidPointRepositoryAsync, AidPointRepositoryAsync>();
builder.Services.AddTransient<IGeocodingService, GoogleGeocodingService>();
builder.Services.AddTransient<IRoutingService, GoogleRoutingService>();
builder.Services.AddTransient<IAidPointService, AidPointService>();


// Google API anahtarýný konfigürasyon dosyasýndan al
string googleApiKey = builder.Configuration["GoogleMaps:ApiKey"];

// GeocodingService için HttpClient yapýlandýrmasý
builder.Services.AddHttpClient<IGeocodingService, GoogleGeocodingService>(client =>
{
    client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());

// RoutingService için HttpClient yapýlandýrmasý
builder.Services.AddHttpClient<IRoutingService, GoogleRoutingService>(client =>
{
    client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/directions/");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());

//Build the application

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseAuthorization();


//Initialize Logger

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(app.Configuration)
                .CreateLogger();

//Seed Default Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await CleanArchitecture.Infrastructure.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
        await CleanArchitecture.Infrastructure.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);

        // Fetch the role from configuration
        var roleFromConfiguration = builder.Configuration.GetValue<string>("DefaultUserRole");
        if (!Enum.TryParse<Roles>(roleFromConfiguration, out var roleFromRequest))
        {
            roleFromRequest = Roles.DisasterAffected; // Default role if parsing fails
        }

        await CleanArchitecture.Infrastructure.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager, roleFromRequest);

        Log.Information("Finished Seeding Default Data");
        Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "An error occurred seeding the DB");
    }
    finally
    {
        Log.CloseAndFlush();
    }
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//Start the application
app.Run();