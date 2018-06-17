using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BlazorTasks.Server.Infrastructure
{
    public class GuidSearchExpressionProvider : ComparableSearchExpressionProvider
    {

        private static Regex guidRegEx = new Regex("^[A-Fa-f0-9]{32}$|" +
                          "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                          "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$", RegexOptions.Compiled);


        public override ConstantExpression GetValue(string input)
        {
            if (String.IsNullOrEmpty(input) || !guidRegEx.IsMatch(input))
                throw new ArgumentException("Invalid search value");

            return Expression.Constant(new Guid(input));
        }

    }
}
