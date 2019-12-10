using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prometheus;

namespace PHExporterWebAPI.Prometheus
{
    public static class AgencyEvent
    {
        private static readonly Gauge AgencyEventGauge = Metrics.CreateGauge("AgencyEvent", "Ocorrências abertas",
            new GaugeConfiguration
            {
                LabelNames = new[] { "Total" }
            });

        public static void Run()
        {
            var AgencyEventSQLResult = Data.SqlDB.GetEventData("mySettingsConfig.SQLConn");
            var AgencyEventRedisResult = Data.Redis.GetEventData("mySettingsConfig.RedisConn");
            int MinutesWithoutCallId = Data.SqlDB.GetEventCreatedWithoutCallerIdReceived("mySettingsConfig.SQLConn");


        }
    }
}
