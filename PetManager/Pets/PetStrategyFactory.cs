using PetManager.Diagnostics;
using PetManager.Models;
using PetManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager.Pets
{
    public class PetStrategyFactory : IStrategyFactory
    {
        private readonly IPetService _petService;
        private readonly InputData _inputData;

        public PetStrategyFactory(IPetService petService, InputData inputData)
        {
            _petService = petService;
            _inputData = inputData;
        }

        public IPetStrategy Resolve(PetType petType)
        {
            IPetStrategy petStrategy = null;

            if (petType == PetType.Cat)
            {
                petStrategy = new CatsStrategy(_petService, _inputData);
            }
            else
            {
                Console.WriteLine(String.Format("Strategy for petType {0} is Not implemented", petType));
                Logger.LogError(new NotImplementedException(String.Format("Strategy for petType {0} is Not implemented", petType)));
                throw new NotImplementedException(String.Format("Strategy for petType {0} is Not implemented",petType));
            }

            return petStrategy;
        }
    }
}