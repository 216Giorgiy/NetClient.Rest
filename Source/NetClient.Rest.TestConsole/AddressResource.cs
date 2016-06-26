namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    [Routes("/rawaddr/{Base58}", "/rawaddr/{Hash160}")]
    [Parameters("limit={Limit}", "offset={Offset}")]
    public class AddressResource : Resource<Address, AddressCriteria>
    {
    }
}
