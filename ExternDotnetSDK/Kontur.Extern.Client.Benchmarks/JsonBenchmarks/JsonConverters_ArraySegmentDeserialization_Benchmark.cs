using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_ArraySegmentDeserialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public Docflow Deserialize_SysTextJson() => context.SysSerializer.DeserializeFromJson<Docflow>(context.JsonBytes);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public Docflow Deserialize_JsonNet() => context.JsonNetSerializer.DeserializeFromJson<Docflow>(context.JsonBytes);
    }
}