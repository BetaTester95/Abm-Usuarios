using abm.data;
using abm.Services;
using abm.validators;
using abm.controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowWebAppOrigin",
                      policy =>
                      {
                          // **IMPORTANTE:** Cambia este puerto si tu Live Server usa uno diferente
                          // 127.0.0.1 es equivalente a localhost
                          policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                                .AllowAnyHeader()   // Permite que el frontend envíe encabezados como Content-Type
                                .AllowAnyMethod();  // Permite métodos HTTP como GET, POST, PUT, DELETE
                      });
});


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Validators 
builder.Services.AddScoped<Validators>();

// Registrar UsuarioServices
builder.Services.AddScoped<UsuarioServices>();

var app = builder.Build();


// --- INICIO: Usar Middleware CORS ---

// 2. Aplicar la política de CORS. 
// DEBE ir antes de UseAuthorization y MapControllers
app.UseCors("AllowWebAppOrigin");

// --- FIN: Usar Middleware CORS ---

//verificar que la api este corriendo
app.MapGet("/", () => "¡La API está funcionando!");

app.UseAuthentication();
app.MapControllers();
app.Run();