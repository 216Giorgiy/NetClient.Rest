using System;

namespace NetClient.Rest.TestConsole
{
    public class RouteAttribute : Attribute
    {
        #region constructors

        public RouteAttribute(string template)
        {
            Template = template;
        }

        #endregion

        #region properties and indexers

        public string Template { get; }

        #endregion
    }
}