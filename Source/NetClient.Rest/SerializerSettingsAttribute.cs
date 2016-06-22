using System;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies the JSON serialization settings.
    /// </summary>
    public class SerializerSettingsAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SerializerSettingsAttribute" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public SerializerSettingsAttribute(Type type)
        {
            Settings = Activator.CreateInstance(type) as JsonSerializerSettings;
        }

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public JsonSerializerSettings Settings { get; }
    }
}