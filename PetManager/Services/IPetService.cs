using Newtonsoft.Json.Linq;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetManager.Services
{
    public interface IPetService
    {
        List<string> GetPets(InputData data, Gender ownerGender, PetType petType);
    }
}
