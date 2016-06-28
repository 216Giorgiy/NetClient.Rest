using System;

namespace NetClient.Rest
{
    /// <summary>
    ///     Specifies a URI parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParameterAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterAttribute" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public ParameterAttribute(string template)
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