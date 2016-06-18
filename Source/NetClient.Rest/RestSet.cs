using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace NetClient.Rest
{
    public class RestSet<T> : ObservableCollection<T>, ISet<T>
    {
        public RestSet(Uri baseUri, string pathTemplate, JsonSerializerSettings serializerSettings, Expression expression = null)
        {
            Provider = new RestQueryProvider<T>(baseUri, pathTemplate, serializerSettings);
            Expression = expression ?? Expression.Constant(this);
        }

        public Type ElementType => typeof(T);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public new IEnumerator<T> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}