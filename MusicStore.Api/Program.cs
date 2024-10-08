using Microsoft.EntityFrameworkCore;
using MusicStore.Persistence;
using MusicStore.Services.Profiles;
using MusicStore.Repositories.DataProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicStore.Services;
using Serilog;
using MusicStore.Domain.Configuration;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using MusicStore.Api.Endpoints;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args); //Crea puerto de desarrollo para la aplicacion web

//Serilog
//Crea el logger para escribir dentro de Console
//MSSqlServerSinkOptions para configurar que no se guarden todos los logs posibles, sino ser�a muy pesado y afecta rendimiento
var logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("MusicStoreDb"),
    new MSSqlServerSinkOptions
    {
        AutoCreateSqlDatabase = true, //crea bd si no existe
        AutoCreateSqlTable = true, //crea tabla si no existe
        TableName = "ApiLogs" //nombre de la tabla

    }, restrictedToMinimumLevel: LogEventLevel.Warning)
    .CreateLogger();
builder.Logging.AddSerilog(logger);


if (builder.Environment.IsDevelopment())
{
    var loggerFile = new LoggerConfiguration()
        .WriteTo.Console(LogEventLevel.Information)
        .WriteTo.File("..\\log.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] - {Message}{NewLine}{Exception}",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Warning)
        .CreateLogger();


    builder.Logging.AddSerilog(loggerFile);
}


//Archivo de configuracion de aplicacion
builder.Services.Configure<AppConfig>(builder.Configuration);


var corsConfiguration = "MusicStoreApi";
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, policy =>
    {
        policy.AllowAnyOrigin(); ////Cualquiera puede consumir el api
        //policy.WithOrigins(new[] { "https://localhost:5500", "midominio.com" }); //Solo estos dominios pueden consumir el API
        policy.AllowAnyMethod(); //Cualquier metodo como patch
        policy.AllowAnyHeader(); //que permita Bearer token para autenticaci�n
    });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Creamos conexion con la base de datos
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreDb"));
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }

});


builder.Services.AddIdentity<MusicStoreUserIdentity, IdentityRole>(policies =>
{
    //Politicas de contrase�a
    policies.Password.RequireNonAlphanumeric = false;
    policies.Password.RequireLowercase = false;
    policies.Password.RequireUppercase = false;
    policies.Password.RequireDigit = true;
    policies.Password.RequiredLength = 6; //tama�o de 6

    policies.User.RequireUniqueEmail = true; //un unico usuario para no tener duplicados

    //Politicas de bloqueo de cuentas
    policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); //tiempo de 10 minutos para bloqueo de cuenta
    policies.Lockout.MaxFailedAccessAttempts = 5;
    policies.Lockout.AllowedForNewUsers = true; //nuevos usuarios tambien se bloquean de contrase�a erronea

}).AddEntityFrameworkStores<MusicStoreDbContext>()
.AddDefaultTokenProviders();

//Conexion de los servicios y repositorios desde DependencyInjection.cs
builder.Services.AddRepositories()
    .AddServices()
    .AddUploader(builder.Configuration);


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ConcertProfile>();
    config.AddProfile<DataProfile>(); //Profile de concert desde repositorio
    config.AddProfile<SaleProfile>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Music Store API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "Autenticacion por JWT usando como ejemplo en el header: Authorization: Bearer [token]",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    //SecretKey larga para convertir en un array de bytes
    //?? si no est� configurado, lanza InvalidOperationException
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
        throw new InvalidOperationException("No se configur� el JWT"));

    //validar token
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //valida emisor
        ValidateAudience = true, //valida audiencia, a quien esta dirigido
        ValidateLifetime = true, //valida duracion del token
        ValidateIssuerSigningKey = true, //valida la llave que yo especifico
        ValidIssuer = builder.Configuration["Jwt:Emisor"], //emisor autorizado del archivo de configuracion
        ValidAudience = builder.Configuration["Jwt:Audiencia"], //audiencia autorizada del archivo de configuracion
        IssuerSigningKey = new SymmetricSecurityKey(key) //tipo de seguridad para la llave

    };
});

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) //este muestra el swagger
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseCors(corsConfiguration);


app.MapControllers();
app.MapReports();
app.MapHomeEndpoints();


//solo aqui se crean instancias
using (var scope = app.Services.CreateScope())
{
    if (builder.Environment.IsDevelopment())
    {
        var db = scope.ServiceProvider.GetRequiredService<MusicStoreDbContext>();
        db.Database.Migrate();
    }
    //Ejecutamos la creacion del usuario admin y roles por default
    await UserDataSeeder.Seed(scope.ServiceProvider);
}
app.Run();

