using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// The most common resources being exchanged by Cyclus agents is a material 
    /// which has both a composition and a mass. While the composition of a 
    /// material object may be manipulated over time by the agents that transact 
    /// it, it is often necessary for the user to define a specific recipe for a 
    /// material. Each recipe section can be used to define a named composition 
    /// that can then be referenced elsewhere, such as in the data for an 
    /// archetype.
    /// </summary>
    public class Recipe
    {
        #region Ctor

        public Recipe()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the basis.
        /// </summary>
        /// <value>An indication of whether the composition is based 
        /// on the atom fractions or mass fractions; one of:
        /// atom - atom fractions are given in the nuclide list
        /// mass - mass fractions are given in the nuclude list
        /// </value>
        [JsonProperty("basis")]
        public string Basis { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The unique name for this commodity.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the nuclide.
        /// </summary>
        /// <value>A list of nuclides and their relative quantities.</value>
        [JsonProperty("nuclide")]
        public Nuclide[] Nuclide { get; set;}

        #endregion

        public static Recipe[] GetRecipes()
        {
            var r = new Recipe[6];
            // natl_u
            r[0] = new Recipe { Name = "natl_u", Basis = "mass", Nuclide = new Nuclide[2] };
            var n = new Nuclide { Comp = 0.711, Id = "U235" };
            r[0].Nuclide[0] = n;
            n = new Nuclide { Comp = 99.289, Id = "U238" };
            r[0].Nuclide[1] = n;
            // fresh_uox
            r[1] = new Recipe { Name = "fresh_uox", Basis = "mass", Nuclide = new Nuclide[2] };
            n = new Nuclide { Comp = 0.04, Id = "U235" };
            r[1].Nuclide[0] = n;
            n = new Nuclide { Comp = 0.96, Id = "U238" };
            r[1].Nuclide[1] = n;
            // depleted_u
            r[2] = new Recipe { Name = "depleted_u", Basis = "mass", Nuclide = new Nuclide[2] };
            n = new Nuclide { Comp = 0.003, Id = "U235" };
            r[2].Nuclide[0] = n;
            n = new Nuclide { Comp = 0.997, Id = "U238" };
            r[2].Nuclide[1] = n;
            // fresh_mox
            r[3] = new Recipe { Name = "fresh_mox", Basis = "mass", Nuclide = new Nuclide[7] };
            n = new Nuclide { Comp = 0.0027381, Id = "U235" };
            r[3].Nuclide[0] = n;
            n = new Nuclide { Comp = 0.9099619, Id = "U238" };
            r[3].Nuclide[1] = n;
            n = new Nuclide { Comp = 0.001746, Id = "Pu238" };
            r[3].Nuclide[2] = n;
            n = new Nuclide { Comp = 0.045396, Id = "Pu239" };
            r[3].Nuclide[3] = n;
            n = new Nuclide { Comp = 0.020952, Id = "Pu240" };
            r[3].Nuclide[4] = n;
            n = new Nuclide { Comp = 0.013095, Id = "Pu241" };
            r[3].Nuclide[5] = n;
            n = new Nuclide { Comp = 0.005238, Id = "Pu242" };
            r[3].Nuclide[6] = n;
            // spent_mox
            r[4] = new Recipe { Name = "spent_mox", Basis = "mass", Nuclide = new Nuclide[7] };
            n = new Nuclide { Comp = 0.0017381, Id = "U235" };
            r[4].Nuclide[0] = n;
            n = new Nuclide { Comp = 0.90, Id = "U238" };
            r[4].Nuclide[1] = n;
            n = new Nuclide { Comp = 0.001746, Id = "Pu238" };
            r[4].Nuclide[2] = n;
            n = new Nuclide { Comp = 0.0134, Id = "Pu239" };
            r[4].Nuclide[3] = n;
            n = new Nuclide { Comp = 0.020952, Id = "Pu240" };
            r[4].Nuclide[4] = n;
            n = new Nuclide { Comp = 0.013095, Id = "Pu241" };
            r[4].Nuclide[5] = n;
            n = new Nuclide { Comp = 0.005238, Id = "Pu242" };
            r[4].Nuclide[6] = n;
            // spent_uox
            r[5] = new Recipe { Name = "spent_uox", Basis = "mass", Nuclide = new Nuclide[13] };
            n = new Nuclide { Comp = 156.729, Id = "U235" };
            r[5].Nuclide[0] = n;
            n = new Nuclide { Comp = 102.103, Id = "U236" };
            r[5].Nuclide[1] = n;
            n = new Nuclide { Comp = 18280.324, Id = "U238" };
            r[5].Nuclide[2] = n;
            n = new Nuclide { Comp = 13.656, Id = "Np237" };
            r[5].Nuclide[3] = n;
            n = new Nuclide { Comp = 5.043, Id = "Pu238" };
            r[5].Nuclide[4] = n;
            n = new Nuclide { Comp = 106.343, Id = "Pu239" };
            r[5].Nuclide[5] = n;
            n = new Nuclide { Comp = 41.357, Id = "Pu240" };
            r[5].Nuclide[6] = n;
            n = new Nuclide { Comp = 36.477, Id = "Pu241" };
            r[5].Nuclide[7] = n;
            n = new Nuclide { Comp = 15.387, Id = "Pu242" };
            r[5].Nuclide[8] = n;
            n = new Nuclide { Comp = 1.234, Id = "Am241" };
            r[5].Nuclide[9] = n;
            n = new Nuclide { Comp = 3.607, Id = "Am243" };
            r[5].Nuclide[10] = n;
            n = new Nuclide { Comp = 0.431, Id = "Cm244" };
            r[5].Nuclide[11] = n;
            n = new Nuclide { Comp = 1.263, Id = "Cm245" };
            r[5].Nuclide[12] = n;
            return r;
        }

    }

    public class Nuclide
    {
        #region Ctor

        public Nuclide()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the comp.
        /// </summary>
        /// <value>A number indicating the quantity of this nuclide to that of other 
        /// nuclides in the material. The quantities are normalized to the sum of all 
        /// a composition’s constituents, so the user need not provide quantities 
        /// normalized to any particular value.</value>
        [JsonProperty("comp")]
        public double Comp { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>identifies a particular nuclide either with its canonical integer 
        /// ID in the form ZZAAAMMMM or one of a few other common forms. Here are some 
        /// acceptable forms: 922350000, U-235, U235, Am242M. The canonical integer 
        /// nuclide format is a general format that encodes the atomic number (Z), 
        /// the mass number (A) and the energy state (M) with the formula 
        /// (Z*1000 + A) * 10000 + M.</value>
        [JsonProperty("id")]
        public string Id { get; set; }

        #endregion
    }

}

