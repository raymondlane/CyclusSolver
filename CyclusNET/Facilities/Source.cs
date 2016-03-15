using System;
using Newtonsoft.Json;

namespace CyclusNET.Facilities
{
    /// <summary>
    /// This facility acts as a source of material with a fixed throughput (per time step) capacity and a 
    /// lifetime capacity defined by a total inventory size. It offers its material as a single commodity. 
    /// If a composition recipe is specified, it provides that single material composition to requesters. 
    /// If unspecified, the source provides materials with the exact requested compositions. The inventory 
    /// size and throughput both default to infinite. Supplies material results in corresponding decrease 
    /// in inventory, and when the inventory size reaches zero, the source can provide no more material.
    /// </summary>
    public class Source
    {
        #region Ctor

        public Source() { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the out commod.
        /// </summary>
        /// <value>Output commodity on which the source offers material.</value>
        [JsonProperty("outcommod")]
        public string OutCommod { get; set; }

        /// <summary>
        /// Gets or sets the out recipe.
        /// </summary>
        /// <value>Name of composition recipe that this source provides regardless of requested composition. 
        /// If empty, source creates and provides whatever compositions are requested.</value>
        [JsonProperty("outrecipe", NullValueHandling = NullValueHandling.Ignore)]
        public string OutRecipe { get; set; }

        /// <summary>
        /// Gets or sets the size of the inventory_.
        /// </summary>
        /// <value>Total amount of material this source has remaining. Every trade decreases this value by the
        ///  supplied material quantity. When it reaches zero, the source cannot provide any more material.</value>
        [JsonProperty("inventory_size", NullValueHandling = NullValueHandling.Ignore)]
        public double? Inventory_Size { get; set; }

        /// <summary>
        /// Gets or sets the throughput.
        /// </summary>
        /// <value>The amount of commodity that can be supplied at each time step.</value>
        [JsonProperty("throughput", NullValueHandling = NullValueHandling.Ignore)]
        public double? Throughput { get; set; }

        #endregion
    }
}

