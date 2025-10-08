using abm.data;
using abm.Services;
using abm.validators;
using abm.controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registrar Validators 
builder.Services.AddScoped<Validators>();

// Registrar UsuarioServices
builder.Services.AddScoped<UsuarioServices>();

var app = builder.Build();

//verificar que la api este corriendo
app.MapGet("/", () => "¡La API está funcionando!");

app.UseAuthentication();
app.MapControllers();
app.Run();
