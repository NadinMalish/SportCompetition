using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using FileServerHost.Services;

var builder = WebApplication.CreateBuilder(args);

// Mongo config via appsettings or env: MongoDb:ConnectionString, DatabaseName, BucketName
var mongoConn = builder.Configuration["MongoDb:ConnectionString"] ?? "mongodb://localhost:27017";
var mongoDbName = builder.Configuration["MongoDb:DatabaseName"] ?? "docservice";
var bucketName = builder.Configuration["MongoDb:BucketName"] ?? "files";

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConn));
builder.Services.AddSingleton(provider =>
{
    var client = provider.GetRequiredService<IMongoClient>();
    var db = client.GetDatabase(mongoDbName);
    return new GridFSBucket(db, new GridFSBucketOptions { BucketName = bucketName, ChunkSizeBytes = 255 * 1024 });
});

builder.Services.AddScoped<GridFsDocumentStore>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapControllers();

app.Run();
