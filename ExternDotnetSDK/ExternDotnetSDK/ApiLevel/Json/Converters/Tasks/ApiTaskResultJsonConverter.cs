#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Api;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Tasks
{
    internal partial class ApiTaskResultJsonConverter : JsonConverterFactory
    {
        private readonly ApiTaskResultDtoPropNames propNames;
        
        public ApiTaskResultJsonConverter(JsonNamingPolicy namingPolicy) => 
            propNames = new ApiTaskResultDtoPropNames(namingPolicy);

        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.IsGenericType &&
            (typeToConvert.GetGenericTypeDefinition() == typeof (_ApiTaskResult<,>) ||
             typeToConvert.GetGenericTypeDefinition() == typeof (_ApiTaskResult<>));

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var typeArguments = typeToConvert.GenericTypeArguments;
            var converterType = typeArguments.Length == 2
                ? typeof (ApiTaskResultOfTwoResultsJsonConverter<,>).MakeGenericType(typeArguments)
                : typeof (ApiTaskResultOfOneResultJsonConverter<>).MakeGenericType(typeArguments);
            return (JsonConverter) Activator.CreateInstance(converterType, propNames);
        }
    }
}