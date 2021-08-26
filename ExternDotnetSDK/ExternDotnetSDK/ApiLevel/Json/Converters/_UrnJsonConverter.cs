#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters
{
    public class _UrnJsonConverter : JsonConverter<Urn>
    {
        public override Urn? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var urnValue = reader.GetString();
            return urnValue == null ? null : new Urn(urnValue);
        }

        public override void Write(Utf8JsonWriter writer, Urn value, JsonSerializerOptions options) => 
            writer.WriteStringValue(value.ToString());
    }
}