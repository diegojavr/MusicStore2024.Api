using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using MusicStore.Persistence;
using MusicStore.Repositories;

var builder = WebApplication.CreateBuilder(args); //Crea puerto de desarrollo para la aplicacion web

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Creamos conexion con la base de datos
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreDb"));
    
});
//builder.Services.AddSingleton<IGenreRepository,GenreRepository>(); //Solo localmente
builder.Services.AddTransient<IGenreRepository, GenreRepository>(); //Con base de datos

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
app.MapGet("/api/Genres", (IGenreRepository repository) => Results.Ok(repository.ListAll()));
app.MapPost("/api/Genres", (IGenreRepository repository, Genre request) =>
{
    repository.Add(request);
    return Results.Ok();

});

app.MapGet("/api/Genres/{id:int}", (IGenreRepository repository, int id) => Results.Ok(repository.GetById(id)));
app.MapPut("/api/Genres/{id:int}", (IGenreRepository repository, int id, Genre request) =>
{
    repository.Update(id, request);
    return Results.Ok();
});

app.MapDelete("/api/Genre/{id:int}", (IGenreRepository repository, int id) =>
{
    repository.Delete(id);
    return Results.Ok();
});

app.Run();

