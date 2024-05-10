using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain
{
    public class Concert : EntityBase
    {
        public required string Title { get; set; }
        public string Description { get; set; } = default!;
        public string Place { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Genre Genre { get; set; } = default!;
        public int GenreId { get; set; }//Entity Framework ya la define como un nuevo id dentro de concert en la BD
        public DateTime DateEvent { get; set; }
        public string? ImageUrl { get; set; }
        public int TicketsQuantity { get; set; }
        public bool Finalized { get; set; }
    }
}
