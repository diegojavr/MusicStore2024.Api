﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Info
{
    public class ReportInfo
    {
        public string ConcertName { get; set; } = default!;
        public decimal Total { get; set; }
    }
}
