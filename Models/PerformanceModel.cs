﻿using Newtonsoft.Json;

namespace MemberLoginSSRS.Models
{
    public class PerformanceModel
    {
        [JsonProperty("machineName")]
        public string MachineName { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("counterName")]
        public string CounterName { get; set; }

        [JsonProperty("instanceName")]
        public string InstanceName { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}