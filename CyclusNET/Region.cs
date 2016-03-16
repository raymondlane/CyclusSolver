using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// Every agent that participates in a Cyclus simulation represents either a facility, 
    /// an institution or a region. Each region block defines an agent that acts as a region 
    /// in the simulation. In contrast to the facility block that defines a prototype, 
    /// this block defines an agent.
    /// </summary>
    public class Region
    {
        #region Ctor

        public Region()
        {
            Name = "SingleRegion";
            Institution = new CyclusNET.Institution[1];
            Institution[0] = new CyclusNET.Institution();
            Config = new RegionConfig();
            Config.NullRegion = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The archetype-specific configuration.</value>
        [JsonProperty("config")]
        public RegionConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the institution.
        /// </summary>
        /// <value>An institution agent operating in this region.</value>
        [JsonProperty("institution")]
        public Institution[] Institution { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A name for the prototype.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the lifetime.
        /// </summary>
        /// <value>A non-negative integer indicating the number of time steps that 
        /// this region agent will be active in the simulation.</value>
        [JsonProperty("lifetime", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lifetime { get; set; }

        #endregion
    }

    public class RegionConfig
    {
        #region Ctor

        public RegionConfig()
        {
        }

        #endregion

        #region Properties

        [JsonProperty("NullRegion", NullValueHandling = NullValueHandling.Include)]
        public CyclusNET.Regions.NullRegion NullRegion { get; set;}



        #endregion
    }
}

