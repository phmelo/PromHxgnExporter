using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PHExporterWebAPI.Models;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PHExporterWebAPI;

namespace PHExporterWebAPI.Services
{
    public class CustomMetricsCollector : BackgroundService
    {
        
        private static readonly Gauge PHGouge = Metrics.CreateGauge("PH_TEST", "Random Number",
            new GaugeConfiguration
            {
                SuppressInitialValue = true
            });

        private static readonly Gauge AgencyEvent = Metrics.CreateGauge("AgencyEvent", "Ocorrências abertas",
            new GaugeConfiguration
            {
                LabelNames = new[] { "Total" }
            });

        private static readonly Gauge Unit = Metrics.CreateGauge("Unit", "Viaturas",
            new GaugeConfiguration
            {
                LabelNames = new[] { "Total" }
            });

        private static readonly Gauge Sessions = Metrics.CreateGauge("Session", "Usuarios Logados",
            new GaugeConfiguration
            {
                LabelNames = new[] { "Total" }
            });

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var rng = new Random();
            while (true)
            {
                watch.Restart();
                PHGouge.Set(rng.Next(0, 100));

                RunPrometheusExporter();

                watch.Stop();
                Console.WriteLine("Tempo de Execução(ms):" + watch.ElapsedMilliseconds);

                await Task.Delay(5000);
            }
        }

        private static void RunPrometheusExporter()
        {
            Models.MySettingsConfig mySettingsConfig = new Models.MySettingsConfig();
            mySettingsConfig.SQLConn = "Server=10.40.11.83;Database=CAD;User Id=phmelo;Password=moHaa231";
            mySettingsConfig.RedisConn = "10.40.11.74:6379,10.40.11.75:6379,10.40.11.76:6379,password=redis";
            mySettingsConfig.RedisTest = "10.40.11.43:6379,password=redis";
            mySettingsConfig.RedisBus = "10.40.11.43:6389,password=redis";
            mySettingsConfig.port = 1237;


            int BusResult = Data.Redis.GetBusLen(mySettingsConfig.RedisBus);


            var AgencyEventSQLResult = Data.SqlDB.GetEventData(mySettingsConfig.SQLConn);
            var AgencyEventRedisResult = Data.Redis.GetEventData(mySettingsConfig.RedisConn);
            int MinutesWithoutCallId = Data.SqlDB.GetEventCreatedWithoutCallerIdReceived(mySettingsConfig.SQLConn);
            var UnitSQLResult = Data.SqlDB.GetUnitData(mySettingsConfig.SQLConn);
            var UnitRedisResult = Data.Redis.GetUnitData("cad-units", mySettingsConfig.RedisConn);

            double SqlTotEvt = 0;
            double SqlTotUnit = 0;
            double RdsTotEvt = AgencyEventRedisResult.Count;
            double RdsTotUnit = UnitRedisResult.Count;
            double AgencyEventStatusDiffTot = 0;
            double AgencyEventDgroupDiffTot = 0;
            double UnitStatusDiffTot = 0;
            double UnitDgroupDiffTot = 0;
            double AgencyEventQtdDiffTot = 0;
            double UnitQtdDiffTot = 0;
            double AnyDiffTot = 0;


            foreach (var ev in AgencyEventSQLResult)
            {
                Models.AgencyEvent ValueResult = null;
                if (AgencyEventRedisResult.TryGetValue(ev.AgencyEventId, out ValueResult))
                {
                    if (ev.DispatchGroup != ValueResult.DispatchGroup)
                    {
                        AgencyEventDgroupDiffTot++;
                    }
                    if (ev.EventStatusCode != ValueResult.EventStatusCode)
                    {
                        AgencyEventStatusDiffTot++;
                    }
                }
                SqlTotEvt++;
            }

            foreach (var u in UnitSQLResult)
            {
                Models.Unit ValueResult = null;
                if (UnitRedisResult.TryGetValue(u.UnitId, out ValueResult))
                {
                    if (u.DispatchGroup != ValueResult.DispatchGroup)
                    {
                        UnitDgroupDiffTot++;
                    }
                    if (u.CurrentStatus != ValueResult.CurrentStatus)
                    {
                        UnitStatusDiffTot++;
                    }
                }
                SqlTotUnit++;
            }

            AgencyEventQtdDiffTot = Math.Abs(SqlTotEvt - RdsTotEvt);
            UnitQtdDiffTot = Math.Abs(SqlTotUnit - RdsTotUnit);
            AnyDiffTot = AgencyEventQtdDiffTot + AgencyEventDgroupDiffTot;

            AgencyEvent.WithLabels("RedisTotal").Set(RdsTotEvt);
            AgencyEvent.WithLabels("SQLTotal").Set(SqlTotEvt);
            AgencyEvent.WithLabels("DgroupDiff").Set(AgencyEventDgroupDiffTot);
            AgencyEvent.WithLabels("StatusDiff").Set(AgencyEventStatusDiffTot);
            AgencyEvent.WithLabels("EventDiff").Set(AgencyEventQtdDiffTot);
            AgencyEvent.WithLabels("AnyDiff").Set(AnyDiffTot);
            AgencyEvent.WithLabels("MinutesWithoutCallId").Set(MinutesWithoutCallId);

            Unit.WithLabels("RedisTotal").Set(RdsTotUnit);
            Unit.WithLabels("SQLTotal").Set(SqlTotUnit);
            Unit.WithLabels("DgroupDiff").Set(UnitDgroupDiffTot);
            Unit.WithLabels("StatusDiff").Set(UnitStatusDiffTot);
            Unit.WithLabels("UnitDiff").Set(UnitQtdDiffTot);


            var SessionRedisResult = Data.Redis.GetSessiontData(mySettingsConfig.RedisConn);
            Sessions.WithLabels("Online").Set(SessionRedisResult.Count);

            var test = new SortedSet<string>();
            int totDuplicatedSessions = 0;
            foreach (var session in SessionRedisResult)
            {
                if (!string.IsNullOrEmpty(session.Value.positionId))
                {
                    Console.WriteLine("Key:" + session.Key + "; Empid:" + session.Value.employeeId + "; PositionID:" + session.Value.positionId + ";");
                    Console.WriteLine(session.Value.ToString());
                }
                if (session.Value.SessionCount != 0)
                {
                    totDuplicatedSessions++;
                    //Console.WriteLine(session.Value.employeeId + ": " + session.Value.positionIds);
                }
            }

            Console.WriteLine(test);

            Sessions.WithLabels("Duplicated").Set(totDuplicatedSessions);
            
        }
    }
}
