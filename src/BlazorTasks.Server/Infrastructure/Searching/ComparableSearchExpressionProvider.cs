using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorTasks.Server.Infrastructure
{
    public abstract class ComparableSearchExpressionProvider : DefaultSearchExpressionProvider
    {

        private const string GreaterThanOperator = "gt";
        private const string GreaterThanOrEqualOperator = "gte";
        private const string LessThanOperator = "lt";
        private const string LessThanOrEqualOperator = "lte";

        public override IEnumerable<string> GetOperators()
            => base.GetOperators()
            .Concat(new[]
            {
                GreaterThanOperator,
                GreaterThanOrEqualOperator,
                LessThanOperator,
                LessThanOrEqualOperator
            });

        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            switch (op.ToLower())
            {
                case GreaterThanOperator: return Expression.GreaterThan(left, right);
                case GreaterThanOrEqualOperator: return Expression.GreaterThanOrEqual(left, right);
                case LessThanOperator: return Expression.LessThan(left, right);
                case LessThanOrEqualOperator: return Expression.LessThanOrEqual(left, right);
                default: return base.GetComparison(left, op, right);
            }
        }

    }
}
