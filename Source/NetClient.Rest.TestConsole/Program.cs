using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // The Resource class represents a resource in a REST-ful service API. Resource can be used in three
            // ways depending on your requirements. Change the DemoType to exercise the demo code demonstrating
            // each of the three use cases.
            var demoType = DemoType.ApiClientResource;

            IQueryable<Address> addresses = null;

            switch (demoType)
            {
                case DemoType.ApiClientResource:
                    // Creating a concrete API client can ease developer interaction with the API. This requires a
                    // concrete client representing the entire API and a property representing each resource you
                    // want to make available.

                    var client = new BlockchainClient { OnError = ex => Console.WriteLine(ex.Message) };
                    addresses = from a in client.Addresses
                                    where a.Base58 == "1FW8KHjgtPTngKLHAw4YALtWoENsRpjt33" &&
                                          client.Addresses.Criteria.Limit == 20 &&
                                          client.Addresses.Criteria.Offset == 100
                                    select a;

                    break;
                case DemoType.ConcreteResource:
                    // Create a concrete resource without an API client. This allows decomposition down to the
                    // resource level and provides an injectable and mockable construct.

                    addresses = from a in new AddressResource() where a.Base58 == "1FW8KHjgtPTngKLHAw4YALtWoENsRpjt33" select a;

                    break;
                case DemoType.ConstructedResource:
                    // Same concept as the concrete resource, but there is no concrete class to support the
                    // settings. Settings must be composed and provided to the resource at construction.

                    var settings = new ResourceSettings();
                    settings.BaseUri = new Uri("https://blockchain.info");
                    settings.RouteTemplates.AddRange(new[] { "/rawaddr/{Base58}", "/rawaddr/{Hash160}" });
                    settings.ParameterTemplates.AddRange(new[] { "limit={Limit}", "offset={Offset}" });

                    addresses = from a in new Resource<Address, AddressCriteria>(settings) where a.Base58 == "1FW8KHjgtPTngKLHAw4YALtWoENsRpjt33" select a;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var address = addresses.ToArray().SingleOrDefault();

            Console.WriteLine();
            Console.WriteLine("  Data from service call.");
            Console.WriteLine();
            Console.WriteLine($"  Base58:   {address?.Base58}");
            Console.WriteLine($"  Hash160:  {address?.Hash160}");
            Console.ReadKey();

            // Testing out root nodes.
            //var client1 = new BlockchainClient { OnError = ex => Console.WriteLine(ex.Message) };
            //var x = client1.UnspentOutputs
            //    .Where(u => u.Address == "1FW8KHjgtPTngKLHAw4YALtWoENsRpjt33")
            //    .ToArray();
        }
    }
}