using System;
using Newtonsoft.Json;

namespace CyclusNET.Facilities
{
    /// <summary>
    /// The Enrichment facility is a simple agent that enriches natural uranium in a 
    /// Cyclus simulation. It does not explicitly compute the physical enrichment process, 
    /// rather it calculates the SWU required to convert an source uranium recipe 
    /// (i.e. natural uranium) into a requested enriched recipe (i.e. 4% enriched uranium), 
    /// given the natural uranium inventory constraint and its SWU capacity constraint.
    /// The Enrichment facility requests an input commodity and associated recipe whose 
    /// quantity is its remaining inventory capacity. All facilities trading the same input 
    /// commodity (even with different recipes) will offer materials for trade. The Enrichment 
    /// facility accepts any input materials with enrichments less than its tails assay, as 
    /// long as some U235 is present, and preference increases with U235 content. If no U235 
    /// is present in the offered material, the trade preference is set to -1 and the material 
    /// is not accepted. Any material components other than U235 and U238 are sent directly 
    /// to the tails buffer.
    /// The Enrichment facility will bid on any request for its output commodity up to the 
    /// maximum allowed enrichment (if not specified, default is 100%) It bids on either the 
    /// request quantity, or the maximum quanity allowed by its SWU constraint or natural 
    /// uranium inventory, whichever is lower. If multiple output commodities with different 
    /// enrichment levels are requested and the facility does not have the SWU or quantity 
    /// capacity to meet all requests, the requests are fully, then partially filled in 
    /// unspecified but repeatable order.
    /// </summary>
    public class Enrichment
    {
        #region Ctor

        public Enrichment()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the feed_ commod.
        /// </summary>
        /// <value>Feed commodity that the enrichment facility accepts.</value>
        [JsonProperty("feed_commod")]
        public string Feed_Commod { get; set;}

        /// <summary>
        /// Gets or sets the feed_ recipe.
        /// </summary>
        /// <value>Recipe for enrichment facility feed commodity.</value>
        [JsonProperty("feed_recipe")]
        public string Feed_Recipe { get; set; }

        /// <summary>
        /// Gets or sets the product_ commod.
        /// </summary>
        /// <value>Product commodity that the enrichment facility generates.</value>
        [JsonProperty("product_commod")]
        public string Product_Commod { get; set; }

        /// <summary>
        /// Gets or sets the tails_ commod.
        /// </summary>
        /// <value>Tails commodity supplied by enrichment facility.</value>
        [JsonProperty("tails_commod")]
        public string Tails_Commod { get; set; }

        /// <summary>
        /// Gets or sets the tails_ assay.
        /// </summary>
        /// <value>Tails assay from the enrichment process.</value>
        [JsonProperty("tails_assay", NullValueHandling = NullValueHandling.Ignore)]
        public double? Tails_Assay { get; set; }

        /// <summary>
        /// Gets or sets the initial_ feed.
        /// </summary>
        /// <value>Amount of natural uranium stored at the enrichment facility at the beginning of the simulation (kg).</value>
        [JsonProperty("initial_feed", NullValueHandling = NullValueHandling.Ignore)]
        public double? Initial_Feed { get; set; }

        /// <summary>
        /// Gets or sets the max_ feed_ inventory.
        /// </summary>
        /// <value>Maximum total inventory of natural uranium in the enrichment facility (kg).</value>
        [JsonProperty("max_feed_inventory", NullValueHandling = NullValueHandling.Ignore)]
        public double? Max_Feed_Inventory { get; set; }

        /// <summary>
        /// Gets or sets the max enrich.
        /// </summary>
        /// <value>The maximum allowed weight fraction of U235 in product.</value>
        [JsonProperty("max_enrich", NullValueHandling = NullValueHandling.Ignore)]
        public double? MaxEnrich { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CyclusNET.Facilities.Enrichment"/> order_ prefs.
        /// </summary>
        /// <value><c>true</c> if preference ordering for input material is desired so that EF chooses higher U235 content first; otherwise, <c>false</c>.</value>
        [JsonProperty("order_prefs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Order_Prefs { get; set; }

        /// <summary>
        /// Gets or sets the SWU_Capacity.
        /// </summary>
        /// <value>Separative work unit (SWU) capacity of enrichment facility (kgSWU/timestep).</value>
        [JsonProperty("swu_capacity", NullValueHandling = NullValueHandling.Ignore)]
        public double? SWU_Capacity { get; set; }

        #endregion
    }
}

