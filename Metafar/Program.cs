using Metafar.Infraestructura;
using Metafar.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"), b => b.MigrationsAssembly("Metafar"))); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Agrego los controllers
builder.Services.AddScoped<ISaldoRepositorio, SaldoRepositorio>();

//builder.Services.AddScoped<ISaldoRepositorio, SaldoRepositorio>();

// Add AutoMapper to the services container.
builder.Services.AddAutoMapper(typeof(ApiMappers));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
