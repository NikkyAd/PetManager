using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManager.Models
{
    public class Pet
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("type")]
        public PetType type { get; set; }
    }    
}
