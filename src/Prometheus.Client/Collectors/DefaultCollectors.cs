using Prometheus.Client.Collectors.DotNetStats;
using Prometheus.Client.Collectors.ProcessStats;

namespace Prometheus.Client.Collectors
{
    public static class DefaultCollectors
    {
        public static ICollectorRegistry UseDefaultCollectors(this ICollectorRegistry registry)
        {
            return UseDefaultCollectors(registry, "");
        }

        public static ICollectorRegistry UseDefaultCollectors(this ICollectorRegistry registry, string prefixName)
        {
            registry.UseDotNetStats(prefixName);
            registry.UseProcessStats(prefixName);

            return registry;
        }
    }
}
