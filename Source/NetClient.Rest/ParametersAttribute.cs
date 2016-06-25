using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies multiple parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ParametersAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParametersAttribute" /> class.
        /// </summary>
        /// <param name="templates">The templates.</param>
        public ParametersAttribute(params string[] templates)
        {
            Templates = templates;
        }

        /// <summary>
        ///     Gets the templates.
        /// </summary>
        /// <value>The templates.</value>
        public string[] Templates { get; }

        /// <summary>
        ///     Gets the templates.
        /// </summary>
        /// <param name="caller">The caller.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public static IEnumerable<string> GetTemplates(object caller, [CallerMemberName] string callerMemberName = null)
        {
            if (callerMemberName == null) throw new ArgumentNullException(nameof(callerMemberName));

            var attributes = caller.GetType().GetProperty(callerMemberName).GetCustomAttributes<ParametersAttribute>();
            var templates = new List<string>();
            foreach (var attribute in attributes)
            {
                templates.AddRange(attribute.Templates);
            }
            return templates;
        }
    }
}