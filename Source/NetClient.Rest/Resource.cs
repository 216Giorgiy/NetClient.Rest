using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetClient.Rest
{
    /// <summary>
    ///     The RestClient Element.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <typeparam name="TCriteria">The criteria type.</typeparam>
    public class Resource<T, TCriteria> : Resource<T>, IElement<T, TCriteria> where TCriteria : new()
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="onError">The on error.</param>
        /// <param name="expression">The expression.</param>
        public Resource(INetClient client, ResourceSettings settings, Action<Exception> onError, Expression expression) : base(client, settings, onError, expression)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public Resource(ResourceSettings settings) : base(settings)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        ///     Gets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        public TCriteria Criteria { get; }
    }

    /// <summary>
    ///     The RestClient Element.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public class Resource<T> : IElement<T>
    {
        private Action<Exception> onError;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="onError">The on error.</param>
        /// <param name="expression">The expression.</param>
        public Resource(INetClient client, ResourceSettings settings, Action<Exception> onError, Expression expression)
        {
            Client = client;
            Settings = settings;
            Provider = new RestQueryProvider<T>(this);
            OnError = onError;
            Expression = expression ?? Expression.Constant(this);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public Resource(ResourceSettings settings)
        {
            Settings = settings;
            Provider = new RestQueryProvider<T>(this);
            Expression = Expression.Constant(this);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        public Resource()
        {
            if (Settings == null)
            {
                Settings = new ResourceSettings();
            }
            Settings.Configure(this);
            Provider = new RestQueryProvider<T>(this);
            Expression = Expression.Constant(this);
        }

        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public ResourceSettings Settings { get; set; }

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
        public Action<Exception> OnError
        {
            get { return onError ?? Client?.OnError; }
            set { onError = value; }
        }
    }

    /// <summary>
    ///     Represents the REST resource.
    /// </summary>
    public class Resource
    {
        /// <summary>
        ///     Gets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        public static object Criteria => null;
    }
}