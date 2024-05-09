using AutoMapper;
using MusicStore.Domain;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Profiles
{
    public class SaleProfile :Profile
    {
        public static CultureInfo Culture = new("es-GT");
        public SaleProfile()
        {
            //Mapeo para SaleService.AddAsync
            CreateMap<SaleDtoRequest, Sale>()
                .ForMember(d => d.ConcertId, o => o.MapFrom(_ => _.ConcertId))
                .ForMember(d => d.Quantity, o => o.MapFrom(_ => _.TicketsQuantity));



            //Mapeo para SaleService.FindByIdAsync
            CreateMap<Sale, SaleDtoResponse>()
                .ForMember(d => d.SaleID, o => o.MapFrom(_ => _.Id))
                .ForMember(d => d.DateEvent, o => o.MapFrom(_ => _.Concert.DateEvent.ToString("D", Culture))) //esto me devuelve la fecha en formato largo
                .ForMember(d => d.TimeEvent, o => o.MapFrom(_ => _.Concert.DateEvent.ToString("T", Culture)))
                .ForMember(d => d.Genre, o => o.MapFrom(_ => _.Concert.Genre.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(_ => _.Concert.ImageUrl))
                .ForMember(d => d.Title, o => o.MapFrom(_ => _.Concert.Title))
                .ForMember(d => d.FullName, o => o.MapFrom(_ => _.Customer.FullName))
                .ForMember(d => d.SaleDate, o => o.MapFrom(_ => _.SaleDate.ToString("dd/MM/yyyy HH:mm",Culture)));



        }
    }
}
