﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Dto.Request
{
    public class ConcertDtoRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int IdGenre { get; set; }
        public string DateEvent { get; set; } = default!;
        public string TimeEvent { get; set; } = default!;
        public string Place { get; set; }= default!;
        public decimal UnitPrice { get; set; }
        public int TicketsQuantity { get; set; }
        public string? Base64Image { get; set; } //imagen
        public string? FileName { get; set; }
    }
}
