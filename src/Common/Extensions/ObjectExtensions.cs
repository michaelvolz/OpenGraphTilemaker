using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Extensions
{
    [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Utility class")]
    public static class ObjectExtensions
    {
        public static string ReturnDump(this object subject)
        {
            var settings =
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };

            return JsonConvert.SerializeObject(subject, settings);
        }

        public static string ReturnDumpFlat(this object subject)
        {
            var settings =
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.None
                };

            return JsonConvert.SerializeObject(subject, settings);
        }

        public static Dictionary<string, object?> PropertiesToDictionary(this object subject) =>
            Guard.Against.Null(subject, nameof(subject)).GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(subject, null));

        public static PropertyInfo? PublicProperty(this object subject, string propertyName) =>
            Guard.Against.Null(subject, nameof(subject)).GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

        public static IEnumerable<PropertyInfo> PublicProperties(this object subject) =>
            Guard.Against.Null(subject, nameof(subject)).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        public static string JSONSerialize(this object subject)
        {
            Guard.Against.Null(subject, nameof(subject));

            var settings =
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.None,
                    TypeNameHandling = TypeNameHandling.None
                };

            string serializeObject;

            try
            {
                serializeObject = JsonConvert.SerializeObject(subject, settings);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"JSON serialization failed for object of type: {subject.GetType()}.\r\n{e.Message}", e);
            }

            return serializeObject;
        }

        [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "Analyzer bug?")]
        public static T JSONUnSerialize<T>(this string stringObj)
        {
            if (stringObj == null) throw new ArgumentNullException(nameof(stringObj));

            var deserializeObject = JsonConvert.DeserializeObject(stringObj, typeof(T))
                                    ?? throw new InvalidOperationException("JsonConvert.DeserializeObject(stringObj, typeof(T))");

            var deserializedConcrete = (T)deserializeObject;
            var isList = deserializedConcrete.GetType().IsGenericType && deserializedConcrete is IEnumerable;

            // -- performance optimization
            if (isList)
                return deserializedConcrete;

            var item = (T)(Activator.CreateInstance(typeof(T)) ?? throw new InvalidOperationException());
            isList = item.GetType().IsGenericType && item is IEnumerable;

            if (!isList)
                return deserializedConcrete;

            var result = JsonConvert.DeserializeObject(stringObj, typeof(T), new FlexibleCollectionConverter())
                         ?? throw new InvalidOperationException("JsonConvert.DeserializeObject(stringObj, typeof(T), new FlexibleCollectionConverter())");

            return (T)result;
        }

        /// <summary>
        ///     FlexibleCollectionConverter
        /// </summary>
        private class FlexibleCollectionConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => serializer.Serialize(writer, value);

            public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
                JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.StartArray)
                    return serializer.Deserialize(reader, objectType);

                var array = new JArray(JToken.ReadFrom(reader));
                return array.ToObject(objectType);
            }

            public override bool CanConvert(Type objectType) => typeof(IEnumerable).IsAssignableFrom(objectType);
        }

        /// <summary>
        ///     GenericListCreationJsonConverter
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedType.Local", Justification = "Utility class")]
        private class GenericListCreationJsonConverter<T> : JsonConverter
        {
            public override bool CanRead => true;
            public override bool CanWrite => false;
            public override bool CanConvert(Type objectType) => true;

            public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.StartArray)
                    return serializer.Deserialize<List<T>>(reader);
                var t = serializer.Deserialize<T>(reader);

                return new List<T>(new[] { t! });
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => throw new NotImplementedException();
        }
    }
}
