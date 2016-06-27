using Newtonsoft.Json;

namespace NetClient.Rest.TestConsole
{
    public class UnspentOutput
    {
        public string Address { get; }

        public string Addresses { get; }

        [JsonProperty("confirmations", Required = Required.Always)]
        public long Confirmations { get; private set; }

        [JsonProperty("script", Required = Required.Always)]
        public string Script { get; private set; }

        [JsonProperty("tx_hash", Required = Required.Always)]
        public string TransactionHash { get; private set; }

        [JsonProperty("tx_index", Required = Required.Always)]
        public long TransactionIndex { get; private set; }

        [JsonProperty("tx_output_n", Required = Required.Always)]
        public int TransactionOutputIndex { get; private set; }

        [JsonProperty("value", Required = Required.Always)]
        public decimal Value { get; private set; }
    }
}