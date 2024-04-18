using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Configuration
{
    public class AppConfig
    {


        public Storageconfiguration StorageConfiguration { get; set; } = default!;


    }
    public class Storageconfiguration
    {
        public string PublicUrl { get; set; } = default!;
        public string Path { get; set; } = default!;
    }
}
