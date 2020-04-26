using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Back.API.Configuration
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        public string DBConnectionString { get; set; }
    }
}
