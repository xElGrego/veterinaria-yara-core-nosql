using Common.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using veterinaria_yara_core_nosql.api.Extensions;
using veterinaria_yara_core_nosql.infrastructure.extentions;
using veterinaria_yara_core_nosql.infrastructure.ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterDependencies();
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    var title = builder.Configuration["OpenApi:info:title"];
    var version = builder.Configuration["OpenApi:info:version"];
    var description = builder.Configuration["OpenApi:info:description"];

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = title,
        Description = description,
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = version,
        Title = title,
        Description = description,
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


var corsOrigin = "_AllowPolicy";

IConfigurationSection myArraySection = builder.Configuration.GetSection("AuthorizeSite:SiteUrl");

string[] folders = myArraySection.Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigin, policy =>
    {
        policy.WithOrigins(folders)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Host.UseSerilog(SeriLogger.Configure);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
    }
   );
}


//Configurando HTTP request
app.ConfigureMetricServer();
app.ConfigureExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(corsOrigin);
app.Run();
