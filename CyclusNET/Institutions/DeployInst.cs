using System;
using Newtonsoft.Json;

namespace CyclusNET.Institutions
{
    /// <summary>
    /// Builds and manages agents (facilities) according to a manually specified 
    /// deployment schedule. Deployed agents are automatically decommissioned at 
    /// the end of their lifetime. The user specifies a list of prototypes for 
    /// each and corresponding build times, number to build, and (optionally) 
    /// lifetimes. The same prototype can be specified multiple times with any 
    /// combination of the same or different build times, build number, and 
    /// lifetimes.
    /// </summary>
    public class DeployInst
    {
        #region Ctor

        public DeployInst()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the prototypes.
        /// </summary>
        /// <value>Ordered list of prototypes to build.</value>
        [JsonProperty("prototypes")]
        public Prototype Prototypes { get; set; }

        /// <summary>
        /// Gets or sets the build_ times.
        /// </summary>
        /// <value>Time step on which to deploy agents given in 
        /// prototype list (same order).</value>
        [JsonProperty("build_times")]
        public Build_Time Build_Times { get; set; }

        /// <summary>
        /// Gets or sets the n_ build.
        /// </summary>
        /// <value>Number of each prototype given in prototype list 
        /// that should be deployed (same order).</value>
        [JsonProperty("n_build")]
        public NBuild N_Build { get; set;}

        /// <summary>
        /// Gets or sets the lifetimes.
        /// </summary>
        /// <value>Lifetimes for each prototype in prototype list (same order). 
        /// These lifetimes override the lifetimes in the original prototype definition. 
        /// If unspecified, lifetimes from the original prototype definitions are used. 
        /// Although a new prototype is created in the Prototypes table for each lifetime w
        /// ith the suffix ‘_life_[lifetime]’, all deployed agents themselves will have 
        /// he same original prototype name (and so will the Agents tables).</value>
        [JsonProperty("lifetimes", NullValueHandling = NullValueHandling.Ignore)]
        public Lifetime Lifetimes { get; set; }


        #endregion

        #region Classes

        public class Prototype
        {
            public Prototype() {}

            public Prototype(params string[] args)
            {
                Val = new string[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class Build_Time
        {
            public Build_Time(){}

            public Build_Time(params int[] args)
            {
                Val = new int[]{};
                Val = args;
            }

            [JsonProperty("val")]
            public int[] Val { get; set; }
        }

        public class NBuild
        {
            public NBuild(){}

            public NBuild(params int[] args)
            {
                Val = new int[]{};
                Val = args;
            }

            [JsonProperty("val")]
            public int[] Val { get; set; }
        }

        public class Lifetime
        {
            public Lifetime() {}

            public Lifetime(params int[] args)
            {
                Val = new int[]{};
                Val = args;
            }

            [JsonProperty("val")]
            public int[] Val { get; set; }

        }

        #endregion


    }
}

