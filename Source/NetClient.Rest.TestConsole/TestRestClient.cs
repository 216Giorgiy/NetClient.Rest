#region using directives

using System;
using Newtonsoft.Json;

#endregion

namespace NetClient.Rest.TestConsole
{
    /// <summary>
    ///     Class TestRestClient.
    /// </summary>
    internal class TestRestClient : RestClient
    {
        #region properties and indexers

        public IElement<TestElement> RawBlocks { get; } = new Element<TestElement>(new Uri("https://blockchain.info"), "/rawblock/{Block_Index}", new JsonSerializerSettings());

        #endregion
    }
}