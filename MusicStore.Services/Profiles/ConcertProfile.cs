using AutoMapper;
using MusicStore.Domain;
using MusicStore.Domain.Info;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<ConcertInfo, ConcertDtoResponse>();

            CreateMap<Concert, ConcertDtoResponse>()
                .ForMember(d => d.Genre, o => o.MapFrom(_ => _.Genre.Name))
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.DateEvent.ToString("yyyy-MM-dd")))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.DateEvent.ToString("HH:mm")));

            CreateMap<ConcertDtoRequest, Concert>()
                .ForMember(d => d.GenreId, o => o.MapFrom(_ => _.IdGenre))
                .ForMember(d => d.DateEvent, o => o.MapFrom(_ => $"{_.DateEvent} {_.TimeEvent}")); //Concatenando fecha y hora

        }
    }
}
