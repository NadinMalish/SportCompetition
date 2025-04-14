using Infrastructure.EntityFramework;
using Infrastructure.Repositories.ImplementationInfrastructure.EntityFrameworks;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using WebApplication.DataAccess.Repositories;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<EventParticipantRepository>();
builder.Services.AddScoped<StatusRepository>();

builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "PromoCode Factory API Doc";
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
    //await db.Database.EnsureDeletedAsync();
    //await db.Database.MigrateAsync(); //TODO: ����� �� �������� :(
    //await DbInitializer.InitializeAsync(db);
}

await app.RunAsync();