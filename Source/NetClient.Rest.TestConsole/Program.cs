using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TestRestClient();
            var elements = from rawBlock in client.RawBlocks where rawBlock.Block_Index == 417260 select rawBlock;

            foreach (var element in elements)
            {
                Console.WriteLine(element.Block_Index);
            }
        }
    }
}