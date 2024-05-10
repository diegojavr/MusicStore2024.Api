using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicStore.Dto.Response;
using MusicStore.Services.Interfaces;

namespace MusicStore.Api.Endpoints
{
    public static class ReportEndpoints
    {
        public static void MapReports(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/Reports")
                .WithTags("Reports");

            group.MapGet("/", async (ISaleService service, string dateStart, string dateEnd) =>
            {
                var response = new BaseResponseGeneric<ICollection<ReportDtoResponse>>();
                try
                {
                    response = await service.GetReportSaleAsync(DateTime.Parse(dateStart), DateTime.Parse(dateEnd));
                    if (response.Success)
                    {
                        return Results.Ok(response);
                    }

                    return Results.BadRequest(response);
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;

                }
                return Results.Ok(response);
            });
        }
    }
}
