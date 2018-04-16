using Newtonsoft.Json;
using PetManager.Diagnostics;
using PetManager.Exceptions;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetManager.Services
{
    public static class JsonDeSerializer
    {
        public static List<OwnerAndTheirPets> FromJson(string json)
        {
            List<OwnerAndTheirPets> ownerAndTheirPets = new List<OwnerAndTheirPets>();

            try
            {
                if (string.IsNullOrEmpty(json))
                    throw new InputDataIncorrectException();

                ownerAndTheirPets = JsonConvert.DeserializeObject<List<OwnerAndTheirPets>>(json, Converter.Settings);
                return ownerAndTheirPets;
            }
            catch(InputDataIncorrectException ex)
            {
                Console.WriteLine(ex.Message);
                Logger.LogError(ex);
                throw;
            }
        }

        static class PetTypeExtensions
        {
            public static PetType? ValueForString(string str)
            {
                switch (str)
                {
                    case "Cat": return PetType.Cat;
                    case "Dog": return PetType.Dog;
                    case "Fish": return PetType.Fish;
                    default: return null;
                }
            }

            public static PetType ReadJson(JsonReader reader, JsonSerializer serializer)
            {
                try
                {
                    var str = serializer.Deserialize<string>(reader);
                    var maybeValue = ValueForString(str);
                    if (maybeValue.HasValue)
                        return maybeValue.Value;
                    else
                        throw new PetTypeIncorrectException(str);
                }
                catch (PetTypeIncorrectException ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.LogError(ex);
                    throw;
                }
            }            
        }

        static class GenderExtensions
        {
            public static Gender? ValueForString(string str)
            {
                switch (str)
                {
                    case "Male": return Gender.Male;
                    case "Female": return Gender.Female;
                    default: return null;
                }
            }

            public static Gender ReadJson(JsonReader reader, JsonSerializer serializer)
            {
                try
                {
                    var str = serializer.Deserialize<string>(reader);
                    var maybeValue = ValueForString(str);
                    if (maybeValue.HasValue)
                        return maybeValue.Value;
                    else
                        throw new GenderIncorrectException(str);
                }
                catch (GenderIncorrectException ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.LogError(ex);
                    throw;
                }
            }            
        }

        internal class Converter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(PetType) || t == typeof(PetType?) || t == typeof(Gender) || t == typeof(Gender?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (t == typeof(PetType))
                    return PetTypeExtensions.ReadJson(reader, serializer);
                if (t == typeof(PetType?))
                {
                    if (reader.TokenType == JsonToken.Null) return null;
                    return PetTypeExtensions.ReadJson(reader, serializer);
                }
                if (t == typeof(Gender))
                    return GenderExtensions.ReadJson(reader, serializer);
                if (t == typeof(Gender?))
                {
                    if (reader.TokenType == JsonToken.Null) return null;
                    return GenderExtensions.ReadJson(reader, serializer);
                }

                throw new Exception("Unknown type");
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                MissingMemberHandling = MissingMemberHandling.Error,
                Converters = { new Converter() }
            };
        }
    }
}
