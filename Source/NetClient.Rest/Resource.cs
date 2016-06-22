using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    /// <summary>
    ///     The RestClient Element.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public class Resource<T> : IElement<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="routeTemplate">The route template.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <param name="onError">The on error.</param>
        /// <param name="expression">The expression.</param>
        public Resource(INetClient client, Uri baseUri, string routeTemplate, JsonSerializerSettings serializerSettings, Action<Exception> onError, Expression expression)
        {
            Client = client;
            Provider = new RestQueryProvider<T>(this, baseUri, routeTemplate, serializerSettings);
            OnError = onError;
            Expression = expression ?? Expression.Constant(this);
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
        ///     Gets or sets the error action.
        /// </summary>
        /// <value>The error action.</value>
        public Action<Exception> OnError { get; set; }
    }
}