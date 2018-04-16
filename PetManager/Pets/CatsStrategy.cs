using PetManager.Models;
using PetManager.Services;
using System.Collections.Generic;
using System.Linq;

namespace PetManager.Pets
{
    public class CatsStrategy : IPetStrategy
    {
        private readonly InputData _inputData;
        private readonly IPetService _petService;

        public CatsStrategy(IPetService petService, InputData inputData)
        {
            _inputData = inputData;
            _petService = petService;
        }

        public List<string> GetPetsOfMaleOwner()
        {
            var pets = _petService.GetPets(_inputData, Gender.Male, PetType.Cat);
            return pets?.OrderBy(x => x).ToList();
        }

        public List<string> GetPetsOfFemaleOwner()
        {
            var pets = _petService.GetPets(_inputData, Gender.Female, PetType.Cat);
            return pets?.OrderBy(x => x).ToList();
        }
    }
}
