using AspNetCoreRateLimit;
using Serilog;
using WebSeriLogApi.Contacts;
using WebSeriLogApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add DI of service
builder.Services.AddSingleton<IMaskService, MaskService>();
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Set minimum log level
    .WriteTo.Console()    // Log to console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Optional: Log to file
    .Enrich.FromLogContext() // Include contextual information
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog as the logging provider

// Load the configuration from appsettings.json
// Load configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// Required services for rate limiting
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Add the necessary processing strategy
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// Add services to the container.
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

// Enable IP rate limiting middleware
app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
