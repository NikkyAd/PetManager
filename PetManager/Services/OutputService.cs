using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManager.Services
{
    public class OutputService
    {
        public void PrintOutput(PetType petType, List<string> maleOwnerPets, List<string> femaleOwnerPets)
        {
            Console.WriteLine("--"+petType+"s--");
            if (maleOwnerPets == null)
            {
                Console.WriteLine(Gender.Male + ":");
                Console.WriteLine("---- No pets to display ----");
            }
            else
            {
                Console.WriteLine(Gender.Male + ":");
                for (var i = 0; i < maleOwnerPets.Count; i++)
                {
                    Console.WriteLine(maleOwnerPets[i]);
                }
            }

            Console.WriteLine("\n");

            
            if (femaleOwnerPets == null)
            {
                Console.WriteLine(Gender.Female + ":");
                Console.WriteLine("---- No pets to display ----");
            }
            else
            {
                Console.WriteLine(Gender.Female + ":");
                for (var i = 0; i < femaleOwnerPets.Count; i++)
                {
                    Console.WriteLine(femaleOwnerPets[i]);
                }
            }          
        }
    }
}
