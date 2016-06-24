namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    public class BlockchainClient : RestClient
    {
        [Route("/rawaddr/{Base58}", "/rawaddr/{Hash160}")]
        public Resource<Address, AddressCriteria> Addresses { get; private set; }
    }
}