using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlazorTasks.Server.Infrastructure
{
    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
    {
        protected const string EqualsOperator = "eq";

        public virtual Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Equals(EqualsOperator, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Invalid oprator {op}");

            return Expression.Equal(left, right);
        }

        public virtual IEnumerable<string> GetOperators()
        {
            yield return EqualsOperator;
        }

        public virtual ConstantExpression GetValue(string input)
            => Expression.Constant(input);
    }
}
