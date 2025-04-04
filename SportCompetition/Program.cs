using Microsoft.EntityFrameworkCore;
using SportCompetition;
using SportCompetition.Domain;
using SportCompetition.Infrastructure;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepositoryExtD<>), typeof(RepositoryExtD<>));
builder.Services.AddScoped(typeof(IPotentRepository), typeof(PotentRepository));

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CompetitionContext>(options => options.UseNpgsql(connection));

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
