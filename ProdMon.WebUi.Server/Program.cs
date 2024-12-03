using ProdMon.Application;
using ProdMon.Infrastructure;
using ProdMon.WebUi.Server.Components;
using Serilog;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for file logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();

// Retrieve configuration values and check for null
var filePath = builder.Configuration["FileWatcher:FilePath"];
var lastCheckedFilePath = builder.Configuration["FileWatcher:LastCheckedFilePath"];
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(lastCheckedFilePath))
{
    throw new ArgumentNullException("File paths for FileWatcher cannot be null or empty.");
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException("Connection string cannot be null or empty.");
}

// Register Application and Infrastructure services
builder.Services.AddApplication(filePath, lastCheckedFilePath);
builder.Services.AddInfrastructure(connectionString);

// Register QuickGrid EntityFramework Adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Add MudBlazor services services.AddMudServices();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Add this line

app.Run();
