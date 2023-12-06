
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
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");


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