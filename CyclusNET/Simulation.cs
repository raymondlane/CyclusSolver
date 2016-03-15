using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    public class Simulation
    {
        #region Ctor

        public Simulation(Control control)
        {
            Control = control;
            Archetypes = CyclusNET.Archetypes.GetArchetypes();
            Facility = CyclusNET.Facility.GetFacilities();
            Recipe = CyclusNET.Recipe.GetRecipes();
            Region = new CyclusNET.Region[1];
            Region[0] = new CyclusNET.Region(); 
        }

        #endregion

        #region Properties

        [JsonProperty("archetypes")]
        public Archetypes Archetypes { get; set; }

        [JsonProperty("control")]
        public Control Control { get; set;}

        [JsonProperty("facility")]
        public Facility[] Facility { get; set; }

        [JsonProperty("recipe")]
        public Recipe[] Recipe { get; set; }

        [JsonProperty("region")]
        public Region[] Region { get; set; }

        #endregion
    }
}

