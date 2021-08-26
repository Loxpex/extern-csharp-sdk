using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_StringDeserialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public Docflow Deserialize_SysTextJson() => context.SysSerializer.DeserializeFromJson<Docflow>(context.Json);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public Docflow Deserialize_JsonNet() => context.JsonNetSerializer.DeserializeFromJson<Docflow>(context.Json);
    }
}