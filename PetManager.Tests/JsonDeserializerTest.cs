using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetManager.Services;
using System.IO;
using PetManager.Exceptions;

namespace PetManager.Tests
{
    [TestClass]
    public class JsonDeserializerTest
    {
        [TestMethod]
        public void TestThrowExceptionIfDataIsEmpty()
        {
            string jsonData = string.Empty;

            try
            {
                var result = JsonDeSerializer.FromJson(jsonData);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InputDataIncorrectException);
                Assert.AreEqual(ex.Message, "Data cannot be null or empty");
            }
        }

        [TestMethod]
        public void TestThrowExceptionIfDataIsNull()
        {
            string jsonData = null;

            try
            {
                var result = JsonDeSerializer.FromJson(jsonData);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InputDataIncorrectException);
                Assert.AreEqual(ex.Message, "Data cannot be null or empty");
            }
        }

        [TestMethod]
        public void TestThrowExceptionIfPetTypeIsInvalid()
        {
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Big Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]}]";

            try
            {
                var result = JsonDeSerializer.FromJson(jsonData);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is PetTypeIncorrectException);
                Assert.AreEqual(ex.Message, "Invalid Pet Type: Big Cat");
            }
        }

        [TestMethod]
        public void TestThrowExceptionIfGenderIsInvalid()
        {
            string jsonData = "[{\"name\":\"Bob\",\"gender\":\"FMale\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]}]";

            try
            {
                var result = JsonDeSerializer.FromJson(jsonData);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is GenderIncorrectException);
                Assert.AreEqual(ex.Message, "Invalid Gender: FMale");
            }
        }
    }
}