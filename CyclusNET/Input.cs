using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    /// <summary>
    /// Every Cyclus input file must have exactly one simulation section that contains all data for a simulation.
    /// </summary>
    public class Input
    {
        #region Ctor

        public Input(Control control)
        {
            Simulation = new Simulation(control);
        }

        #endregion

        #region Properties

        [JsonProperty("simulation")]
        public Simulation Simulation { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        #endregion
    }
}

