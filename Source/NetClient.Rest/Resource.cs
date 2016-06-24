using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    /// <summary>
    /// The RestClient Element.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public class Resource<T> : IElement<T>
    {
        private Action<Exception> onError;
        private string[] routeTemplates;
        private JsonSerializerSettings serializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="property">The property.</param>
        /// <param name="onError">The on error.</param>
        /// <param name="expression">The expression.</param>
        public Resource(INetClient client, PropertyInfo property, Action<Exception> onError, Expression expression)
        {
            Client = client;
            Property = property;
            Provider = new RestQueryProvider<T>(this);
            OnError = onError;
            Expression = expression ?? Expression.Constant(this);
        }

        /// <summary>
        ///     Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Deletes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the base URI.
        /// </summary>
        public Uri BaseUri => (Client as RestClient)?.BaseUri;

        /// <summary>
        ///     Gets or sets the route templates.
        /// </summary>
        /// <value>The route templates.</value>
        public string[] RouteTemplates
        {
            get
            {
                if (routeTemplates != null && routeTemplates.Any())
                {
                    return routeTemplates;
                }

                var routeAttributes = new List<string>();
                var attributes = Property.GetCustomAttributes(typeof(RouteAttribute), true);
                foreach (var attribute in attributes)
                {
                    var routeAttribute = attribute as RouteAttribute;
                    if (routeAttribute != null)
                    {
                        routeAttributes.AddRange(routeAttribute.Templates);
                    }
                }
                return routeAttributes.ToArray();
            }
            set { routeTemplates = value; }
        }

        /// <summary>
        ///     Gets or sets the serializer settings.
        /// </summary>
        /// <value>The serializer settings.</value>
        public JsonSerializerSettings SerializerSettings
        {
            get
            {
                if (serializerSettings != null)
                {
                    return serializerSettings;
                }

                var attribute = Property.GetCustomAttributes(typeof(SerializerSettingsAttribute), true).FirstOrDefault() as SerializerSettingsAttribute;
                if (attribute != null)
                {
                    return attribute.SerializerSettings;
                }

                var client = Client as RestClient;
                return client?.SerializerSettings;
            }
            set { serializerSettings = value; }
        }

        /// <summary>
        ///     Gets the type of the element.
        /// </summary>
        /// <value>The type of the element.</value>
        public Type ElementType => typeof(T);

        /// <summary>
        ///     Gets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public Expression Expression { get; }

        /// <summary>
        ///     Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public IQueryProvider Provider { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator&lt;T&gt;.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public INetClient Client { get; }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyInfo Property { get; }

        /// <summary>
        ///     Gets or sets the error action.
        /// </summary>
        /// <value>The error action.</value>
        public Action<Exception> OnError
        {
            get { return onError ?? Client?.OnError; }
            set { onError = value; }
        }
    }
}