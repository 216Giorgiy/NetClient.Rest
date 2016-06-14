﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.RestApi
{
    internal class RestApiQueryTranslator : ExpressionVisitor
    {
        private readonly IDictionary<string, object> resourceValues = new Dictionary<string, object>();

        internal IDictionary<string, object> GetResourceValues(Expression expression)
        {
            Visit(expression);

            return resourceValues;
        }

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
    }
}