using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    /// <summary>
    ///     Class RestElement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestElement<T> : IElement<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RestElement{T}" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="routeTemplate">The route template.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <param name="expression">The expression.</param>
        public RestElement(Uri baseUri, string routeTemplate, JsonSerializerSettings serializerSettings, Expression expression = null)
        {
            Provider = new RestQueryProvider<T>(baseUri, routeTemplate, serializerSettings);
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
    }
}