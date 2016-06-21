using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new BlockchainClient();
            var rawBlocks = from r in client.RawBlocks where r.Block_Index == 417260 select r;
            var rawBlock = rawBlocks.ToArray().SingleOrDefault();

            Console.WriteLine(rawBlock?.Block_Index);
            Console.ReadKey();
        }
    }
}