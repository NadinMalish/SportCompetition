using Microsoft.EntityFrameworkCore;
using SportCompetition.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

var app = builder.Build();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseOpenApi();
app.UseHttpsRedirection();

app.UseRouting();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUi(x =>
    {
        x.DocExpansion = "list";
    });
}

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});


app.Run();
