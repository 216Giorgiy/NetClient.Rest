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
        /// <param name="resource">The resource.</param>
        public void Configure(IElement resource)
        {
            if (BaseUri == null)
            {
                BaseUri = resource.GetType().GetCustomAttributes<BaseUriAttribute>(true).FirstOrDefault()?.BaseUri;
            }

            if (SerializerSettings == null)
            {
                SerializerSettings = resource.GetType().GetCustomAttributes<SerializerSettingsAttribute>(true).FirstOrDefault()?.SerializerSettings;
            }

            var routes = resource.GetType().GetCustomAttributes<RouteAttribute>(true)?.Select(attribute => attribute.Route);
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
        /// <param name="criteria">The criteria.</param>
        public void Configure(object criteria)
        {
            BaseUri = criteria.GetType().GetCustomAttribute<BaseUriAttribute>(true)?.BaseUri;
            SerializerSettings = criteria.GetType().GetCustomAttribute<SerializerSettingsAttribute>(true)?.SerializerSettings;

            var routes = new List<Route>();
            var parameters = new List<string>();
            foreach (var property in criteria.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(criteria);

                foreach (var attribute in property.GetCustomAttributes<RouteAttribute>(true))
                {
                    routes.Add(attribute.Route);

                    if (propertyValue == null) continue;
                    foreach (var index in Enumerable.Range(0, attribute.Route.Templates.Length))
                    {
                        attribute.Route.Templates[index] = attribute.Route.Templates[index].Replace($"{{{property.Name}}}", propertyValue.ToString());
                    }
                }
                foreach (var attribute in property.GetCustomAttributes<ParameterAttribute>(true))
                {
                    var parameter = attribute.Template;
                    if (propertyValue != null)
                    {
                        parameter = parameter.Replace($"{{{property.Name}}}", propertyValue.ToString());
                    }
                    parameters.Add(parameter);
                }
            }

            foreach (var route in routes)
            {
                route.Parameters = parameters.ToArray();
                Routes.Add(route);
            }
        }
    }
}