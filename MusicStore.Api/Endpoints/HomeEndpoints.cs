using MusicStore.Services.Interfaces;

namespace MusicStore.Api.Endpoints
{
    public static class HomeEndpoints
    {
        public static void MapHomeEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/Home", async (IConcertService concertService, IGenreService genreService) =>
            {
                //Obtenemos los listados de ambos servicios
                var concerts = await concertService.ListAsync(string.Empty, 1, 10);
                var genres = await genreService.ListAsync();

                //Retornamos como Ok el listado de Concerts y Genres
                return Results.Ok(new
                {
                    Concerts = concerts.Data,
                    Genres = genres.Data,
                    Success = true
                });
            }).WithDescription("Permite mostrar los endpoints de la pagina principal")
            .WithOpenApi();
        }
    }
}
