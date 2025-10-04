using abm.data;
using abm.Services;
using abm.validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registrar Validators 
builder.Services.AddScoped<Validators>();

// Registrar UsuarioServices
builder.Services.AddScoped<UsuarioServices>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthentication();
app.MapControllers();
app.Run();
