#region using directives

using Newtonsoft.Json;

#endregion

namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info"), SerializerSettings(typeof(JsonSerializerSettings))]
    internal class BlockchainClient : RestClient
    {
        #region properties and indexers

        [Route("/rawblock/{Block_Index}")]
        public RestElement<RawBlock> RawBlocks { get; set; }

        #endregion
    }
}