using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public static class Utilities
    {
        public static int GetTotalPages(int total, int rows)
        {
            var totalPages = total/ rows;
            if (totalPages % rows > 0)
            {
                totalPages++;
            }

            return totalPages;
        }
    }
}
