using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager.Pets
{
    interface IStrategyFactory
    {
        IPetStrategy Resolve(PetType petType);
    }
}
