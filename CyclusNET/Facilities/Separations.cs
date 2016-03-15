using System;
using Newtonsoft.Json;

namespace CyclusNET.Facilities
{
    /// <summary>
    /// Separations processes feed material into one or more streams containing specific 
    /// elements and/or nuclides. It uses mass-based efficiencies.
    /// User defined separations streams are specified as groups of component-efficiency 
    /// pairs where ‘component’ means either a particular element or a particular nuclide. 
    /// Each component’s paired efficiency represents the mass fraction of that component 
    /// in the feed that is separated into that stream. The efficiencies of a particular 
    /// component across all streams must sum up to less than or equal to one. If less 
    /// than one, the remainining material is sent to a waste inventory and (potentially) 
    /// traded away from there.
    /// The facility receives material into a feed inventory that it processes with a 
    /// specified throughput each time step. Each output stream has a corresponding output 
    /// inventory size/limit. If the facility is unable to reduce its stocks by trading and 
    /// hits this limit for any of its output streams, further processing/separations of 
    /// feed material will halt until room is again available in the output streams.
    /// </summary>
    public class Separations
    {
        #region Ctor

        public Separations()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the feed_ commods.
        /// </summary>
        /// <value>Ordered list of commodities on which to request feed material to separate.
        /// Order only matters for matching up with feed commodity preferences if specified.</value>
        [JsonProperty("feed_commods")]
        public FeedCommods Feed_Commods { get; set; }

        /// <summary>
        /// Gets or sets the feed_ commod_ prefs.
        /// </summary>
        /// <value>Feed commodity request preferences for each of the given feed commodities 
        /// (same order). If unspecified, default is to use 1.0 for all preferences.</value>
        [JsonProperty("feed_commod_prefs", NullValueHandling = NullValueHandling.Ignore)]
        public FeedCommodPrefs Feed_Commod_Prefs { get; set; }

        /// <summary>
        /// Gets or sets the feed_ recipe.
        /// </summary>
        /// <value>Name for recipe to be used in feed requests. Empty string results in use of a dummy recipe.</value>
        [JsonProperty("feed_recipe", NullValueHandling = NullValueHandling.Ignore)]
        public string Feed_Recipe { get; set; }

        /// <summary>
        /// Gets or sets the size of the feedbuf_.
        /// </summary>
        /// <value>Maximum amount of feed material to keep on hand.</value>
        [JsonProperty("feedbuf_size")]
        public double Feedbuf_Size { get; set; }

        /// <summary>
        /// Gets or sets the throughput.
        /// </summary>
        /// <value>Maximum quantity of feed material that can be processed per time step.</value>
        [JsonProperty("throughput")]
        public double Throughput { get; set; }

        /// <summary>
        /// Gets or sets the leftover_ commod.
        /// </summary>
        /// <value>Commodity on which to trade the leftover separated material stream. 
        /// This MUST NOT be the same as any commodity used to define the other 
        /// separations streams.</value>
        [JsonProperty("leftover_commod", NullValueHandling = NullValueHandling.Ignore)]
        public string Leftover_Commod { get; set; }

        /// <summary>
        /// Gets or sets the size of the leftoverbuf_.
        /// </summary>
        /// <value>Maximum amount of leftover separated material (not included in any 
        /// other stream) that can be stored. If full, the facility halts operation u
        /// ntil space becomes available.</value>
        [JsonProperty("leftoverbuf_size", NullValueHandling = NullValueHandling.Ignore)]
        public double? Leftoverbuf_Size { get; set;}

        /// <summary>
        /// Gets or sets the streams.
        /// </summary>
        /// <value>Output streams for separations. Each stream must have a unique name 
        /// identifying the commodity on which its material is traded, a max buffer 
        /// capacity in kg (neg values indicate infinite size), and a set of component 
        /// efficiencies. ‘comp’ is a component to be separated into the stream (e.g. U, 
        /// Pu, etc.) and ‘eff’ is the mass fraction of the component that is separated 
        /// from the feed into this output stream. If any stream buffer is full, the 
        /// facility halts operation until space becomes available. The sum total of all 
        /// component efficiencies across streams must be less than or equal to 1 (e.g. 
        /// sum of U efficiencies for all streams must be <= 1).</value>
        [JsonProperty("streams")]
        public Stream Streams { get; set; }

        #endregion

        #region Classes

        public class FeedCommods
        {
            public FeedCommods() {}

            public FeedCommods(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class FeedCommodPrefs
        {
            public FeedCommodPrefs() { }

            public FeedCommodPrefs(params double[] args)
            {
                Val = new double[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public double[] Val { get; set; }

        }

        public class Item_1
        {
            public Item_1() {}

            [JsonProperty("commod")]
            public string Commod { get; set; }

            [JsonProperty("info")]
            public Info Info { get; set; }

        }

        public class Info
        {
            public Info() {}

            [JsonProperty("buf_size")]
            public double Buf_Size { get; set; }

            [JsonProperty("efficiencies")]
            public Efficiencies Efficiencies { get; set; }
        }

        public class Efficiencies
        {
            public Efficiencies(Item_2[] items) 
            {
                Item = new Item_2[]{};
                Item = items;
            }

            [JsonProperty("item")]
            public Item_2[] Item { get; set;}
        }

        public class Item_2
        {
            public Item_2() {}

            [JsonProperty("comp")]
            public string Comp { get; set; }

            [JsonProperty("eff")]
            public double Eff { get; set; }
        }

        public class Stream
        {
            public Stream() {}

            public Stream(Item_1[] items)
            {
                Item = new Item_1[] {};
                Item = items;
            }

            [JsonProperty("item")]
            public Item_1[] Item { get; set; }
        }

        #endregion
    }
}

