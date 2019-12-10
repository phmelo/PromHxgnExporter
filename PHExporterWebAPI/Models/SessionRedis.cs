using System;
using System.Collections.Generic;
using System.Text;

namespace PHExporterWebAPI.Models
{
    public class SessionRedis
    {
        public SessionRedisEntries Entries { get; set; }
        public string Id { get; set; }
    }

    public class SessionRedisEntries
    {
        public string locale { get; set; }
        public string localeconfig { get; set; }
        public string SessionGuid { get; set; }
        public string employeeId { get; set; }
        public string allowedcmds { get; set; }
        public string Role { get; set; }
        public string dataaccess { get; set; }
        public string LicenseName { get; set; }
        public string GroupId { get; set; }
        public int SessionCount { get; set; }
        public string positionId { get; set; }
        public string positionIds { get; set; }

    }
}
