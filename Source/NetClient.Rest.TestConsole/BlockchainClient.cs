using Newtonsoft.Json;

namespace NetClient.Rest.TestConsole
{
    /// <summary>
    ///     Blockchain NetClient.
    /// </summary>
    /// <seealso cref="NetClient.Rest.RestClient" />
    [BaseUri("https://blockchain.info"), SerializerSettings(typeof(JsonSerializerSettings))]
    internal class BlockchainClient : RestClient
    {
        /// <summary>
        ///     Gets or sets the raw blocks.
        /// </summary>
        /// <value>The raw blocks.</value>
        [Route("/rawblock/{Block_Index}")]
        public RestElement<RawBlock> RawBlocks { get; set; }
    }
}