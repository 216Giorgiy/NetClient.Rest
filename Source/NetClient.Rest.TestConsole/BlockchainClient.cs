using System.Collections.Generic;

namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    public class BlockchainClient : RestClient
    {
        [Routes("/rawaddr/{Base58}", "/rawaddr/{Hash160}")]
        [Parameters("limit={Limit}", "offset={Offset}")]
        public Resource<Address, AddressCriteria> Addresses { get; private set; }

        [Routes("/unspent?active={Address}", "/unspent?active={Addresses}")]
        [RootNode("unspent_outputs")]
        public Resource<UnspentOutput> UnspentOutputs { get; private set; }
    }
}