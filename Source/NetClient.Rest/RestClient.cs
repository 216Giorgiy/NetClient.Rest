using System;
using System.Threading.Tasks;

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
            foreach (var property in GetType().GetProperties())
            {
                if (!property.PropertyType.IsGenericType || (property.PropertyType.GetGenericTypeDefinition() != typeof(Resource<>) && property.PropertyType.GetGenericTypeDefinition() != typeof(Resource<,>))) continue;

                var settings = new ResourceSettings();
                settings.Configure(this, property);

                var element = Activator.CreateInstance(property.PropertyType, this, settings, null, null);
                property.SetValue(this, element);
            }
        }

        /// <summary>
        ///     Saves the changes asynchronously.
        /// </summary>
        /// <returns>Task.</returns>
        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets or sets the error action.
        /// </summary>
        /// <value>The error action.</value>
        public Action<Exception> OnError { get; set; }
    }
}