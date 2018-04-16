using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PetManager.Models;
using PetManager.Pets;
using PetManager.Services;
using System;
using System.Collections.Generic;
using System.Text;
namespace PetManager.Tests
{
    [TestClass]
    public class PetStrategyFactoryTest
    {
        private class TestDependencies
        {
            public IPetService petService { get; set; }
            public InputData inputData { get; set; }
            public PetStrategyFactory petStrategyFactory { get; set; }
        }

        private TestDependencies GetTestDependencies()
        {
            IPetService petService = new PetService();
            InputData inputData = new InputData();
            PetStrategyFactory petStrategyFactory = new PetStrategyFactory(petService, inputData);

            return new TestDependencies
            {
                petService = petService,
                inputData = inputData,
                petStrategyFactory = petStrategyFactory
            };
        }

        [TestMethod]
        public void TestStrategyInstanceForCats()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);
            var expectedStrategy = new CatsStrategy(dependencies.petService, dependencies.inputData);

            var result = dependencies.petStrategyFactory.Resolve(PetType.Cat);
            Assert.AreEqual(JsonConvert.SerializeObject(expectedStrategy), JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public void TestThrowExceptionOnRetrievingDogs()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            try
            {
                var result = dependencies.petStrategyFactory.Resolve(PetType.Dog);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NotImplementedException);
            }
        }

        [TestMethod]
        public void TestThrowExceptionOnRetrievingFish()
        {
            // Arrange
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
            var dependencies = GetTestDependencies();
            dependencies.inputData.ownerAndTheirPets = JsonDeSerializer.FromJson(jsonData);

            try
            {
                var result = dependencies.petStrategyFactory.Resolve(PetType.Fish);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NotImplementedException);
            }
        }
    }
}
