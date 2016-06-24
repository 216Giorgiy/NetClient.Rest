using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new BlockchainClient { OnError = ex => Console.WriteLine(ex.Message) };
            var addresses = from a in client.Addresses where client.Addresses.Criteria.Base58 == "1FW8KHjgtPTngKLHAw4YALtWoENsRpjt33" select a;
            var address = addresses.ToArray().SingleOrDefault();

            Console.WriteLine($"{address?.Hash160}");
            Console.ReadKey();
        }
    }
}