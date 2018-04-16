using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetManager.Services;
using PetManager.Models;

namespace PetManager.Tests
{
    [TestClass]
    public class PetServiceTest
    {
        private class TestDependencies
        {
            public IPetService petService { get; set; }
            public InputData inputData { get; set; }
        }

        private TestDependencies GetTestDependencies()
        {
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            return new TestDependencies
            {
                petService = petService,
                inputData = inputData
            };
        }

        [TestMethod]
        public void TestCatsCountOfMaleOwnerWhenThereAreNoCats()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            var result = petService.GetPets(inputData, Gender.Male, PetType.Cat);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCatsCountOfMaleOwnerWhenThereIsOnlyOneCat()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            var result = petService.GetPets(inputData, Gender.Male, PetType.Cat);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestCatsCountOfFemaleOwnerWhenThereAreNoCats()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Dog\"}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            var result = petService.GetPets(inputData, Gender.Female, PetType.Cat);

            Assert.AreEqual(0, result.Count);
        }

        
        [TestMethod]
        public void TestCatsCountOfFemaleOwnerWhenThereIsOnlyOneCat()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            var result = petService.GetPets(inputData, Gender.Female, PetType.Cat);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestCatsCountOfFemaleOwnerWhenThereIsMoreThanOneCat()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            var result = petService.GetPets(inputData, Gender.Female, PetType.Cat);

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestCatsCountOfMaleOwnerWhenThereIsMoreThanOneCat()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            InputData inputData = new InputData();
            inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            IPetService petService = new PetService();

            var result = petService.GetPets(inputData, Gender.Male, PetType.Cat);

            Assert.AreEqual(4, result.Count);
        }
        
    }
}
