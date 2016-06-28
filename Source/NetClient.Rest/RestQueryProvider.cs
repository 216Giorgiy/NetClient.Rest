using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetClient.Rest
{
    /// <summary>
    ///     Defines methods to create and execute queries.
    /// </summary>
    /// <typeparam name="T">The type of element to query.</typeparam>
    internal class RestQueryProvider<T> : IQueryProvider
    {
        private readonly Resource<T> resource;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RestQueryProvider{T}" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public RestQueryProvider(IElement<T> element)
        {
            resource = element as Resource<T>;
        }

        private static void ReplaceParameterPlaceHolders(IEnumerable<Route> routes, IDictionary<string, object> resourceValues)
        {
            foreach (var route in routes)
            {
                if (route.Parameters == null) continue;

                foreach (var index in Enumerable.Range(0, route.Parameters.Length))
                {
                    route.Parameters[index] = resourceValues.Aggregate(route.Parameters[index], (seed, accumulate) => seed.Replace($"{{{accumulate.Key}}}", accumulate.Value.ToString())).TrimStart('/');
                }
            }
        }

        private static void ReplaceTemplatePlaceHolders(IEnumerable<Route> routes, IDictionary<string, object> resourceValues)
        {
            foreach (var route in routes)
            {
                if (route.Templates == null) continue;

                foreach (var index in Enumerable.Range(0, route.Templates.Length))
                {
                    route.Templates[index] = resourceValues.Aggregate(route.Templates[index], (seed, accumulate) => seed.Replace($"{{{accumulate.Key}}}", accumulate.Value.ToString())).TrimStart('/');
                }
            }
        }

        private bool ContainsPlaceHolder(string item)
        {
            return item.Contains("{") && item.Contains("}");
        }

        private async Task<TResult> GetRestValueAsync<TResult>(Expression expression)
        {
            var result = JsonConvert.DeserializeObject<TResult>("[]");
            var resourceValues = new RestQueryTranslator().GetResourceValues(expression);

            if (resource?.Settings?.BaseUri == null)
            {
                resource?.OnError?.Invoke(new InvalidOperationException("Unable to obtain data from the service because a base URI was not specified."));
                return result;
            }

            if (resource?.Settings?.Routes == null || !resource.Settings.Routes.Any())
            {
                resource?.OnError?.Invoke(new InvalidOperationException("Unable to obtain data from the service because a route was not specified."));
                return result;
            }

            ReplaceTemplatePlaceHolders(resource?.Settings?.Routes, resourceValues);
            ReplaceParameterPlaceHolders(resource?.Settings?.Routes, resourceValues);

            var workingRoute = resource?.Settings?.Routes?.FirstOrDefault(r => r.Templates.Any(t => !ContainsPlaceHolder(t)));

            var path = workingRoute?.Templates?.FirstOrDefault(r => !ContainsPlaceHolder(r));
            if (string.IsNullOrWhiteSpace(path))
            {
                resource.OnError?.Invoke(new InvalidOperationException("Route resolution has failed. You probably failed to provide an appropriate route attribute with valid route templates."));
                return result;
            }

            var parameters = string.Empty;
            parameters = workingRoute.Parameters?.Aggregate(parameters, (seed, accumulate) => ContainsPlaceHolder(accumulate) ? seed : $"{seed}&{accumulate}").TrimStart('&');

            var uriString = $"{resource.Settings?.BaseUri.AbsoluteUri}{path}";
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                uriString = $"{uriString}?{parameters}";
            }

            using (var client = new HttpClient())
            {
                var requestUri = new Uri(uriString);
                using (var response = await client.GetAsync(requestUri))
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        if (string.IsNullOrWhiteSpace(responseContent)) return result;

                        if (workingRoute.Nodes != null)
                        {
                            foreach (var node in workingRoute.Nodes)
                            {
                                responseContent = JToken.Parse(responseContent)[node].ToString();
                            }
                        }

                        if (JToken.Parse(responseContent).Type != JTokenType.Array)
                        {
                            responseContent = $"[{responseContent}]";
                        }

                        result = JsonConvert.DeserializeObject<TResult>(responseContent, resource?.Settings?.SerializerSettings);
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///     Creates the query.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>IQueryable.</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return new Resource<T>(resource.Client, resource.Settings, resource.OnError, expression);
        }

        /// <summary>
        ///     Creates the query.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>IQueryable&lt;TElement&gt;.</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>) new Resource<T>(resource.Client, resource.Settings, resource.OnError, expression);
        }

        /// <summary>
        ///     Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.Object.</returns>
        public object Execute(Expression expression)
        {
            return Execute<Resource<T>>(expression);
        }

        /// <summary>
        ///     Executes the specified expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>TResult</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            var result = JsonConvert.DeserializeObject<TResult>("[]");

            GetRestValueAsync<TResult>(expression).ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    result = task.Result;
                }
                else
                {
                    resource.OnError?.Invoke(task.Exception);
                }
            }).Wait();

            return result;
        }
    }
}