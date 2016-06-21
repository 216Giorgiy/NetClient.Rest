using System;
using System.Reflection;
using NetClient.Rest.TestConsole;

namespace NetClient.Rest
{
    /// <summary>
    ///     Class RestClient.
    /// </summary>
    public abstract class RestClient
    {
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

                var element = Activator.CreateInstance(property.PropertyType, baseUri, route, serializerSettings, null);

                property.SetValue(this, element);
            }
        }
    }
}