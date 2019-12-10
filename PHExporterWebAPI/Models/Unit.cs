using System;
using System.Collections.Generic;
using System.Text;

namespace PHExporterWebAPI.Models
{
    public class Unit
    {
        public string Agency { get; set; }
        public string AssignedEventId { get; set; }
        public string DispatchGroup { get; set; }
        public string UnitId { get; set; }
        public string CurrentStatus { get; set; }
        public string LastStatusChange { get; set; }
        public string OutType { get; set; }
        public string UnitType { get; set; }
        

    }
}
