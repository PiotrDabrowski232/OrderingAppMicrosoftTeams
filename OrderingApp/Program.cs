using Microsoft.EntityFrameworkCore;
using Microsoft.Fast.Components.FluentUI;
using OrderingApp.Logic;
using OrderingApp.Components;
using OrderingApp.Data.DBConfig;
using OrderingApp.DIConfig;
using OrderingApp.Interop.TeamsSDK;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var config = builder.Configuration.Get<ConfigOptions>();
builder.Services.AddTeamsFx(config.TeamsFx.Authentication);
builder.Services.AddScoped<MicrosoftTeams>();

builder.Services.AddControllers();
builder.Services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() 
    .WriteTo.Console()    
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();

builder.Host.UseSerilog();

//DbContext

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<OrderingDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.WithServices();

builder.Services.AddFluentUIComponents();
builder.Services.AddDataGridEntityFrameworkAdapter();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();