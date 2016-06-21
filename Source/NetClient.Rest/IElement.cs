using System.Linq;

namespace NetClient.Rest
{
    public interface IElement<out T> : IQueryable<T>
    {
    }
}