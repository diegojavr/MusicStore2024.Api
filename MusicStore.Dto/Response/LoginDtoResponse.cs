using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Dto.Response
{
    public class LoginDtoResponse :BaseResponse
    {
        public string FullName { get; set; } = default!;
        public ICollection<string> Roles { get; set;} = default!;
        public string Token { get; set; } = default!;

    }
}
