using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorTasks.Client.Infrastructure
{
	public static class QueryHelpers
	{
		public static string AddQueryString(string uri, Dictionary<string, string> query)
		{
			if (query == null) return uri;
			var stringBuilder = new StringBuilder();
			string str = "?";
			foreach (var q in query)
			{
				stringBuilder.Append(str + q.Key + "=" + q.Value);
				str = "&";
			}
			return (uri + stringBuilder.ToString());
		}

	}
}