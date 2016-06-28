using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    /// <summary>
    ///     The REST resource settings.
    /// </summary>
    public class ResourceSettings
    {
        /// <summary>
        ///     Gets or sets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        public Uri BaseUri { get; set; }

        /// <summary>
        ///     Gets the routes.
        /// </summary>
        /// <value>The routes.</value>
        public IList<Route> Routes { get; } = new List<Route>();

        /// <summary>
        ///     Gets or sets the serializer settings.
        /// </summary>
        /// <value>The serializer settings.</value>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        ///     Fills the settings.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="property">The property.</param>
        public void Configure(INetClient client, PropertyInfo property)
        {
            if (BaseUri == null)
            {
                BaseUri = property.GetCustomAttributes<BaseUriAttribute>(true)?.FirstOrDefault()?.BaseUri;
            }

            if (BaseUri == null)
            {
                BaseUri = client.GetType().GetCustomAttributes<BaseUriAttribute>(true)?.FirstOrDefault()?.BaseUri;
            }

            if (SerializerSettings == null)
            {
                SerializerSettings = property.GetCustomAttributes<SerializerSettingsAttribute>(true)?.FirstOrDefault()?.SerializerSettings;
            }

            if (SerializerSettings == null)
            {
                SerializerSettings = client.GetType().GetCustomAttributes<SerializerSettingsAttribute>(true)?.FirstOrDefault()?.SerializerSettings;
            }

            var routes = property.GetCustomAttributes<RouteAttribute>(true)?.Select(attribute => attribute.Route);
            if (routes != null)
            {
                foreach (var route in routes)
                {
                    Routes.Add(route);
                }
            }
        }

        /// <summary>
        ///     Fills the settings.
        /// </summary>
        /// <param name="caller">The caller.</param>
        public void Configure(object caller)
        {
            if (BaseUri == null)
            {
                BaseUri = caller.GetType().GetCustomAttributes<BaseUriAttribute>(true).FirstOrDefault()?.BaseUri;
            }

            if (SerializerSettings == null)
            {
                SerializerSettings = caller.GetType().GetCustomAttributes<SerializerSettingsAttribute>(true).FirstOrDefault()?.SerializerSettings;
            }

            var routes = caller.GetType().GetCustomAttributes<RouteAttribute>(true)?.Select(attribute => attribute.Route);
            if (routes != null)
            {
                foreach (var route in routes)
                {
                    Routes.Add(route);
                }
            }
        }
    }
}