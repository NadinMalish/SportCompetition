using Infrastructure.EntityFramework;
using Infrastructure.Repositories.ImplementationInfrastructure.EntityFrameworks;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using WebApplication.DataAccess.Repositories;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("WebApplication")
    ));

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<EventParticipantRepository>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<DocTypeRepository>();
builder.Services.AddScoped<DocRepository>();
builder.Services.AddScoped<PotentRepository>();
builder.Services.AddScoped<CompetitionRepository>();


builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Competitions Events API Doc";
    options.Version = "1.0";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseOpenApi();
app.UseSwaggerUi(x =>
{
    x.DocExpansion = "list";
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Context>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
    //await db.Database.MigrateAsync(); //TODO: Метод не работает :(
    await DbInitializer.InitializeAsync(db);
}


await app.RunAsync();