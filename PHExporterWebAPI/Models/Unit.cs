using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHExporterWebAPI.Models
{
    public class Unit
    {
        public string Agency { get; set; }
        public string CustomData { get; set; }
        public string DispatchGroup { get; set; }
        public string UnitId { get; set; }
        public string UnitType { get; set; }
        public string CurrentLocationText { get; set; }
        public string AssignedEventId { get; set; }
        public string[] DispatchAssignedEventIds { get; set; }
        //public string[] DispatchAssignedUnitStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string LocalizedStatus { get; set; }
        public string UnitStatusMnemonic { get; set; }
        public string Zone { get; set; }
        public string StationId { get; set; }
        public string StagingAreaId { get; set; }
        public string LastStatusChange { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Alarm { get; set; }
        public string Emergency { get; set; }
        public string[] EmployeeIds { get; set; }
        public string EmployeeNames { get; set; }
        public string Beat { get; set; }
        public string OutType { get; set; }

    }
}
