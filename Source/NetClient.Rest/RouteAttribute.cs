using System;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies the route.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class RouteAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="templates">The templates.</param>
        public RouteAttribute(params string[] templates)
        {
            Templates = templates;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public RouteAttribute(string template)
        {
            Templates = new[] {template};
        }

        /// <summary>
        ///     Gets the templates.
        /// </summary>
        /// <value>The templates.</value>
        public string[] Templates { get; }
    }
}