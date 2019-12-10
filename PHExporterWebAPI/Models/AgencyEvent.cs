using System;
using System.Collections.Generic;
using System.Text;

namespace PHExporterWebAPI.Models
{
    public class AgencyEvent
    {
        public string Agency { get; set; }
        public string AgencyEventId { get; set; }
        public string DispatchGroup { get; set; }
        public int EventStatusCode { get; set; }
        public int Priority { get; set; }
    }
}
