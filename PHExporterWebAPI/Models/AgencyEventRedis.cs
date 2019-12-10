using System;
using System.Collections.Generic;
using System.Text;

namespace PHExporterWebAPI.Models
{
    public class AgencyEventRedis
    {
        public string CustomData { get; set; }
        public string ItemType { get; set; }
        public string Agency { get; set; }
        public string DispatchGroup { get; set; }
        public string Priority { get; set; }
        public string AgencyEventId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string TypeLiteral { get; set; }
        public string DispatchAssignedUnitId { get; set; }
        public string LocationFormmatted { get; set; }
        public int EventStatusCode { get; set; }
        public string EventStatusMnemonic { get; set; }
        public string LastStatusChange { get; set; }
        public string CrossStreets { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SubType { get; set; }
        public string SubTypeLiteral { get; set; }
        public string Alarm { get; set; }
        public string RelatedInformation { get; set; }
        public string CommonEventId { get; set; }
        public string[] AssignedUnitIds { get; set; }
        public string Recommendation { get; set; }
        public string CaseNumberId { get; set; }
        public string CaseNumbers { get; set; }
        public string IsClosed { get; set; }
        public string AlarmLevel { get; set; }
    }
}
