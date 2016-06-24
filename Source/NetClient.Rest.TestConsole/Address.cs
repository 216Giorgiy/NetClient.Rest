using Newtonsoft.Json;

namespace NetClient.Rest.TestConsole
{
    public class Address
    {
        [JsonProperty("address", Required = Required.Always)]
        public string Base58 { get; private set; }

        [JsonProperty("hash160", Required = Required.Always)]
        public string Hash160 { get; private set; }
        [JsonProperty("total_received", Required = Required.Always)]
        public decimal Received { get; private set; }

        [JsonProperty("total_sent", Required = Required.Always)]
        public decimal Sent { get; private set; }

        [JsonProperty("n_tx", Required = Required.Always)]
        public long TransactionCount { get; private set; }
    }
}