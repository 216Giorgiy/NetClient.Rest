namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    [Route("/rawaddr/{Base58}", "limit={Limit}", "offset={Offset}")]
    [Route("/rawaddr/{Hash160}", "limit={Limit}", "offset={Offset}")]
    public class AddressResource : Resource<Address>
    {
    }
}
