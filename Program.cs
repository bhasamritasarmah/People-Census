using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PeopleCensus.Models;
using PeopleCensus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<PeopleCensusDatabaseSettings>(
	builder.Configuration.GetSection(nameof(PeopleCensusDatabaseSettings)));

builder.Services.AddSingleton<IPeopleCensusDatabaseSettings>(sp =>
	sp.GetRequiredService<IOptions<PeopleCensusDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
	new MongoClient(builder.Configuration.GetValue<string>("PeopleCensusDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IPeopleCensusService, PeopleCensusService>();

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

app.MapControllers();

app.Run();
