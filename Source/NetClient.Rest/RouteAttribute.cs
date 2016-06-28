using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies the route.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class RouteAttribute : Attribute
    {
        private readonly Route route = new Route();

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="nodes">The nodes.</param>
        public RouteAttribute(string template, string[] parameters = null, string[] nodes = null)
        {
            route.Templates = new[] { template };
            route.Parameters = parameters;
            route.Nodes = nodes;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="node">The node.</param>
        public RouteAttribute(string template, string[] parameters = null, string node = null)
        {
            route.Templates = new[] { template };
            route.Parameters = parameters;
            route.Nodes = new[] { node };
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        public RouteAttribute(string template, params string[] parameters)
        {
            route.Templates = new[] { template };
            route.Parameters = parameters;
        }

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

        /// <summary>
        ///     Gets the templates.
        /// </summary>
        /// <param name="caller">The caller.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public static IEnumerable<string> GetTemplates(object caller, [CallerMemberName] string callerMemberName = null)
        {
            if (callerMemberName == null) throw new ArgumentNullException(nameof(callerMemberName));

            var attributes = caller.GetType().GetProperty(callerMemberName).GetCustomAttributes<RouteAttribute>();
            return attributes.Select(attribute => attribute.Template).ToList();
        }
    }
}