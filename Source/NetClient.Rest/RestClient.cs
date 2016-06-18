using System;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    public class RestClient
    {
        public ISet<TestElement> Test1 { get; } = new RestSet<TestElement>(new Uri("https://blockchain.info"), "/rawblock/{Id}", new JsonSerializerSettings());
    }
}