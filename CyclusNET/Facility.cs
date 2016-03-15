using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// Every agent that participates in a Cyclus simulation represents either a facility, 
    /// an institution or a region. Each facility block defines a single prototype for an 
    /// agent that acts as a facility in the simulation.    /// 
    /// </summary>
    public class Facility
    {
        #region Ctor

        public Facility()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>the archetype-specific configuration.</value>
        [JsonProperty("config")]
        public Config Config { get; set; }

        /// <summary>
        /// Gets or sets the lifetime.
        /// </summary>
        /// <value>A non-negative integer indicating the number of time steps 
        /// that an agent of this prototype will be active in the simulation.</value>
        [JsonProperty("lifetime", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lifetime { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A name for the prototype.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion

        #region Methods

        public static Facility FacilityFactory(FacilityType type)
        {
            var f = new Facility();
            var c = new Config();
            switch (type)
            {
                case FacilityType.Source:
                    f.Name = "source";
                    c.Source = new CyclusNET.Facilities.Source { OutCommod = "natl_u", Inventory_Size = 0, OutRecipe = "natl_u", Throughput = 30000 };
                    f.Config = c;
                    return f;
                case FacilityType.Sink:
                    f.Name = "repo";
                    c.Sink = new CyclusNET.Facilities.Sink { Capacity = 1E10, In_Commods = new CyclusNET.Facilities.Sink.InCommods("waste") };
                    f.Config = c;
                    return f;
                case FacilityType.Enrichment:
                    f.Name = "enrichment";
                    c.Enrichment = new CyclusNET.Facilities.Enrichment
                    { Feed_Commod = "natl_u", Feed_Recipe = "natl_u", Initial_Feed = 1E100,
                        Product_Commod = "uox", SWU_Capacity = 1E100, Tails_Assay = 0.003, Tails_Commod = "waste"
                    };
                    f.Config = c;
                    return f;
                case FacilityType.Separations:
                    f.Name = "separations";
                    c.Separations = new CyclusNET.Facilities.Separations
                    { Feed_Commods = new CyclusNET.Facilities.Separations.FeedCommods("depleted_u"), 
                        Feed_Commod_Prefs = new CyclusNET.Facilities.Separations.FeedCommodPrefs(2.0),
                        Throughput = 30001, Leftover_Commod = "waste", Feedbuf_Size = 30001
                    };
                    var s = new CyclusNET.Facilities.Separations.Item_1[1];
                    s[0] = new CyclusNET.Facilities.Separations.Item_1();
                    s[0].Commod = "sep_stream";
                    var i = new CyclusNET.Facilities.Separations.Info { Buf_Size = 1E100 };
                    i.Efficiencies = new CyclusNET.Facilities.Separations.Efficiencies(new CyclusNET.Facilities.Separations.Item_2[1]);
                    i.Efficiencies.Item[0] = new CyclusNET.Facilities.Separations.Item_2 { Comp = "Pu", Eff = 0.99 };
                    s[0].Info = i;
                    c.Separations.Streams = new CyclusNET.Facilities.Separations.Stream(s);
                    f.Config = c;
                    return f;
                case FacilityType.FuelFab:
                    f.Name = "fuelfab";
                    c.FuelFab = new CyclusNET.Facilities.FuelFab
                    { Fill_Commods = new CyclusNET.Facilities.FuelFab.FillCommods("depleted_u"),
                        Fill_Recipe = "depleted_u", Fill_Size = 30001, Fiss_Commods = new CyclusNET.Facilities.FuelFab.FissCommods("sep_stream"), 
                        Fiss_Size = 15000, Outcommod = "mox", Throughput = 30001, Spectrum = "thermal"
                    };
                    f.Config = c;
                    return f;
                case FacilityType.Reactor:
                    f.Name = "reactor";
                    c.Reactor = new CyclusNET.Facilities.Reactor
                    { Fuel_Incommods = new CyclusNET.Facilities.Reactor.FuelIncommods("uox", "mox"),
                        Fuel_Inrecipes = new CyclusNET.Facilities.Reactor.FuelInrecipes("fresh_uox", "fresh_mox"),
                        Fuel_Prefs = new CyclusNET.Facilities.Reactor.FuelPrefs(1.0, 2.0),
                        Fuel_Outcommods = new CyclusNET.Facilities.Reactor.FuelOutcommods("spent_uox", "waste"),
                        Fuel_Outrecipes = new CyclusNET.Facilities.Reactor.FuelOutrecipes("spent_uox", "spent_mox"),
                        Assem_Size = 30000, N_Assem_Batch = 1, N_Assem_Core = 3, Cycle_Time = 17, Refuel_Time = 2
                    };
                    f.Config = c;
                    return f;
                case FacilityType.SourceDepletedU:
                    f.Name = "depleted_src";
                    c.Source = new CyclusNET.Facilities.Source { OutCommod = "depleted_u", OutRecipe = "depleted_u" };
                    f.Config = c;
                    return f;
                default:
                    return f;
            }
        }

        public static Facility[] GetFacilities()
        {
            var f = new Facility[7];
            f[0] = FacilityFactory(FacilityType.Source);
            f[1] = FacilityFactory(FacilityType.Enrichment);
            f[2] = FacilityFactory(FacilityType.Separations);
            f[3] = FacilityFactory(FacilityType.FuelFab);
            f[4] = FacilityFactory(FacilityType.Reactor);
            f[5] = FacilityFactory(FacilityType.Sink);
            f[6] = FacilityFactory(FacilityType.SourceDepletedU);
            return f;
        }

        #endregion
    }

    public enum FacilityType
    {
        Source,
        Sink,
        Enrichment,
        Separations,
        FuelFab,
        Reactor,
        SourceDepletedU
    }
}

