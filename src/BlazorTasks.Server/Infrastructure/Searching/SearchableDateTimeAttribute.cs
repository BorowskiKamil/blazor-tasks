using System;
namespace BlazorTasks.Server.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SearchableDateTimeAttribute : SearchableAttribute
    {
        public SearchableDateTimeAttribute()
        {
            ExpressionProvider = new DateTimeSearchExpressionProvider();
        }
    }
}
