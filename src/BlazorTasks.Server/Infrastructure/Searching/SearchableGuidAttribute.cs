using System;
namespace BlazorTasks.Server.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SearchableGuidAttribute : SearchableAttribute
    {
        public SearchableGuidAttribute()
        {
            ExpressionProvider = new GuidSearchExpressionProvider();
        }
    }
}
