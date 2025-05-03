using Microsoft.EntityFrameworkCore;
using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Infrastructure.Data;
using UltimaPrueba.Infrastructure.Repository;
using UltimaPrueba.Services;
using UltimaPrueba.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UltimaPrueba.Infrastructure.Seed;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// CONFIGURACION DE JWT
//Obtiene la configuración de JWT desde
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];


//Configura el esquema de autenticación por defecto
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//Establece los parámetros de validación para tokens JWT
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

//Registro de Servicios
//AddScoped:Registra servicios con ciclo de vida por solicitud
builder.Services.AddScoped<IAuthService, AuthService>();
//AddControllers: Habilita el sistema de controladores
builder.Services.AddControllers();
//AddDbContext: Configura EF Core con base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("BilleteraDb"));
//AddScoped:Registra servicios con ciclo de vida por solicitud
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();



//Configuración de Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

/*AddSwaggerGen: Configura la documentación API con:
Versión y título de la API
Esquema de seguridad para JWT
Requerimiento de autenticación para endpoints*/
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UltimaPrueba API", Version = "v1" });

    /*Configuración para JWT*/
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {

        Description = @"JWT Authorization header using the Bearer scheme.  
                        Escribe 'Bearer' seguido de tu token.  
                        Ejemplo: 'Bearer eyJhbGciOi...'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData.Inicializar(context);
}


app.Run();
