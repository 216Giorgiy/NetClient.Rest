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
        ///     Gets the parameter templates.
        /// </summary>
        /// <value>The parameter templates.</value>
        public List<string> ParameterTemplates { get; } = new List<string>();

        /// <summary>
        ///     Gets the JSON root node path.
        /// </summary>
        /// <value>The root node path.</value>
        public List<string> RootNode { get; } = new List<string>();

        /// <summary>
        ///     Gets the route templates.
        /// </summary>
        /// <value>The route templates.</value>
        public List<string> RouteTemplates { get; } = new List<string>();

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
                var attribute = client.GetType().GetCustomAttributes<BaseUriAttribute>(true).FirstOrDefault();
                BaseUri = attribute?.BaseUri;
            }

            if (BaseUri == null)
            {
                var attribute = property.GetCustomAttributes<BaseUriAttribute>(true).FirstOrDefault();
                BaseUri = attribute?.BaseUri;
            }

            if (SerializerSettings == null)
            {
                var attribute = client.GetType().GetCustomAttributes<SerializerSettingsAttribute>(true).FirstOrDefault();
                SerializerSettings = attribute?.SerializerSettings;
            }

            if (SerializerSettings == null)
            {
                var attribute = property.GetCustomAttributes<SerializerSettingsAttribute>(true).FirstOrDefault();
                SerializerSettings = attribute?.SerializerSettings;
            }

            var rootNode = property.GetCustomAttributes<RootNodeAttribute>(true).FirstOrDefault()?.Nodes;
            if (rootNode != null)
            {
                RootNode.AddRange(rootNode);
            } ;
            
            RouteTemplates.AddRange(RouteAttribute.GetTemplates(client, property.Name));
            RouteTemplates.AddRange(RoutesAttribute.GetTemplates(client, property.Name));

            ParameterTemplates.AddRange(ParameterAttribute.GetTemplates(client, property.Name));
            ParameterTemplates.AddRange(ParametersAttribute.GetTemplates(client, property.Name));
        }

        /// <summary>
        ///     Fills the settings.
        /// </summary>
        /// <param name="caller">The caller.</param>
        public void Configure(object caller)
        {
            if (BaseUri == null)
            {
                var attribute = caller.GetType().GetCustomAttributes(typeof(BaseUriAttribute), true).FirstOrDefault() as BaseUriAttribute;
                BaseUri = attribute?.BaseUri;
            }

            if (SerializerSettings == null)
            {
                var attribute = caller.GetType().GetCustomAttributes(typeof(SerializerSettingsAttribute), true).FirstOrDefault() as SerializerSettingsAttribute;
                SerializerSettings = attribute?.SerializerSettings;
            }

            RouteTemplates.AddRange(caller.GetType().GetCustomAttributes<RouteAttribute>().Select(attribute => attribute.Template));
            RouteTemplates.AddRange(caller.GetType().GetCustomAttributes<RoutesAttribute>().SelectMany(attribute => attribute.Templates));

            ParameterTemplates.AddRange(caller.GetType().GetCustomAttributes<ParameterAttribute>().Select(attribute => attribute.Template));
            ParameterTemplates.AddRange(caller.GetType().GetCustomAttributes<ParametersAttribute>().SelectMany(attribute => attribute.Templates));
        }
    }
}