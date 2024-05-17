using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MusicStore.HealthCheckApi.HealthChecks;
using MusicStore.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreDb"));
});



//HealthChecks
builder.Services
    .AddHealthChecksUI(setup =>
    {
        setup.SetHeaderText("Comprobacion del estado de Music Store API");
        setup.SetEvaluationTimeInSeconds(10);
    }).AddInMemoryStorage();


//valida configuracion del api y de la base de datos
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "api" })
    .AddDbContextCheck<MusicStoreDbContext>(tags: new[] {"database"})
    .AddTypeActivatedCheck<PingHealthCheck>("Firebase",HealthStatus.Degraded, tags: new[] {"api"},args: "firebase.com")
    .AddTypeActivatedCheck<PingHealthCheck>("Azure", HealthStatus.Unhealthy, tags: new[] { "api" }, args: "azure.microsoft.com");
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/health/api", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = x => x.Tags.Contains("api")
}); ;
app.MapHealthChecks("/health/database", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = x => x.Tags.Contains("database")
});


app.MapHealthChecksUI();
app.Run();

