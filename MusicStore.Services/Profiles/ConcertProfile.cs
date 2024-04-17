using AutoMapper;
using MusicStore.Domain;
using MusicStore.Domain.Info;
using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Profiles
{
    public class ConcertProfile:Profile
    {
        public ConcertProfile()
        {
            CreateMap<ConcertInfo, ConcertDtoResponse>();
        }
    }
}
