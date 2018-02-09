﻿using System.Linq.Expressions;

namespace Z.EntityFramework.Plus
{
    /// <summary>A batch update visitor.</summary>
    public class BatchUpdateVisitor : ExpressionVisitor
    {
        /// <summary>Gets or sets a value indicating whether the expression contains an OrderBy method.</summary>
        /// <value>true if the expression contains an OrderBy metho has order by, false if not.</value>
        public bool HasOrderBy { get; set; }

        /// <summary>
        /// True if a query is simple and references only one Entity, doesn't include unions etc.
        /// </summary>
        public bool IsSimpleQuery { get; set; } = true;

        /// <summary>
        ///     Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        ///     The modified expression, if it or any subexpression was modified; otherwise, returns the
        ///     original expression.
        /// </returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "OrderBy")
            {
                HasOrderBy = true;
            }
            if (node.Method.Name == "Join" || node.Method.Name == "Concat" || node.Method.Name == "GroupBy")
            {
                IsSimpleQuery = false;
            }

            return base.VisitMethodCall(node);
        }
    }
}