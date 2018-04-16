using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManager.Models
{
    public class InputData
    {
        public List<OwnerAndTheirPets> ownerAndTheirPets { get; set; }
    }
}
