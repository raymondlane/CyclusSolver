using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// In Cyclus, a commodity is used to define how agents can interact. 
    /// Commodities are simply used to define which resources a facility 
    /// can request and/or offer. The list of commodities in a problem is 
    /// defined exclusively by the commodities that are used in the 
    /// definition of facility prototypes.
    /// The commodity input block is only used to indicate a non-default 
    /// priority for a particular commodity in the dynamic resource exchange 
    /// solution.
    /// 
    /// </summary>
    public class Commodity
    {
        #region Ctor

        public Commodity(string name, float priority)
        {
            Name = name;
            Solution_Priority = priority;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The the unique name for this commodity.</value>
        [JsonProperty("name")]
        public string Name { get; set;}

        /// <summary>
        /// Gets or sets the solution_ priority.
        /// </summary>
        /// <value>A number that defines the relative priority 
        /// for resolution in the dynamic resource exchange.</value>
        [JsonProperty("solution_priority")]
        public float Solution_Priority { get; set; }

        #endregion
    }
}

