using System;
using System.Linq;
using Newtonsoft.Json;

namespace CyclusNET
{
    public class Control
    {

        #region Ctor

        public Control(int duration, int startMonth, int startYear)
        {
            Duration = duration;
            StartMonth = startMonth;
            StartYear = startYear;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration of the simulation in months.</value>
        [JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the start month.
        /// </summary>
        /// <value>The starting month.</value>
        [JsonProperty("startmonth")]
        public int StartMonth { get; set; }

        /// <summary>
        /// Gets or sets the start year.
        /// </summary>
        /// <value>The starting year.</value>
        [JsonProperty("startyear")]
        public int StartYear { get; set; }

        /// <summary>
        /// Gets or sets the sim handle.
        /// </summary>
        /// <value>A user-defined identifier for this simulation. (optional)</value>
        [JsonProperty("simhandle", NullValueHandling = NullValueHandling.Ignore)]
        public string SimHandle { get; set; }

        /// <summary>
        /// Gets or sets the duration of a single time step.
        /// </summary>
        /// <value>The duration of a single time step in seconds. If omitted, a default value of 1/12 of a year is used (i.e. 2,629,846 seconds).</value>
        [JsonProperty("dt", NullValueHandling = NullValueHandling.Ignore)]
        public int? DT { get; set; }

        /// <summary>
        /// Gets or sets the decay behavior of the simulation.
        /// </summary>
        /// <value>The decay behavior of the simulation. 
        /// never: turns decay off, 
        /// manual: decay is only computed if archetypes/agents explicilty decay their own material objects,
        /// lazy: decay is only computed whenever archetypes/agents “look” at a composition.</value>
        [JsonProperty("decay", NullValueHandling = NullValueHandling.Ignore)]
        public string Decay { get; set;}

        #endregion
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum DecayChoice
    {
        Never,
        Manual,
        Lazy
    }

}

