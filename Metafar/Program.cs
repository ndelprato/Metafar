using System.Text;
using Metafar.Core.Models;
using Metafar.Infraestructura;
using Metafar.Infraestructura.Data;
using Metafar.Infraestructura.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"), b => b.MigrationsAssembly("Metafar"))); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
    {
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = 
            "JWT Authorization header using the Bearer scheme. Example: \r\n\r\n " +
            "Ingrese la palabra 'Bearer' seguido de un espacio y luego su token en el campo de texto a continuación.\r\n\r\n" +
            "Ejemplo: \"Bearer fghdfgfdhdfas\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"           
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });

    });



//Agrego los controllers
builder.Services.AddScoped<ISaldoRepositorio, SaldoRepositorio>();
builder.Services.AddScoped<IOperacionesRepositorio, OperacionesRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

var key = builder.Configuration.GetValue<string>("AppiSettings:Secreta");


//builder.Services.AddScoped<ISaldoRepositorio, SaldoRepositorio>();

// Add AutoMapper to the services container.
builder.Services.AddAutoMapper(typeof(ApiMappers));

builder.Services.AddAuthentication
(    
    x => 
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }       
).AddJwtBearer(
    x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
);

var app = builder.Build();
// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
