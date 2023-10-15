using Microsoft.EntityFrameworkCore;
using Q2.Models;
using Q2.Repository;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
string path =Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, builder.Configuration.GetConnectionString("SqliteDb"));
string connectionString = $"Data Source={path}";
builder.Services.AddDbContext<CustomerServiceContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddScoped<ICustomerProfileRepository, CustomerRepository>();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                 .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\log-.log", rollingInterval: RollingInterval.Day)
               .WriteTo.Console()
               .CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
