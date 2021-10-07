namespace TestConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using BenchmarkDotNet.Running;
    using Services;

    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<StorageBenchmark>();
        }
    }

    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class StorageBenchmark
    {
        private static readonly Random rnd = new();
        private static List<ChatDto> Chats = Enumerable
            .Range(1, 1_000)
            .Select(_ => new ChatDto(
                rnd.Next(1_000),
                rnd.Next(1_000),
                rnd.Next(1_000)))
            .ToList();
        
        private static UserChatsStorage UserChatsStorage1 = new();
        private static UserChatsStorage UserChatsStorage2 = new();

        [Benchmark]
        public void InitializeVoidBenchmark()
        {
            UserChatsStorage1.InitializeVoid(Chats);
        }

        [Benchmark]
        public void InitializeBoolBenchmark()
        {
            UserChatsStorage2.InitializeBool(Chats);
        }
    }
}