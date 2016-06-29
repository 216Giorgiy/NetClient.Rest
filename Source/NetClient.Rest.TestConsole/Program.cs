using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a concrete resource without an API client. This allows decomposition down to the
            // resource level and provides an injectable and mockable construct.

            var addressResource = new AddressResource();
            addressResource.OnError = ex => { Console.WriteLine(ex); };
            var addresses = from a in addressResource where a.Hash160 == "62e907b15cbf27d5425399ebf6f0fb50ebb88f18" select a;
            var address = addresses.ToArray().SingleOrDefault();

            Console.WriteLine();
            Console.WriteLine("  Data from service call.");
            Console.WriteLine();
            Console.WriteLine($"  Base58:   {address?.Base58}");
            Console.WriteLine($"  Hash160:  {address?.Hash160}");
            Console.ReadKey();
        }
    }
}