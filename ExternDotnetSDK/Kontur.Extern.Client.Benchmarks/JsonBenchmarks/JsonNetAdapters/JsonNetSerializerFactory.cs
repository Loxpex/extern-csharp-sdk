using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters.Converters;
using Kontur.Extern.Client.Http.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters
{
    public static class JsonNetSerializerFactory
    {
        public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false) => new JsonNetSerializer(
            new KebabCaseNamingStrategy(),
            new JsonConverter[]
            {
                new UrnJsonConverter(),
                new DocflowContainingConverter<Docflow>(),
                new DocflowContainingConverter<DocflowPageItem>()
            },
            ignoreIndentation
        );
    }
}