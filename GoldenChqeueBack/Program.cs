
#region Last Program
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
#endregion

#region MyConfig

using Microsoft.EntityFrameworkCore;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//Sql Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Service Injected
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBankRepository, BankRepository>();
//builder.Services.AddScoped<ICustomService<Resultss>, ResultService>();
//builder.Services.AddScoped<ICustomService<Departments>, DepartmentsService>();
//builder.Services.AddScoped<ICustomService<SubjectGpas>, SubjectGpasService>();
#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
    //app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion