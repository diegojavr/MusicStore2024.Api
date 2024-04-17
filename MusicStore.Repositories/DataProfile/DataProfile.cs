using MusicStore.Domain.Info;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MusicStore.Repositories.DataProfile
{
    public class DataProfile:Profile
    {
        public DataProfile()
        {
            //Este se pasa dentro de Repositorios xq aun consigue la información de Genre desde la BD
            CreateMap<Concert, ConcertInfo>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.Title, o => o.MapFrom(x => x.Title))
                .ForMember(d => d.Description, o => o.MapFrom(x => x.Description))
                .ForMember(d => d.Place, o => o.MapFrom(x => x.Place))
                .ForMember(d => d.UnitPrice, o => o.MapFrom(x => x.UnitPrice))
                .ForMember(d => d.Genre, o => o.MapFrom(x => x.Genre.Name))
                .ForMember(d => d.GenreId, o => o.MapFrom(x => x.GenreId))
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.DateEvent.ToString("yyyy-MM-dd")))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.DateEvent.ToString("HH:mm")))
                .ForMember(d => d.TicketsQuantity, o => o.MapFrom(x => x.TicketsQuantity))
                .ForMember(d => d.Finalized, o => o.MapFrom(x => x.Finalized));
        }
    }
}
