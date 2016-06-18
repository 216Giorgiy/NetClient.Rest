using System;
using System.Linq;

namespace NetClient.Rest.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient();
            var elements = from element in client.Test1 where element.Id == 2 select element;

            foreach (var element in elements)
            {
                Console.WriteLine(element.Id);
            }
        }
    }
}