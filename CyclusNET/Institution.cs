using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// Every agent that participates in a Cyclus simulation represents either a 
    /// facility, an institution or a region. Each institution block defines an 
    /// agent that acts as an institution in the simulation. In contrast to the 
    /// facility block that defines a prototype, this block defines an agent.
    /// </summary>
    public class Institution
    {
        #region Ctor

        public Institution()
        {
            Name = "SingleInstitution";
            InitialFacilityList = new FacilityList();
            InitialFacilityList.EntryList = new Entry[7];
            InitialFacilityList.EntryList[0] = new Entry("source", 0);
            InitialFacilityList.EntryList[1] = new Entry("repo", 1);
            InitialFacilityList.EntryList[2] = new Entry("reactor", 2);
            InitialFacilityList.EntryList[3] = new Entry("depleted_src", 1);
            InitialFacilityList.EntryList[4] = new Entry("fuelfab", 1);
            InitialFacilityList.EntryList[5] = new Entry("separations", 1);
            InitialFacilityList.EntryList[6] = new Entry("enrichment", 1);
            Config = new InstitutionConfig();
            Config.NullRegion = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The archetype-specific configuration.</value>
        [JsonProperty("config")]
        public InstitutionConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the initial facility list.
        /// </summary>
        /// <value>A list of facility agents operating at the beginning of the simulation.</value>
        [JsonProperty("initialfacilitylist", NullValueHandling = NullValueHandling.Ignore)]
        public FacilityList InitialFacilityList { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A name for the prototype.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the lifetime.
        /// </summary>
        /// <value>A non-negative integer indicating the number of time steps that this institution 
        /// agent will be active in the simulation.</value>
        [JsonProperty("lifetime", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lifetime { get; set; }

        #endregion

        #region Classes

        public class Entry
        {
            #region Ctor

            public Entry() {}

            public Entry(string prototype, int number)
            {
                Prototype = prototype;
                Number = number;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the prototype.
            /// </summary>
            /// <value>The name of a facility prototype defined elsewhere in the input file.</value>
            [JsonProperty("prototype")]
            public string Prototype { get; set; }

            /// <summary>
            /// Gets or sets the number.
            /// </summary>
            /// <value>The number of such facilities that are operating at the beginning of the simulation.</value>
            [JsonProperty("number")]
            public int Number { get; set; }

            #endregion
        }

        public class FacilityList
        {
            #region Ctor

            public FacilityList(params Entry[] entries)
            {
                EntryList = entries;
            }

            #endregion

            #region Properties

            [JsonProperty("entry")]
            public Entry[] EntryList { get; set; }

            #endregion
        }

        #endregion
    }

    public class InstitutionConfig
    {
        #region Ctor

        public InstitutionConfig()
        {
        }

        #endregion

        #region Properties

        [JsonProperty("NullInst", NullValueHandling = NullValueHandling.Include)]
        public CyclusNET.Institutions.NullInstitution NullRegion { get; set;}

        #endregion
    }

}

