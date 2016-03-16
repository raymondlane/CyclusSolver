using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// Every agent that participates in a Cyclus simulation is based on a prototype 
    /// that is formed by configuring an archetype. The archetypes block defines the 
    /// set of archetypes that are available to a simulation, and provides 
    /// specifications that uniquely identify each archetype.
    /// A single archetypes block is required, and contains one or more spec blocks. 
    /// Each spec block has these blocks in the following order:
    /// path (optional) - a slash-separated path
    /// lib (optional) - a library name
    /// name (required) - a name
    /// alias (optional) - a alternative name for the archetype
    /// In addition to the unambiguous specification (as defined in Archetype Identification 
    /// and Discovery) formed by the path, lib, and name, the alias provides an 
    /// alternative     /// name by which to refer to the archetype elsewhere in the file. 
    /// If an alias is defined, it is the only way to refer to that archetype in other locations.
    /// </summary>
    public class Archetypes
    {
        #region Ctor

        public Archetypes() { }

        #endregion

        #region Properties

        [JsonProperty("spec")]
        public Spec[] Spec { get; set; }

        #endregion

        #region Methods

        public static Archetypes GetArchetypes()
        {
            var s = new CyclusNET.Spec[9];
            s[0] = new CyclusNET.Spec { Lib = "agents", Name = "NullInst" };
            s[1] = new CyclusNET.Spec { Lib = "agents", Name = "NullRegion" };
            s[2] = new CyclusNET.Spec { Lib = "cycamore", Name = "Source" };
            s[3] = new CyclusNET.Spec { Lib = "cycamore", Name = "Sink" };
            s[4] = new CyclusNET.Spec { Lib = "cycamore", Name = "Enrichment" };
            s[5] = new CyclusNET.Spec { Lib = "cycamore", Name = "Reactor" };
            s[6] = new CyclusNET.Spec { Lib = "cycamore", Name = "FuelFab" };
            s[7] = new CyclusNET.Spec { Lib = "cycamore", Name = "Separations" };
            s[8] = new CyclusNET.Spec { Lib = "cycamore", Name = "DeployInst" };
            var a = new Archetypes();
            a.Spec = s;
            return a;
        }

        #endregion
    }

    public struct Spec
    {
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set;}

        [JsonProperty("lib", NullValueHandling = NullValueHandling.Ignore)]
        public string Lib { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

    }

}

