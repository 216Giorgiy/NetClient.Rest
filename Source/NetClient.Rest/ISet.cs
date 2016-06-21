#region using directives

using System.Linq;

#endregion

namespace NetClient.Rest
{
    /// <summary>
    /// Interface ISet
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISet<out T> : IOrderedQueryable<T>
    {
    }
}