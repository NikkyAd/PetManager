using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManager.Exceptions
{
    /// <summary>
    /// Incorrect PetType Exception
    /// </summary>
    public class PetTypeIncorrectException : Exception
    {
        public PetTypeIncorrectException(string petType)
            : base(String.Format("Invalid Pet Type: {0}", petType))
        {
        }
    }

    /// <summary>
    /// Incorrect Gender Exception
    /// </summary>
    public class GenderIncorrectException : Exception
    {
        public GenderIncorrectException(string gender)
            : base(String.Format("Invalid Gender: {0}", gender))
        {
        }
    }

    public class InputDataIncorrectException : Exception
    {
        public InputDataIncorrectException()
            : base("Data cannot be null or empty")
        {
        }
    }
}

