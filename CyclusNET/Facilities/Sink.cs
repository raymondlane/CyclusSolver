using System;
using Newtonsoft.Json;

namespace CyclusNET.Facilities
{
    /// <summary>
    /// A sink facility that accepts materials and products with a fixed throughput 
    /// (per time step) capacity and a lifetime capacity defined by a total inventory 
    /// size. The inventory size and throughput capacity both default to infinite. 
    /// If a recipe is provided, it will request material with that recipe. Requests 
    /// are made for any number of specified commodities.
    /// </summary>
    public class Sink
    {
        #region Ctor

        public Sink()
        {
        }



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the in_ commods.
        /// </summary>
        /// <value>Commodities that the sink facility accepts.</value>
        [JsonProperty("in_commods")]
        public InCommods In_Commods { get; set; }

        /// <summary>
        /// Gets or sets the name of the recipe_.
        /// </summary>
        /// <value>Name of recipe to use for material requests, where the default (empty string) is to accept everything.</value>
        [JsonProperty("recipe_name", NullValueHandling = NullValueHandling.Ignore)]
        public string Recipe_Name { get; set; }

        /// <summary>
        /// Gets or sets the size of the max_ inventory_.
        /// </summary>
        /// <value>Total maximum inventory size of sink facility.</value>
        [JsonProperty("max_inventory_size", NullValueHandling = NullValueHandling.Ignore)]
        public double? Max_Inventory_Size { get; set; }

        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        /// <value>Capacity the sink facility can accept at each time step.</value>
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public double? Capacity { get; set; }

        #endregion

        #region Classes

        public class InCommods
        {
            public InCommods()
            {
            }

            public InCommods(params string[] vals)
            {
                Val = new string[] { };
                Val = vals;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }

        }

        #endregion
    }
}

