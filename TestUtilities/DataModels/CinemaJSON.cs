using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestUtilities.DataModels
{
    public class CinemaJSON
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("branchId")]
        public int BranchId { get; set; }

        [JsonProperty("id")]
        public long id { get; set; }
    }
}
