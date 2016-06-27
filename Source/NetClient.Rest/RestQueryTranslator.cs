using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NetClient.Rest
{
    /// <summary>
    ///     The query translator.
    /// </summary>
    internal class RestQueryTranslator : ExpressionVisitor
    {
        private readonly IDictionary<string, object> resourceValues = new Dictionary<string, object>();

        /// <summary>
        ///     Translates binary nodes.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>Expression.</returns>
        /// <exception cref="InvalidOperationException">
        ///     A duplicate resource key was used in the query expression.
        ///     or
        ///     An invalid expression type was used in the query expression.
        /// </exception>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    var name = (node.Left as MemberExpression)?.Member.Name;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        if (resourceValues.ContainsKey(name))
                        {
                            throw new InvalidOperationException("A duplicate resource key was used in the query expression.");
                        }
                        //var value = (node.Right as MemberExpression);
                        //var y = x.Update();
                        //var y = Expression.Lambda(x.Object).Compile().DynamicInvoke();
                        //var y = x.Update(x.Object, x.Arguments);
                        resourceValues.Add(name, (node.Right as ConstantExpression)?.Value);
                    }
                    break;
                case ExpressionType.AndAlso:
                    break;
                default:
                    throw new InvalidOperationException("An invalid expression type was used in the query expression.");
            }

            return base.VisitBinary(node);
        }

        /// <summary>
        ///     Gets the resource values.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>IDictionary&lt;System.String, System.Object&gt;.</returns>
        internal IDictionary<string, object> GetResourceValues(Expression expression)
        {
            Visit(expression);
            return resourceValues;
        }
    }
}