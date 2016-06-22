using System.Linq;

namespace NetClient.Rest
{
    /// <summary>
    ///     The NetClient element
    /// </summary>
    /// <typeparam name="T">Type of element.</typeparam>
    public interface IElement<out T> : IQueryable<T>
    {
    }
}