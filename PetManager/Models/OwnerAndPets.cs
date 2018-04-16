using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManager.Models
{
    public class OwnerAndTheirPets
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("gender")]
        public Gender gender { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }

        [JsonProperty("pets")]
        public List<Pet> pets { get; set; }
    }
}
