using Prometheus.Client.Contracts;

namespace Prometheus.Client.MetricsWriter
{
    public static class MetricsWriterExtensions
    {
        public static IMetricsWriter WriteSample(
            this IMetricsWriter writer,
            double value,
            string suffix = "",
            CLabelPair[] labels = null,
            long? timestamp = null)
        {
            var sampleWriter = writer.StartSample(suffix);
            if ((labels!=null) && (labels.Length > 0))
            {
                var labelWriter = sampleWriter.StartLabels();
                labelWriter.WriteLabels(labels);
                labelWriter.EndLabels();
            }
            sampleWriter.WriteValue(value);
            if (timestamp.HasValue)
            {
                sampleWriter.WriteTimestamp(timestamp.Value);
            }

            return writer;
        }

        public static IMetricsWriter WriteMetricHeader(
            this IMetricsWriter writer,
            string metricName,
            MetricType metricType,
            string help = "")
        {
            writer.StartMetric(metricName);
            if (!string.IsNullOrEmpty(help))
            {
                writer.WriteHelp(help);
            }

            writer.WriteType(metricType);
            return writer;
        }

        public static ILabelWriter WriteLabels(
            this ILabelWriter labelWriter,
            CLabelPair[] labels
            )
        {
            for (var i = 0; i < labels.Length; i++)
            {
                labelWriter.WriteLabel(labels[i].Name, labels[i].Value);
            }

            return labelWriter;
        }
    }
}
