using Microsoft.EntityFrameworkCore;
using Q2.Context;
using Q2.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
string connectionString = builder.Configuration.GetConnectionString("SQLiteConnection") ??
    throw new InvalidOperationException("Connection string 'SQLiteConnection' not found.");
builder.Services.AddDbContext<CustomerServiceContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddScoped<ICustomerProfileRepository, CustomerRepository>();

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
