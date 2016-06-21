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
        #region fields and constants

        private readonly Uri baseUri;
        private readonly string pathTemplate;
        private readonly JsonSerializerSettings serializerSettings;

        #endregion

        #region constructors

        public RestQueryProvider(Uri baseUri, string pathTemplate, JsonSerializerSettings serializerSettings)
        {
            this.baseUri = baseUri;
            this.pathTemplate = pathTemplate;
            this.serializerSettings = serializerSettings;
        }

        #endregion

        #region methods and other members

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
            catch (Exception ex)
            {
                return default(TResult);
            }
        }

        #endregion

        #region implementations for IQueryProvider

        public IQueryable CreateQuery(Expression expression)
        {
            return new Element<T>(baseUri, pathTemplate, serializerSettings, expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>) new Element<T>(baseUri, pathTemplate, serializerSettings, expression);
        }

        public object Execute(Expression expression)
        {
            return Execute<Element<T>>(expression);
        }

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

        #endregion
    }
}