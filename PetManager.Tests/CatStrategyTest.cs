using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetManager.Models;
using PetManager.Pets;
using PetManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManager.Tests
{
    [TestClass]
    public class CatStrategyTest
    {
        private class TestDependencies
        {
            public IPetService petService { get; set; }
            public InputData inputData { get; set; }
            public CatsStrategy catStrategy { get; set; }
        }

        private TestDependencies GetTestDependencies()
        {
            IPetService petService = new PetService();
            InputData inputData = new InputData();
            CatsStrategy catStrategy = new CatsStrategy(petService, inputData);

            return new TestDependencies
            {
                petService = petService,
                inputData = inputData,
                catStrategy = catStrategy
            };
        }

        [TestMethod]
        public void TestCatsListOfMaleOwnerWhenThereMoreThanOneCat_SortedAscending()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var expectedOutput = new List<string> { "Garfield", "Jim", "Max", "Tom"};

            var result = dependencies.catStrategy.GetPetsOfMaleOwner();

            Assert.AreEqual(expectedOutput.Count, result.Count);
            Assert.AreEqual(expectedOutput[0], result[0]);
            Assert.AreEqual(expectedOutput[1], result[1]);
            Assert.AreEqual(expectedOutput[2], result[2]);
            Assert.AreEqual(expectedOutput[3], result[3]);
        }

        [TestMethod]
        public void TestCatsListOfFemaleOwnerWhenThereMoreThanOneCat_SortedAscending()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var expectedOutput = new List<string> { "Garfield", "Simba", "Tabby" };

            var result = dependencies.catStrategy.GetPetsOfFemaleOwner();

            Assert.AreEqual(expectedOutput.Count, result.Count);
            Assert.AreEqual(expectedOutput[0], result[0]);
            Assert.AreEqual(expectedOutput[1], result[1]);
            Assert.AreEqual(expectedOutput[2], result[2]);
        }

        [TestMethod]
        public void TestCatsListOfMaleOwnerWhenThereAreNoCats()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var result = dependencies.catStrategy.GetPetsOfMaleOwner();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCatsListOfMaleOwnerWhenPetsIsNull()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":null}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var result = dependencies.catStrategy.GetPetsOfMaleOwner();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCatsListOfMaleOwnerWhenThereIsOnlyOneCat()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var result = dependencies.catStrategy.GetPetsOfMaleOwner();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Garfield", result[0]);

        }

        [TestMethod]
        public void TestCatsListOfFemaleOwnerWhenThereAreNoCats()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Female\",\"age\":23,\"pets\":[{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Dog\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var result = dependencies.catStrategy.GetPetsOfFemaleOwner();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCatsListOfFemaleOwnerWhenPetsIsNull()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":null}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var result = dependencies.catStrategy.GetPetsOfFemaleOwner();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCatsListOfFemaleOwnerWhenThereIsOnlyOneCat()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            var result = dependencies.catStrategy.GetPetsOfFemaleOwner();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Garfield", result[0]);
        }
    }
}
