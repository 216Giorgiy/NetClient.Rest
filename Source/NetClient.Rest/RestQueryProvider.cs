using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    public class RestQueryProvider<T> : IQueryProvider
    {
        private readonly Uri baseUri;
        private readonly string callerMemberName;
        private readonly string pathTemplate;
        private readonly JsonSerializerSettings serializerSettings;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RestQueryProvider{T}" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="pathTemplate">The path template.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        public RestQueryProvider(Uri baseUri, string pathTemplate, JsonSerializerSettings serializerSettings)
        {
            this.baseUri = baseUri;
            this.pathTemplate = pathTemplate;
            this.serializerSettings = serializerSettings;
        }

        private async Task<TResult> GetAsync<TResult>(Uri requestUri)
        {
            try
            {
                TResult result;
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(requestUri))
                    {
                        using (var content = response.Content)
                        {
                            var json = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<TResult>($"[{json}]", serializerSettings);
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return default(TResult);
            }
        }

        /// <summary>
        ///     Creates the query.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>IQueryable.</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return new RestElement<T>(baseUri, pathTemplate, serializerSettings, expression);
        }

        /// <summary>
        ///     Creates the query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>IQueryable&lt;TElement&gt;.</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>) new RestElement<T>(baseUri, pathTemplate, serializerSettings, expression);
        }

        /// <summary>
        ///     Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.Object.</returns>
        public object Execute(Expression expression)
        {
            return Execute<RestElement<T>>(expression);
        }

        /// <summary>
        ///     Executes the specified expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>TResult</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            var resourceValues = new RestQueryTranslator().GetResourceValues(expression);
            var path = resourceValues.Aggregate(pathTemplate,
                (current, resourceValue) => current.Replace($"{{{resourceValue.Key}}}", resourceValue.Value.ToString()));

            var requestUri = new Uri($"{baseUri.AbsoluteUri}{path}");
            var result = default(TResult);
            GetAsync<TResult>(requestUri).ContinueWith(task => { result = task.Result; }).Wait();
            return result;
        }
    }
}