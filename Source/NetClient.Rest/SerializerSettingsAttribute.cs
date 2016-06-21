#region using directives

using System;
using Newtonsoft.Json;

#endregion

namespace NetClient.Rest.TestConsole
{
    public class SerializerSettingsAttribute : Attribute
    {
        #region constructors

        public SerializerSettingsAttribute(Type type)
        {
            Settings = Activator.CreateInstance(type) as JsonSerializerSettings;
        }

        #endregion

        #region properties and indexers

        public JsonSerializerSettings Settings { get; }

        #endregion
    }
}