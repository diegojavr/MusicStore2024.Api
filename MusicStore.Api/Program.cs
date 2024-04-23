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
    //Politicas de contraseña
    policies.Password.RequireNonAlphanumeric = false;
    policies.Password.RequireLowercase = false;
    policies.Password.RequireUppercase = false;
    policies.Password.RequireDigit = true;
    policies.Password.RequiredLength = 6; //tamaño de 6

    policies.User.RequireUniqueEmail = true; //un unico usuario para no tener duplicados

    //Politicas de bloqueo de cuentas
    policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); //tiempo de 10 minutos para bloqueo de cuenta
    policies.Lockout.MaxFailedAccessAttempts = 5;
    policies.Lockout.AllowedForNewUsers = true; //nuevos usuarios tambien se bloquean de contraseña erronea

}).AddEntityFrameworkStores<MusicStoreDbContext>()
.AddDefaultTokenProviders();


//builder.Services.AddSingleton<IGenreRepository,GenreRepository>(); //Solo localmente
builder.Services.AddTransient<IGenreRepository, GenreRepository>(); //Con base de datos
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IConcertRepository, ConcertRepository>(); //Con base de datos
builder.Services.AddTransient<IConcertService, ConcertService>();

//Si consigue el valor core.windows.net utiliza el uploader a Azure, sino será local
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

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //este muestra el swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



//Aqui mapeamos los endpoints de la aplicaciones
//app.MapGet("/api/Genres", async (IGenreRepository repository) =>
//  Results.Ok(await repository.ListAsync()));

//app.MapPost("/api/Genres", async (IGenreRepository repository, Genre request) =>
//{
//    var response = await repository.AddAsync(request);
//    return Results.Ok(new
//    {
//        Id = response
//    });

//});

//app.MapGet("/api/Genres/{id:int}", async (IGenreRepository repository, int id) =>
//{
//    var registro = await repository.FindByIdAsync(id);
//    return registro is null ? Results.NotFound() : Results.Ok(registro);
//});
//app.MapPut("/api/Genres/{id:int}", async (IGenreRepository repository, int id, Genre request) =>
//{
//    var registro = await repository.FindByIdAsync(id);
//    if (registro is null)
//        return Results.NotFound();
//    registro.Name = request.Name;
//    await repository.UpdateAsync(registro);
//    return Results.Ok();
//});

//app.MapDelete("/api/Genre/{id:int}", async (IGenreRepository repository, int id) =>
//{

//    await repository.DeleteAsync(id);
//    return Results.Ok();
//});

app.MapControllers();
app.Run();

