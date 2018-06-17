using System;
using System.Net;

namespace BlazorTasks.Server
{
	public static class HttpStatusCodeExtensions
	{
		public static bool IsSuccessStatusCode(this HttpStatusCode httpWebResponse)
		{
			return ((int)httpWebResponse >= 200) && ((int)httpWebResponse <= 299);
		}
	}
}