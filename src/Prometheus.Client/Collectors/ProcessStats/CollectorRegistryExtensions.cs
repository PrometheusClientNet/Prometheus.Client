using System.Diagnostics;

namespace Prometheus.Client.Collectors.ProcessStats
{
    public static class CollectorRegistryExtensions
    {
        public static ICollectorRegistry UseProcessStats(this ICollectorRegistry registry)
        {
            registry.Add(new ProcessCollector(Process.GetCurrentProcess()));

            return registry;
        }
    }
}
