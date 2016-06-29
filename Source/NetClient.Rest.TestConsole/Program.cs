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
            addressResource.OnError = Console.WriteLine;

            var address = addressResource
                .Where(a => a.Base58 == "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa")
                .ToArray()
                .SingleOrDefault();

            Console.WriteLine();
            Console.WriteLine("  Data from service call.");
            Console.WriteLine();
            Console.WriteLine($"  Base58:   {address?.Base58}");
            Console.WriteLine($"  Hash160:  {address?.Hash160}");
            Console.ReadKey();
        }
    }
}