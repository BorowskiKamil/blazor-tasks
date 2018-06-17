using System;
using System.Globalization;
using System.Linq.Expressions;

namespace BlazorTasks.Server.Infrastructure
{
    public class DateTimeSearchExpressionProvider : ComparableSearchExpressionProvider
    {


        public override ConstantExpression GetValue(string input)
        {
            if (!DateTimeOffset.TryParse(input, out var parsedDate))
                throw new ArgumentException("Invalid search value");

            return Expression.Constant(parsedDate);
        }

    }
}
