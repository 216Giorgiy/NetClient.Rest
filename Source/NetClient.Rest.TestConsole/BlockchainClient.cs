namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    public class BlockchainClient : RestClient
    {
        [Routes("/rawaddr/{Base58}", "/rawaddr/{Hash160}")]
        [Parameters("limit={Limit}", "offset={Offset}")]
        public Resource<Address, AddressCriteria> Addresses { get; private set; }
    }
}