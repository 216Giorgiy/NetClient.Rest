using System;
using System.Collections.Generic;
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
        private readonly string pathTemplate;
        private readonly JsonSerializerSettings serializerSettings;

        public RestQueryProvider(Uri baseUri, string pathTemplate, JsonSerializerSettings serializerSettings)
        {
            this.baseUri = baseUri;
            this.pathTemplate = pathTemplate;
            this.serializerSettings = serializerSettings;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new RestSet<T>(baseUri, pathTemplate, serializerSettings, expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>)new RestSet<T>(baseUri, pathTemplate, serializerSettings, expression);
        }

        public object Execute(Expression expression)
        {
            return Execute<RestSet<T>>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var resourceValues = new RestQueryTranslator().GetResourceValues(expression);
            var path = resourceValues.Aggregate(pathTemplate, (current, resourceValue) => current.Replace($"{{{resourceValue.Key}}}", resourceValue.Value.ToString()));

            var elements = new List<TestElement>
            {
                new TestElement { Id = 1, Name = "111", Sort = 10 },
                new TestElement { Id = 2, Name = "222", Sort = 20 },
                new TestElement { Id = 3, Name = "333", Sort = 30 },
            }.AsQueryable();

            var requestUri = new Uri($"{baseUri.AbsoluteUri}{path}");
            var result = default(TResult);
            GetAsync<TResult>(requestUri).ContinueWith(task =>
            {
                result = task.Result;
            }).Wait();

            return result;
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
                            result = JsonConvert.DeserializeObject<TResult>(json, serializerSettings);
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
    }
}