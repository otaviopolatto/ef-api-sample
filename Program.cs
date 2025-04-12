using Financecontrol.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using FinanceControl.Service.TagService;
using FinanceControl.Service.LancamentoService;


var builder = WebApplication.CreateBuilder(args);

// Debug
var connectionString = builder.Configuration["ConnectionString"];
Console.WriteLine("ConnectionString: " + connectionString);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    x.JsonSerializerOptions.WriteIndented = true;
    // Remove os Campos "id e "values" da Serialização JSON
    // x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; 
});

// Services
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ILancamentoService, LancamentoService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = $"v1",
        Title = "FinanceControl API",
        Description = $"An ASP.NET Core Web API for Finance Control",
    });

    setup.EnableAnnotations();

    var xmlFiles = new[]
    {
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
        $"Domain.xml",
    };

    foreach (var xmlFile in xmlFiles)
    {
        var xmlCommentFile = xmlFile;
        var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
        if (File.Exists(xmlCommentsFullPath))
        {
            setup.IncludeXmlComments(xmlCommentsFullPath);
        }
    }

});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionString"]));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", c => c
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.ConfigObject = new ConfigObject
    {
        ShowCommonExtensions = true,
        ShowExtensions = true
    };

    c.ConfigObject.AdditionalItems.Add("showCommonExtensions", true);

    c.ShowExtensions();
});

app.UseCors("CorsPolicy");

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
