using Microsoft.Extensions.Hosting;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PHExporterWebAPI
{
    public class CustomMetricsCollector : BackgroundService
    {
        private static readonly Gauge PHGouge = Metrics.CreateGauge("PH_TEST", "Random Number",
            new GaugeConfiguration
            {
                SuppressInitialValue = true
            });

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rng = new Random();
            while (true)
            {
                PHGouge.Set(rng.Next(0, 100));
                await Task.Delay(5000);
            }
        }
    }
}
