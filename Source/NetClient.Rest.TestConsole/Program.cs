using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    /// <summary>
    ///     The application entry class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            var client = new BlockchainClient();
            client.RawBlocks.OnError = ex => Console.WriteLine(ex.Message);

            var rawBlocks = from r in client.RawBlocks where r.Block_Index == 417260 select r;
            var rawBlock = rawBlocks.ToArray().SingleOrDefault();

            Console.WriteLine(rawBlock?.Block_Index);
            Console.ReadKey();
        }
    }
}