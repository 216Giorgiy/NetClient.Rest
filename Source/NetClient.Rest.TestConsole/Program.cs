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
                .Where(a => a.Hash160 == "e6fd80cf7a96cec946e8ffa13573fb946a5e0980051f739c97ba31bc4a3d7fc1")
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