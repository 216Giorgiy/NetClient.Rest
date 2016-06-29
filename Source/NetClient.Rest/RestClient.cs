using System;
using System.Diagnostics;
using System.Linq;
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
                if (!IsAssignableToGenericType(property.PropertyType, typeof(IQueryable<>))) continue;

                if (property.GetValue(this) != null) continue;

                object element;
                if (property.PropertyType.IsGenericType)
                {
                    var concreteType = Type.GetType($"{property.Name}Resource", false);
                    if (concreteType != null)
                    {
                        
                    }
                    Debug.WriteLine($"Type!!! {concreteType}");

                    var settings = new ResourceSettings();
                    settings.Configure(this, property);

                    var resourceType = typeof(Resource<>).MakeGenericType(property.PropertyType.GenericTypeArguments);
                    element = Activator.CreateInstance(resourceType, this, settings, null, null);
                }
                else
                {
                    element = Activator.CreateInstance(property.PropertyType);
                }
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

        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            var baseType = givenType.BaseType;

            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }
    }
}