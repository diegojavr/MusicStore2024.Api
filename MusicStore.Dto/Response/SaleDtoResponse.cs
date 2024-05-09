using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Dto.Response
{
    public class SaleDtoResponse
    {
        public int SaleID { get; set; }
        public string DateEvent { get; set; } = default!;
        public string TimeEvent { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = default!;
        public string OperationNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int Quantity { get; set; }
        public string SaleDate { get; set; } = default!;
        public short Total { get; set; }
    }
}
