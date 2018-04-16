using System;
using System.Collections.Generic;
using PetManager.Models;
using PetManager.Services;
using PetManager.Pets;

namespace PetManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            HttpClientService httpClientService = new HttpClientService();
            // Read data from Http Service
            string jsonData = httpClientService.Get();
            
            // Deserialize json data
            InputData data = new InputData();
            data.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            if (data.ownerAndTheirPets != null)
            {
                // Get Pets of Pet types listed
                List<PetType> getPetsOfType = new List<PetType>() { PetType.Cat };

                IPetService petService = new PetService();
                OutputService outPutService = new OutputService();
                var stratergyFactory = new PetStrategyFactory(petService, data);

                // Determine the strategy and get pets based on PetType and Owner Gender
                foreach (PetType petType in getPetsOfType)
                {
                    var Strategy = stratergyFactory.Resolve(petType);
                    List<string> maleOwnerPets = Strategy.GetPetsOfMaleOwner();
                    List<string> femaleOwnerCats = Strategy.GetPetsOfFemaleOwner();

                    outPutService.PrintOutput(petType, maleOwnerPets, femaleOwnerCats);
                }
            }
            else
            {
                Console.WriteLine("No data to process");
            }

            Console.ReadKey();
        }
    }
}