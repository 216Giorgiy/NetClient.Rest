using System;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies the route.
    /// </summary>
    public class RouteAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public RouteAttribute(string template)
        {
            Template = template;
        }

        /// <summary>
        ///     Gets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template { get; }
    }
}