using System;
using Newtonsoft.Json;

namespace CyclusNET.Facilities
{
    /// <summary>
    /// FuelFab takes in 2 streams of material and mixes them in ratios in order to supply 
    /// material that matches some neutronics properties of reqeusted material. It uses an 
    /// equivalence type method [1] inspired by a similar approach in the COSI fuel cycle 
    /// simulator.
    /// The FuelFab has 3 input inventories: fissile stream, filler stream, and an optional 
    /// top-up inventory. All materials received into each inventory are always combined 
    /// into a single material (i.e. a single fissile material, a single filler material, 
    /// etc.). The input streams and requested fuel composition are each assigned weights 
    /// based on summing:
    /// N * (p_i - p_U238) / (p_Pu239 - p_U238)
    /// for each nuclide where:
    /// p = nu*sigma_f - sigma_a for the nuclide
    /// p_U238 is p for pure U238
    /// p_Pu239 is p for pure Pu239
    /// N is the nuclide’s atom fraction
    /// nu is the average # neutrons per fission
    /// sigma_f is the microscopic fission cross-section
    /// sigma_a is the microscopic neutron absorption cross-section
    /// The cross sections are from the simple cross section library in PyNE. They can be set 
    /// to either a thermal or fast neutron spectrum. A linear interpolation is performed using 
    /// the weights of the fissile, filler, and target streams. The interpolation is used to 
    /// compute a mixing ratio of the input streams that matches the target weight. In the event 
    /// that the target weight is higher than the fissile stream weight, the FuelFab will attempt 
    /// to use the top-up and fissile input streams together instead of the fissile and filler 
    /// streams. All supplied material will always have the same weight as the requested material.
    /// The supplying of mixed material is constrained by available inventory quantities and a per 
    /// time step throughput limit. Requests for fuel material larger than the throughput can never 
    /// be met. Fissile inventory can be requested/received via one or more commodities. The DRE 
    /// request preference for each of these commodities can also optionally be specified. 
    /// By default, the top-up inventory size is zero, and it is not used for mixing.
    /// </summary>
    public class FuelFab
    {
        #region Ctor

        public FuelFab()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the fill_ commods.
        /// </summary>
        /// <value>Ordered list of commodities on which to requesting filler stream material.</value>
        [JsonProperty("fill_commods")]
        public FillCommods Fill_Commods { get; set; }

        /// <summary>
        /// Gets or sets the fill_ commod_ prefs.
        /// </summary>
        /// <value>Filler stream commodity request preferences for each of the given filler commodities 
        /// (same order). If unspecified, default is to use 1.0 for all preferences.</value>
        [JsonProperty("fill_commod_prefs", NullValueHandling = NullValueHandling.Ignore)]
        public FillCommodPrefs Fill_Commod_Prefs { get; set; }

        /// <summary>
        /// Gets or sets the fill_ recipe.
        /// </summary>
        /// <value>Name of recipe to be used in filler material stream requests.</value>
        [JsonProperty("fill_recipe")]
        public string Fill_Recipe { get; set; }

        /// <summary>
        /// Gets or sets the size of the fill_.
        /// </summary>
        /// <value>Size of filler material stream inventory.</value>
        [JsonProperty("fill_size")]
        public double Fill_Size { get; set;}

        /// <summary>
        /// Gets or sets the fiss_ commods.
        /// </summary>
        /// <value>Ordered list of commodities on which to requesting fissile stream material.</value>
        [JsonProperty("fiss_commods")]
        public FissCommods Fiss_Commods {get;set;}

        /// <summary>
        /// Gets or sets the fiss_ commod_ prefs.
        /// </summary>
        /// <value>Fissile stream commodity request preferences for each of the given fissile 
        /// commodities (same order). If unspecified, default is to use 1.0 for all preferences.</value>
        [JsonProperty("fiss_commod_prefs", NullValueHandling = NullValueHandling.Ignore)]
        public FissCommodPrefs Fiss_Commod_Prefs { get; set; }

        /// <summary>
        /// Gets or sets the fiss_ recipe.
        /// </summary>
        /// <value>Name for recipe to be used in fissile stream requests. Empty string results in use of an empty dummy recipe.</value>
        [JsonProperty("fiss_recipe", NullValueHandling = NullValueHandling.Ignore)]
        public string Fiss_Recipe { get; set; }

        /// <summary>
        /// Gets or sets the size of the fiss_.
        /// </summary>
        /// <value>Size of fissile material stream inventory.</value>
        [JsonProperty("fiss_size")]
        public double Fiss_Size { get; set; }

        /// <summary>
        /// Gets or sets the topup_ commod.
        /// </summary>
        /// <value>Commodity on which to request material for top-up stream. This MUST be set if ‘topup_size > 0’.</value>
        [JsonProperty("topup_commod", NullValueHandling = NullValueHandling.Ignore)]
        public string Topup_Commod { get; set; }

        /// <summary>
        /// Gets or sets the topup_ preference.
        /// </summary>
        /// <value>Top-up material stream request preference.</value>
        [JsonProperty("topup_pref", NullValueHandling = NullValueHandling.Ignore)]
        public double? Topup_Pref { get; set; }

        /// <summary>
        /// Gets or sets the topup_ recipe.
        /// </summary>
        /// <value>Name of recipe to be used in top-up material stream requests. This MUST be set if ‘topup_size > 0’.</value>
        [JsonProperty("topup_recipe", NullValueHandling = NullValueHandling.Ignore)]
        public string Topup_Recipe { get; set; }

        /// <summary>
        /// Gets or sets the size of the topup_.
        /// </summary>
        /// <value>Size of top-up material stream inventory.</value>
        [JsonProperty("topup_size", NullValueHandling = NullValueHandling.Ignore)]
        public double? Topup_Size { get; set; }

        /// <summary>
        /// Gets or sets the outcommod.
        /// </summary>
        /// <value>Commodity on which to offer/supply mixed fuel material.</value>
        [JsonProperty("outcommod")]
        public string Outcommod { get; set; }

        /// <summary>
        /// Gets or sets the throughput.
        /// </summary>
        /// <value>Maximum number of kg of fuel material that can be supplied per time step.</value>
        [JsonProperty("throughput")]
        public double Throughput { get; set; }

        /// <summary>
        /// Gets or sets the spectrum.
        /// </summary>
        /// <value>The type of cross-sections to use for composition property calculation. Use 
        /// ‘fission_spectrum_ave’ for fast reactor compositions or ‘thermal’ for thermal reactors.</value>
        [JsonProperty("spectrum")]
        public string Spectrum { get; set; }

        #endregion

        #region Classes

        public class FillCommods
        {
            public FillCommods() {}

            public FillCommods(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class FillCommodPrefs
        {
            public FillCommodPrefs() {}

            public FillCommodPrefs(params double[] args)
            {
                Val = new double[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public double[] Val { get; set; }

        }

        public class FissCommods
        {
            public FissCommods() {}

            public FissCommods(params string[] args)
            {
                Val = new string[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }

        }

        public class FissCommodPrefs
        {
            public FissCommodPrefs() {}

            public FissCommodPrefs(params double[] args)
            {
                Val = new double[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public double[] Val { get; set; }

        }

        #endregion
    }
}

