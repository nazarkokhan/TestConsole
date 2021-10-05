namespace TestConsole
{
    using System;
    using System.Collections.Generic;
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
        private List<ChatDto> Chats = new List<ChatDto>(10_000_000);
        // public ChatsStorage ChatsStorage = new ChatsStorage(default);
        private UserChatsStorage UserChatsStorage = new UserChatsStorage();

        public StorageBenchmark()
        {
            var rnd = new Random();

            for (var i = 0; i < 10_000_000; i++)
            {
                Chats.Add(new ChatDto(
                    rnd.Next(10_000_000),
                    rnd.Next(10_000_000),
                    rnd.Next(10_000_000)));
            }
        }

        [Benchmark]
        public void InitializeVoidBenchmark()
        {
            UserChatsStorage.InitializeVoid(Chats);
        }

        [Benchmark]
        public void InitializeBoolBenchmark()
        {
            UserChatsStorage.InitializeBool(Chats);
        }
    }
}