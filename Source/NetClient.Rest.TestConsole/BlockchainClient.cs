using Newtonsoft.Json;

namespace NetClient.Rest.TestConsole
{
    /// <summary>
    ///     Blockchain.info client.
    /// </summary>
    /// <seealso cref="RestClient" />
    [BaseUri("https://blockchain.info")]
    [SerializerSettings(typeof(JsonSerializerSettings))]
    internal class BlockchainClient : RestClient
    {
        /// <summary>
        ///     Gets or sets the raw blocks element.
        /// </summary>
        /// <value>The raw blocks element.</value>
        [Route("/rawblock/{Block_Index}")]
        public Resource<RawBlock> RawBlocks { get; set; }
    }
}