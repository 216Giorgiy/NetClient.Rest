#region using directives

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace NetClient.Rest
{
    internal class RestQueryTranslator : ExpressionVisitor
    {
        #region fields and constants

        private readonly IDictionary<string, object> resourceValues = new Dictionary<string, object>();

        #endregion

        #region methods and other members

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
                            throw new InvalidOperationException(
                                "A duplicate resource key was used in the query expression.");
                        }
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

        internal IDictionary<string, object> GetResourceValues(Expression expression)
        {
            Visit(expression);

            return resourceValues;
        }

        #endregion
    }
}