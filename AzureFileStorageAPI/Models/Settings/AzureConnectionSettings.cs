using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFileStorage.API.Models.Settings
{
    public class AzureConnectionSettings : IAzureConnectionSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IAzureConnectionSettings
    {
        public string ConnectionString { get; set; }
    }
}
