using CardanoCalculationBackend.Services;
using ExternalApi.Api;
using ExternalApi.ConfigurationModel;
using ExternalApi.Interface;

var builder = WebApplication.CreateBuilder(args);

var Configurationbuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);

Configurationbuilder.AddEnvironmentVariables();
IConfiguration Configuration = Configurationbuilder.Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICardanoAPI, CardanoApi>();
builder.Services.AddScoped<CsvService>();
builder.Services.Configure<ExretnalApiModel>(Configuration.GetSection("ExternalApi"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
