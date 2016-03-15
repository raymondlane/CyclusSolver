using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    public class Config
    {
        #region Ctor

        public Config()
        {
        }

        #endregion

        #region Properties

        [JsonProperty("Source", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Facilities.Source Source { get; set; }

        [JsonProperty("Sink", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Facilities.Sink Sink { get; set; }

        [JsonProperty("Enrichment", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Facilities.Enrichment Enrichment { get; set; }

        [JsonProperty("Separations", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Facilities.Separations Separations { get; set; }

        [JsonProperty("FuelFab", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Facilities.FuelFab FuelFab { get; set; }

        [JsonProperty("Reactor", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Facilities.Reactor Reactor { get; set; }

        [JsonProperty("Region", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CyclusNET.Region Region { get; set; }

        #endregion
    }
}

