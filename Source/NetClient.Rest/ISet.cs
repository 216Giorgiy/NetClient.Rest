using System.Linq;

namespace NetClient.Rest
{
    public interface ISet<out T> : IOrderedQueryable<T>
    {
    }
}