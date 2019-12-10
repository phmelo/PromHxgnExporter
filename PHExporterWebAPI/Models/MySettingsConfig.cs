using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHExporterWebAPI.Models
{
    public class MySettingsConfig
    {
        public string SQLConn { get; set; }
        public string RedisConn { get; set; }
        public string RedisBus { get; set; }
        public string RedisTest { get; set; }
        public string AccountName { get; set; }
        public string ApiKey { get; set; }
        public string RedisPass { get; set; }
        public string RedisUser { get; set; }
        public int port { get; set; }

        
    }
}
