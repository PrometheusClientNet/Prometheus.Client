﻿using System;
using System.Collections.Generic;
using Prometheus.Client.Collectors.Abstractions;
using Prometheus.Client.SummaryImpl;

namespace Prometheus.Client
{
    public class MetricFactory
    {
        private readonly ICollectorRegistry _registry;

        public MetricFactory(ICollectorRegistry registry)
        {
            _registry = registry;
        }

        /// <summary>
        ///     Create Counter
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="help">Help text</param>
        /// <param name="labelNames">Array of label names</param>
        public Counter CreateCounter(string name, string help, params string[] labelNames)
        {
            return CreateCounter(name, help, false, labelNames);
        }

        /// <summary>
        ///     Create  Counter
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="help">Help text</param>
        /// <param name="includeTimestamp">Include unix timestamp for metric</param>
        /// <param name="labelNames">Array of label names</param>
        public Counter CreateCounter(string name, string help, bool includeTimestamp, params string[] labelNames)
        {
            var metric = new Counter(name, help, includeTimestamp, labelNames);
            return (Counter) _registry.GetOrAdd(metric);
        }

        public Gauge CreateGauge(string name, string help, params string[] labelNames)
        {
            var metric = new Gauge(name, help, labelNames);
            return (Gauge) _registry.GetOrAdd(metric);
        }

        public Summary CreateSummary(string name, string help, params string[] labelNames)
        {
            var metric = new Summary(name, help, labelNames);
            return (Summary) _registry.GetOrAdd(metric);
        }

        public Summary CreateSummary(string name, string help, string[] labelNames, IList<QuantileEpsilonPair> objectives, TimeSpan maxAge, int? ageBuckets,
            int? bufCap)
        {
            var metric = new Summary(name, help, labelNames, objectives, maxAge, ageBuckets, bufCap);
            return (Summary) _registry.GetOrAdd(metric);
        }

        public Histogram CreateHistogram(string name, string help, params string[] labelNames)
        {
            return CreateHistogram(name, help, null, labelNames);
        }

        public Histogram CreateHistogram(string name, string help, double[] buckets, params string[] labelNames)
        {
            var metric = new Histogram(name, help, labelNames, buckets);
            return (Histogram) _registry.GetOrAdd(metric);
        }
    }
}
