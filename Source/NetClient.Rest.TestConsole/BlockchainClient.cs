using System.Collections.Generic;

namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    public class BlockchainClient : RestClient
    {
        //[Route("/rawaddr/{Base58}", "limit={Limit}", "offset={Offset}")]
        //[Route("/rawaddr/{Hash160}", "limit={Limit}", "offset={Offset}")]
        [Routes("/rawaddr/{Base58}", "/rawaddr/{Hash160}")]
        [Parameters("limit={Limit}", "offset={Offset}")]
        public Resource<Address, AddressCriteria> Addresses { get; private set; }

        //[Route("/unspent?active={Address}", node: "unspent_outputs")]
        //[Route("/unspent?active={Addresses}", node: "unspent_outputs")]
        [Routes("/unspent?active={Address}", "/unspent?active={Addresses}")]
        [RootNode("unspent_outputs")]
        public Resource<UnspentOutput> UnspentOutputs { get; private set; }
    }
}