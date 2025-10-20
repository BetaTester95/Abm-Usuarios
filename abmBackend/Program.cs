using abm.data;
using abm.Services;
using abm.validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

// --- INICIO: Agregar Configuración CORS ---

// 1. Añadir el servicio de CORS y definir una política.
// Esta política permite el origen específico de tu aplicación web (el puerto 5500).
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

// --- FIN: Agregar Configuración CORS ---

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Validators 
builder.Services.AddScoped<Validators>();

// Registrar UsuarioServices
builder.Services.AddScoped<UsuarioServices>();

builder.Services.AddControllers();

var app = builder.Build();

// --- INICIO: Usar Middleware CORS ---

// 2. Aplicar la política de CORS. 
// DEBE ir antes de UseAuthorization y MapControllers
app.UseCors("AllowWebAppOrigin");

// --- FIN: Usar Middleware CORS ---

app.UseAuthentication();
app.MapControllers();
app.Run();