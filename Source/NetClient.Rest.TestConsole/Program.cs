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
            var client = new BlockchainClient { OnError = ex => Console.WriteLine(ex.Message) };

            var rawBlocksFromIndex = from r in client.RawBlocks where r.Block_Index == 417260 select r;
            var rawBlockFromIndex = rawBlocksFromIndex.ToArray().SingleOrDefault();
            Console.WriteLine($"Raw Block from Index: {rawBlockFromIndex?.Block_Index}");

            var rawBlocksFromHash = from r in client.RawBlocks where r.Hash == "0000000000000000444a4e81b64a9a43e9c1bc9be488a56aa0d2c0c7152939c5" select r;
            var rawBlockFromHash = rawBlocksFromHash.ToArray().SingleOrDefault();
            Console.WriteLine($"Raw Block from Hash: {rawBlockFromHash?.Block_Index}");

            Console.ReadKey();
        }
    }
}