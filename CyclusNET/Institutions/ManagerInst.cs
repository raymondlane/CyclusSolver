using System;

namespace CyclusNET.Institutions
{
    /// <summary>
    /// An institution that owns and operates a manually 
    /// entered list of facilities in the input file.
    /// </summary>
    public class ManagerInst
    {
        #region Ctor

        public ManagerInst()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the prototypes.
        /// </summary>
        /// <value>A set of facility prototypes that this institution can build. 
        /// All prototypes in this list must be based on an archetype that 
        /// implements the cyclus::toolkit::CommodityProducer interface.</value>
        [JsonProperty("prototypes")]
        public Prototype Prototypes { get; set; }

        #endregion

    }
}

