
#region MyConfig

using Microsoft.EntityFrameworkCore;
using GoldenChequeBack.Infrastructure.Extension;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Implementation;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GoldenChequeBack.Service;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var builder = WebApplication.CreateBuilder(args);





var configuration = builder.Configuration;
// NOTE: appsettings.json, appsettings.Development.json, user-secrets and environment
// variables are already loaded by the default host builder in the correct precedence
// order. Do NOT re-add appsettings.json here: it would be appended AFTER
// appsettings.Development.json and its empty "Jwt:Key"/ConnectionStrings values
// would override the local developer overrides.


builder.Services.AddControllers();
builder.Services.AddDbContext(configuration, builder.Configuration);
builder.Services.AddIdentityService(configuration);
//builder.Services.AddAutoMapper();
builder.Services.AddScopedServices();
builder.Services.AddTransientServices();
builder.Services.AddSingleton<IConfiguration>(configuration).AddFeatureManagement().AddFeatureFilter<PercentageFilter>();



builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMailSetting(configuration);

builder.Services.AddServiceLayer();
#region Service Injected


//CommentAttribute for Error add annotation to swagger
//builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

#endregion
var app = builder.Build();

// Apply EF Core migrations automatically at startup.
// Migrate() creates the database if it does not exist and the migrations
// themselves contain the seed data (HasData -> InsertData), so on a fresh
// machine the databases are created and seeded without any manual step.
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger("GoldenCheque.DatabaseStartup");

    try
    {
        var appDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        appDb.Database.Migrate();
        logger.LogInformation("ApplicationDbContext: database created/updated and seed data (Units, Categories) applied.");

        var identityDb = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        if (identityDb.Database.IsRelational())
        {
            identityDb.Database.Migrate();
            logger.LogInformation("IdentityContext: database created/updated and seed data (Roles, Users superadmin/basicuser) applied.");

            // Fix sign-in failures (System.FormatException from PasswordHasher) caused by
            // corrupted PasswordHash rows that exist in older local databases:
            // seeded users get a fresh, valid hash for the documented default passwords.
            await GoldenChequeBack.Persistence.Seeds.IdentityStartupRepair
                .RepairSeededUsersAsync(identityDb, logger);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex,
            "Database migration/seeding failed. Check ConnectionStrings (OnionArchConn / IdentityConnection) in appsettings.Development.json and make sure SQL Server is running.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}
app.UseHttpsRedirection();
app.UseCors(options =>
{
   options.AllowAnyHeader();
   options.AllowAnyOrigin();
   options.AllowAnyMethod();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles( new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
}  );
app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion