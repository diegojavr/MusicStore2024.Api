using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using MusicStore.Api.Controllers;
using MusicStore.Domain;
using MusicStore.Persistence;
using MusicStore.Repositories.Implementations;
using MusicStore.Repositories.Interfaces;
using MusicStore.Services.Interfaces;
using MusicStore.Services.Implementations;
using MusicStore.Services.Profiles;
using MusicStore.Repositories.DataProfile;
using MusicStore.Domain.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args); //Crea puerto de desarrollo para la aplicacion web

builder.Services.Configure<AppConfig>(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Creamos conexion con la base de datos
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreDb"));

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


//builder.Services.AddSingleton<IGenreRepository,GenreRepository>(); //Solo localmente
builder.Services.AddTransient<IGenreRepository, GenreRepository>(); //Con base de datos
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IConcertRepository, ConcertRepository>(); //Con base de datos
builder.Services.AddTransient<IConcertService, ConcertService>();

//Si consigue el valor core.windows.net utiliza el uploader a Azure, sino ser� local
if (builder.Configuration.GetSection("StorageConfiguration:Path").Value!.Contains("core.windows.net"))
{
    builder.Services.AddTransient<IFileUploader, AzureBlobStorageUploader>();
}
else
    builder.Services.AddTransient<IFileUploader, FileUploader>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ConcertProfile>();
    config.AddProfile<DataProfile>(); //Profile de concert desde repositorio
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if (app.Environment.IsDevelopment()) //este muestra el swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

//solo aqui se crean instancias
using (var scope = app.Services.CreateScope())
{
    //Ejecutamos la creacion del usuario admin y roles por default
    await UserDataSeeder.Seed(scope.ServiceProvider);
}
app.Run();

