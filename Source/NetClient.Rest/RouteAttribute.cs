using System;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies the route.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class RouteAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="nodes">The nodes.</param>
        public RouteAttribute(string template, string[] parameters = null, string[] nodes = null)
        {
            Route.Templates = new[] {template};
            Route.Parameters = parameters;
            Route.Nodes = nodes;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="node">The node.</param>
        public RouteAttribute(string template, string[] parameters = null, string node = null)
        {
            Route.Templates = new[] {template};
            Route.Parameters = parameters;
            Route.Nodes = new[] {node};
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        public RouteAttribute(string template, params string[] parameters)
        {
            Route.Templates = new[] {template};
            Route.Parameters = parameters;
        }

        /// <summary>
        ///     Gets the route.
        /// </summary>
        /// <value>The route.</value>
        public Route Route { get; } = new Route();
    }
}