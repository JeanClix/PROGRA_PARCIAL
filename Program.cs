using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROGRA_PARCIAL.Data;
using PROGRA_PARCIAL.Services;


var builder = WebApplication.CreateBuilder(args);

// Configuración para PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection")
    ?? throw new InvalidOperationException("Connection string 'PostgreSQLConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));  // Usa Npgsql para PostgreSQL

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuración de Identity con EntityFramework
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Registro de servicios adicionales
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CurrencyConversionService>();
builder.Services.AddHttpClient<ICurrencyConversionService, CurrencyConversionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
