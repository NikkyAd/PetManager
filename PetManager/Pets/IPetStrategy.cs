using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager.Pets
{
    public interface IPetStrategy
    {
        List<string> GetPetsOfMaleOwner();
        List<string> GetPetsOfFemaleOwner();
    }
}
