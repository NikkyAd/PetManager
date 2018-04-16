using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetManager.Services
{
    public class PetService : IPetService
    {
        public List<string> GetPets(InputData data, Gender ownerGender, PetType petType)
        {
            List<Pet> petsByOwnerGender = GetPetsByOwnerGender(data, ownerGender);
            List<string> petsByType = GetPetsByPetType(petsByOwnerGender, petType);

            return petsByType;
        }

        private List<Pet> GetPetsByOwnerGender(InputData data, Gender ownerGender)
        {
            return data?.ownerAndTheirPets?.Where(x => x.gender == ownerGender && x.pets != null)
                    ?.SelectMany(x => x.pets)
                    .ToList();
        }

        private List<string> GetPetsByPetType(List<Pet> petsByOwnerGender, PetType petType)
        {
            return petsByOwnerGender?.Where(y => y.type == petType)
                   ?.Select(z => z.name.ToString())
                    .ToList();
        }
    }
}
