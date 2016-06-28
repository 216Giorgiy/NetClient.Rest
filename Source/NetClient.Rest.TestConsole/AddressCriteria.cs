using System.Reflection.Emit;

namespace NetClient.Rest.TestConsole
{
    [BaseUri("https://blockchain.info")]
    public class AddressCriteria
    {
        [Route("/rawaddr/{Base58}")]
        public string Base58 { get; set; }

        [Route("/rawaddr/{Hash160}")]
        public string Hash160 { get; set; }

        [Parameter("limit={Limit}")]
        public int? Limit { get; set; }

        [Parameter("offset={Offset}")]
        public int? Offset { get; set; }
    }
}