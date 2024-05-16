using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Dto.Request
{
    public record SaleDtoRequest
    {
        public int ConcertId { get; init; }
        public short TicketsQuantity { get; init; }

    }
}
