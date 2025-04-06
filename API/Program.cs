using API.HostedServices;
using Application.Services;
using Domain.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Persistence.Configuration;
using Persistence.Repositories;
using Persistence.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders(); // remove default logging

var logConfig = new LoggerConfiguration()
    .WriteTo.File(
        path: "Logs/portfolyo-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug();

Log.Logger = logConfig.CreateLogger();

builder.Logging.AddSerilog(Log.Logger);

builder.Services
    .AddHttpClient<ICoinPriceService, CoinloreService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://api.coinlore.net/");
    });

builder.Services.AddSingleton<IPortfolioRepository, InMemoryPortfolioRepository>();
builder.Services.AddScoped<IUploadPortfolioService, UploadPortfolioService>();
builder.Services.AddScoped<IGetPortfolioService, GetPortfolioService>();
builder.Services.AddScoped<IRefreshPortfolioService, RefreshPortfolioService>();

builder.Services.AddHostedService<PortfolioRefreshHostedService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<CoinPriceOptions>(
    builder.Configuration.GetSection("CoinPrice"));

builder.Services.Configure<PortfolioRefreshOptions>(
    builder.Configuration.GetSection("PortfolioRefresh"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.Map("/error", (HttpContext http) =>
{
    var feature = http.Features.Get<IExceptionHandlerFeature>();
    var ex = feature?.Error;

    var problemDetails = new ProblemDetails
    {
        Title = "An unexpected error occurred",
        Detail = ex?.Message,
        Status = StatusCodes.Status500InternalServerError
    };

    return Results.Problem(problemDetails.Detail, statusCode: problemDetails.Status);
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error"); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
