using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CyclusNET.Facilities
{
    /// <summary>
    /// Reactor is a simple, general reactor based on static compositional transformations 
    /// to model fuel burnup. The user specifies a set of input fuels and corresponding burnt 
    /// compositions that fuel is transformed to when it is discharged from the core. No 
    /// incremental transmutation takes place. Rather, at the end of an operational cycle, 
    /// the batch being discharged from the core is instantaneously transmuted from its original 
    /// fresh fuel composition into its spent fuel form.
    /// Each fuel is identified by a specific input commodity and has an associated input recipe 
    /// (nuclide composition), output recipe, output commidity, and preference. The preference 
    /// identifies which input fuels are preferred when requesting. Changes in these preferences 
    /// can be specified as a function of time using the pref_change variables. Changes in the 
    /// input-output recipe compositions can also be specified as a function of time using the 
    /// recipe_change variables.
    /// The reactor treats fuel as individual assemblies that are never split, combined or otherwise 
    /// treated in any non-discrete way. Fuel is requested in full-or-nothing assembly sized quanta. 
    /// If real-world assembly modeling is unnecessary, parameters can be adjusted (e.g. n_assem_core, 
    /// assem_size, n_assem_batch). At the end of every cycle, a full batch is discharged from the core 
    /// consisting of n_assem_batch assemblies of assem_size kg. The reactor also has a specifiable 
    /// refueling time period following the end of each cycle at the end of which it will resume 
    /// operation on the next cycle if it has enough fuel for a full core; otherwise it waits until 
    /// it has enough fresh fuel assemblies.
    /// In addition to its core, the reactor has an on-hand fresh fuel inventory and a spent fuel 
    /// inventory whose capacities are specified by n_assem_fresh and n_assem_spent respectively. 
    /// Each time step the reactor will attempt to acquire enough fresh fuel to fill its fresh fuel 
    /// inventory (and its core if the core isn’t currently full). If the fresh fuel inventory has 
    /// zero capacity, fuel will be ordered just-in-time after the end of each operational cycle before 
    /// the next begins. If the spent fuel inventory becomes full, the reactor will halt operation at the 
    /// end of the next cycle until there is more room. Each time step, the reactor will try to trade 
    /// away as much of its spent fuel inventory as possible.
    /// When the reactor reaches the end of its lifetime, it will discharge all material from its core 
    /// and trade away all its spent fuel as quickly as possible. Full decommissioning will be delayed 
    /// ntil all spent fuel is gone. If the reactor has a full core when it is decommissioned (i.e. is 
    /// mid-cycle) when the reactor is decommissioned, half (rounded up to nearest int) of its assemblies 
    /// are transmuted to their respective burnt compositions.
    /// </summary>
    public class Reactor
    {
        #region Ctor

        public Reactor()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the fuel_ incommods.
        /// </summary>
        /// <value>Ordered list of input commodities on which to requesting fuel.</value>
        [JsonProperty("fuel_incommods")]
        public FuelIncommods Fuel_Incommods { get; set; }

        /// <summary>
        /// Gets or sets the fuel_ inrecipes.
        /// </summary>
        /// <value>Fresh fuel recipes to request for each of the given fuel input commodities (same order).</value>
        [JsonProperty("fuel_inrecipes")]
        public FuelInrecipes Fuel_Inrecipes { get; set; }

        /// <summary>
        /// Gets or sets the fuel_ prefs.
        /// </summary>
        /// <value>The preference for each type of fresh fuel requested corresponding to each input commodity (same order). 
        /// If no preferences are specified, 1.0 is used for all fuel requests (default).</value>
        [JsonProperty("fuel_prefs", NullValueHandling = NullValueHandling.Ignore)]
        public FuelPrefs Fuel_Prefs { get; set; }

        /// <summary>
        /// Output commodities on which to offer spent fuel originally received as each particular input commodity (same order).
        /// </summary>
        /// <value>The fuel_ outcommods.</value>
        [JsonProperty("fuel_outcommods")]
        public FuelOutcommods Fuel_Outcommods { get; set; }

        /// <summary>
        /// Gets or sets the fuel_ outrecipes.
        /// </summary>
        /// <value>Spent fuel recipes corresponding to the given fuel input commodities (same order). Fuel received via a particular 
        /// input commodity is transmuted to the recipe specified here after being burned during a cycle.</value>
        [JsonProperty("fuel_outrecipes")]
        public FuelOutrecipes Fuel_Outrecipes { get; set; }

        /// <summary>
        /// Gets or sets the recipe_ change_ times.
        /// </summary>
        /// <value>A time step on which to change the input-output recipe pair for a requested fresh fuel.</value>
        [JsonProperty("recipe_change_times", NullValueHandling = NullValueHandling.Ignore)]
        public RecipeChangeTimes Recipe_Change_Times { get; set; }

        /// <summary>
        /// Gets or sets the recipe_ change_ commods.
        /// </summary>
        /// <value>The input commodity indicating fresh fuel for which recipes will be changed. 
        /// Same order as and direct correspondence to the specified recipe change times.</value>
        [JsonProperty("recipe_change_commods", NullValueHandling = NullValueHandling.Ignore)]
        public RecipeChangeCommods Recipe_Change_Commods { get; set; }

        /// <summary>
        /// Gets or sets the recipe_ change_ in.
        /// </summary>
        /// <value>The new input recipe to use for this recipe change. Same order as and direct 
        /// correspondence to the specified recipe change times.</value>
        [JsonProperty("recipe_change_in", NullValueHandling = NullValueHandling.Ignore)]
        public RecipeChangeIn Recipe_Change_In { get; set; }

        /// <summary>
        /// Gets or sets the recipe_ change_ out.
        /// </summary>
        /// <value>The new output recipe to use for this recipe change. Same order as and direct 
        /// correspondence to the specified recipe change times.</value>
        [JsonProperty("recipe_change_out", NullValueHandling = NullValueHandling.Ignore)]
        public RecipeChangeOut Recipe_Change_Out { get; set; }

        /// <summary>
        /// Gets or sets the size of the assem_.
        /// </summary>
        /// <value>Mass (kg) of a single assembly.</value>
        [JsonProperty("assem_size")]
        public double Assem_Size { get; set; }

        /// <summary>
        /// Gets or sets the n_ assem_ batch.
        /// </summary>
        /// <value>Number of assemblies that constitute a single batch. This is the number of 
        /// assemblies discharged from the core fully burned each cycle.Batch size is equivalent 
        /// to n_assem_batch / n_assem_core.</value>
        [JsonProperty("n_assem_batch")]
        public int N_Assem_Batch { get; set; }

        /// <summary>
        /// Gets or sets the n_ assem_ core.
        /// </summary>
        /// <value>Number of assemblies that constitute a full core.</value>
        [JsonProperty("n_assem_core")]
        public int N_Assem_Core { get; set; }

        /// <summary>
        /// Gets or sets the n_ assem_ fresh.
        /// </summary>
        /// <value>Number of fresh fuel assemblies to keep on-hand if possible.</value>
        [JsonProperty("n_assem_fresh", NullValueHandling = NullValueHandling.Ignore)]
        public int? N_Assem_Fresh { get; set; }

        /// <summary>
        /// Gets or sets the n_ assem_ spent.
        /// </summary>
        /// <value>Number of spent fuel assemblies that can be stored on-site before 
        /// reactor operation stalls.</value>
        [JsonProperty("n_assem_spent", NullValueHandling = NullValueHandling.Ignore)]
        public int? N_Assem_Spent { get; set; }

        /// <summary>
        /// Gets or sets the cycle_ time.
        /// </summary>
        /// <value>The duration of a full operational cycle (excluding refueling time) in time steps.</value>
        [JsonProperty("cycle_time")]
        public int Cycle_Time { get; set; }

        /// <summary>
        /// Gets or sets the refuel_ time.
        /// </summary>
        /// <value>The duration of a full refueling period - the minimum time between 
        /// the end of a cycle and the start of the next cycle.</value>
        [JsonProperty("refuel_time")]
        public int Refuel_Time { get; set; }

        /// <summary>
        /// Gets or sets the cycle_ step.
        /// </summary>
        /// <value>Number of time steps since the start of the last cycle. Only set 
        /// this if you know what you are doing.</value>
        [JsonProperty("cycle_step", NullValueHandling = NullValueHandling.Ignore)]
        public int? Cycle_Step { get; set; }

        /// <summary>
        /// Gets or sets the power_ cap.
        /// </summary>
        /// <value>Amount of electrical power the facility produces when operating normally.</value>
        [JsonProperty("power_cap", NullValueHandling = NullValueHandling.Ignore)]
        public double? Power_Cap { get; set; }

        /// <summary>
        /// Gets or sets the name of the power_.
        /// </summary>
        /// <value>The name of the ‘power’ commodity used in conjunction with a deployment curve.</value>
        [JsonProperty("power_name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Power_Name { get; set; }

        /// <summary>
        /// Gets or sets the pref_ change_ times.
        /// </summary>
        /// <value>A time step on which to change the request preference for a particular fresh fuel type.</value>
        [JsonProperty("pref_change_times", NullValueHandling = NullValueHandling.Ignore)]
        public PrefChangeTimes Pref_Change_Times { get; set; }

        /// <summary>
        /// Gets or sets the pref_ change_ commods.
        /// </summary>
        /// <value>The input commodity for a particular fuel preference change. Same order as and 
        /// direct correspondence to the specified preference change times.</value>
        [JsonProperty("pref_change_commods", NullValueHandling = NullValueHandling.Ignore)]
        public PrefChangeCommods Pref_Change_Commods { get; set; }

        /// <summary>
        /// Gets or sets the pref_ change_ values.
        /// </summary>
        /// <value>The new/changed request preference for a particular fresh fuel. Same order as and 
        /// direct correspondence to the specified preference change times.</value>
        [JsonProperty("pref_change_values", NullValueHandling = NullValueHandling.Ignore)]
        public PrefChangeValues Pref_Change_Values { get; set; }

        #endregion

        #region Classes

        public class FuelIncommods
        {
            public FuelIncommods() {}

            public FuelIncommods(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class FuelInrecipes
        {
            public FuelInrecipes() {}

            public FuelInrecipes(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class FuelPrefs
        {
            public FuelPrefs() {}

            public FuelPrefs(params double[] args)
            {
                Val = new double[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public double[] Val { get; set; }

        }

        public class FuelOutcommods
        {
            public FuelOutcommods() {}

            public FuelOutcommods(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class FuelOutrecipes
        {
            public FuelOutrecipes() {}

            public FuelOutrecipes(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class RecipeChangeTimes
        {
            public RecipeChangeTimes() {}

            public RecipeChangeTimes(params int[] args)
            {
                Val = new int[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public int[] Val { get; set; }

        }

        public class RecipeChangeCommods
        {
            public RecipeChangeCommods() {}

            public RecipeChangeCommods(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class RecipeChangeIn
        {
            public RecipeChangeIn() {}

            public RecipeChangeIn(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class RecipeChangeOut
        {
            public RecipeChangeOut() {}

            public RecipeChangeOut(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class PrefChangeTimes
        {
            public PrefChangeTimes() {}

            public PrefChangeTimes(params int[] args)
            {
                Val = new int[] {};
                Val = args;
            }

            [JsonProperty("val")]
            public int[] Val { get; set; }

        }

        public class PrefChangeCommods
        {
            public PrefChangeCommods() {}

            public PrefChangeCommods(params string[] args)
            {
                Val = new string[] { };
                Val = args;
            }

            [JsonProperty("val")]
            public string[] Val { get; set; }
        }

        public class PrefChangeValues
        {
            public PrefChangeValues() {}

            public PrefChangeValues(params double[] args)
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

