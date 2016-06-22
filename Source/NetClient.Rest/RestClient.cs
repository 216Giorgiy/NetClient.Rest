using System;
using System.Reflection;

namespace NetClient.Rest
{
    /// <summary>
    ///     Provides a base class for sending requests and receiving responses over network boundaries.
    /// </summary>
    public abstract class RestClient : INetClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RestClient" /> class.
        /// </summary>
        protected RestClient()
        {
            var baseUri = GetType().GetCustomAttribute<BaseUriAttribute>()?.BaseUri;
            // throw if no baseUri

            var serializerSettings = GetType().GetCustomAttribute<SerializerSettingsAttribute>()?.Settings;
            // throw if no serializerSettings

            foreach (var property in GetType().GetProperties())
            {
                var route = property.GetCustomAttribute<RouteAttribute>()?.Template;
                // throw if no route

                var element = Activator.CreateInstance(property.PropertyType, this, baseUri, route, serializerSettings, null);

                property.SetValue(this, element);
            }
        }
    }
}