using System.Collections.Generic;

namespace NetClient.Rest
{
    internal class RestQueryValues
    {
        internal IList<object> Criteria { get; } = new List<object>();

        internal IDictionary<string, object> ResourceValues { get; } = new Dictionary<string, object>();
    }
}