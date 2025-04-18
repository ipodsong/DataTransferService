using DataTransferService.Configs;
using DataTransferService.Loggers;
using DataTransferService.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Link API controller XML comments to Swagger UI
    // Project > Properties > Build > Output > Check "XML documentation file" to generate API documentation
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    bool includeControllerXmlComments = true;
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments);
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient();

// Register a singleton instance of AppSettings using the configuration from the builder
var environment = builder.Environment.EnvironmentName; // (Production/Development)

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSingleton(new AppSettings(builder.Configuration));

// Add mongodb logger
builder.Services.AddSingleton<ILoggerProvider, MongoDBLoggerProvider>();

builder.Services.AddSingleton<TransferService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.UseRouting();

app.Run();