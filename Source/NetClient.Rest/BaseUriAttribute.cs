using System;

namespace NetClient.Rest
{
    public class BaseUriAttribute : Attribute
    {
        #region constructors

        public BaseUriAttribute(string baseUri)
        {
            BaseUri = new Uri(baseUri);
        }

        #endregion

        #region properties and indexers

        public Uri BaseUri { get; }

        #endregion
    }
}