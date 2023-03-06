using MediatR;
using MongoDB.Driver;
using RatingAPI.Core;
using RatingAPI.Core.Abstraction;
using RatingAPI.Core.Domain;
using RatingAPI.Host.Middleware;
using RatingAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(AssemblyMarker));

builder.Services.AddSingleton<LoggingMiddleware>();
builder.Services.AddSingleton<ITextAnalyzer, TextAnalyzerService>();
builder.Services.AddSingleton<IRatingsRepository, MongoDbRatingsRepository>();
builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddSingleton<IMongoCollection<RatingEntity>>(sp =>
    sp.GetRequiredService<IMongoClient>()
        .GetDatabase("ratings-api")
        .GetCollection<RatingEntity>("ratings"));

var app = builder.Build();

app.UseLoggingMiddleware();

app.MapControllers();
app.Run();

public partial class Program {}