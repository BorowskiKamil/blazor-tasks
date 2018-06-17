using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorTasks.Server.Infrastructure
{
    public class SearchOptionsProcessor<T, TEntity>
    {
        private readonly string[] _searchQuery;

        public SearchOptionsProcessor(string[] searchQuery)
        {
            _searchQuery = searchQuery;
        }

        public IEnumerable<SearchTerm> GetAllTerms()
        {
            if (_searchQuery == null) yield break;

            foreach (var expression in _searchQuery)
            {
                if (string.IsNullOrEmpty(expression)) continue;

                var tokens = expression.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = expression
                    };

                    continue;
                }

                if (tokens.Length < 3)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = tokens[0]
                    };

                    continue;
                }

                yield return new SearchTerm
                {
                    ValidSyntax = true,
                    Name = tokens[0],
                    Operator = tokens[1],
                    Value = string.Join(" ", tokens.Skip(2))
                };
            }
        }

        public IEnumerable<SearchTerm> GetValidTerms()
        {
            var queryTerms = GetAllTerms()
                .Where(t => t.ValidSyntax)
                .ToArray();

            if (!queryTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;

                yield return new SearchTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = declaredTerm.ExpressionProvider
                };

            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var terms = GetValidTerms().ToArray();
            if (!terms.Any()) return query;

            var modifiedQuery = query;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var right = term.ExpressionProvider.GetValue(term.Value);

                var comparisionExpression = term.ExpressionProvider
                                                .GetComparison(left, term.Operator, right);

                var lambdaExpression = ExpressionHelper.GetLambda<TEntity, bool>(obj, comparisionExpression);

                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);
            }

            return modifiedQuery;
        }

        private static IEnumerable<SearchTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                .Select(p => new SearchTerm 
                { 
                    Name = p.Name,
                    ExpressionProvider = p.GetCustomAttribute<SearchableAttribute>().ExpressionProvider
                });

    }
}
