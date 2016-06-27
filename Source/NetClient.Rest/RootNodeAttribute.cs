using System;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies the JSON root node.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class RootNodeAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RootNodeAttribute" /> class.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public RootNodeAttribute(params string[] nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        ///     Gets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        public string[] Nodes { get; }
    }
}