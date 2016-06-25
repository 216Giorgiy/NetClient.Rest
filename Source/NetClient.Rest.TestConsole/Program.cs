using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new BlockchainClient { OnError = ex => Console.WriteLine(ex.Message) };
            var addresses = from a in client.Addresses
                            where a.Base58 == "1FW8KHjgtPTngKLHAw4YALtWoENsRpjt33" &&
                                  client.Addresses.Criteria.Limit == 20 &&
                                  client.Addresses.Criteria.Offset == 100
                            select a;
            var address = addresses.ToArray().SingleOrDefault();

            Console.WriteLine($"{address?.Hash160}");
            Console.ReadKey();
        }
    }
}