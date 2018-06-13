using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlazorTasks.Client.Data
{
	public class HttpApiClient : HttpClient
	{
		public HttpApiClient()
        {
			BaseAddress = new Uri("http://localhost:7777/api/");
			DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   
			DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
			DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "*");
			DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "*");
			DefaultRequestHeaders.Add("Access-Control-Max-Age", "86400");  
		}
	}
}