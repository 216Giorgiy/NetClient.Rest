#region using directives

using System;

#endregion

namespace NetClient.Rest
{
    /// <summary>
    ///     Set attributes required for REST NetClient.
    /// </summary>
    public class RestAttribute : Attribute
    {
        #region constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RestAttribute" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="route">The route.</param>
        /// <param name="serializerSettingsType">The serializer settings type.</param>
        public RestAttribute(string baseUri, string route, Type serializerSettingsType)
        {
            BaseUri = baseUri;
            Route = route;
            SerializerSettingsType = serializerSettingsType;
        }

        #endregion

        #region properties and indexers

        /// <summary>
        ///     Gets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        public string BaseUri { get; }

        /// <summary>
        ///     Gets the route.
        /// </summary>
        /// <value>The route.</value>
        public string Route { get; }

        /// <summary>
        ///     Gets the type of the serializer settings.
        /// </summary>
        /// <value>The type of the serializer settings.</value>
        public Type SerializerSettingsType { get; }

        #endregion
    }
}