using System;
using Newtonsoft.Json;

namespace CyclusNET
{
    public class In_Commods
    {
        public In_Commods()
        {
        }

        public In_Commods(params string[] vals)
        {
            Val = new string[] { };
            Val = vals;
        }

        public string[] Val { get; set; }

    }
}

