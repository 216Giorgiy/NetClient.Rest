using System.Collections.Generic;

namespace NetClient.Rest
{
    internal class RestQueryValues
    {
        internal IDictionary<string, object> ResourceValues { get; } = new Dictionary<string, object>();
    }
}