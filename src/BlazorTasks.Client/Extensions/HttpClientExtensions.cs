using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlazorTasks.Client.Infrastructure;
using BlazorTasks.Client.Models;
using Newtonsoft.Json;

namespace BlazorTasks.Client
{
	public static class HttpClientExtensions
	{

		public static async Task<ApiObjectResponse<TObject>> ApiGetAsync<TObject>(
			this HttpClient httpClient, 
			string requestUri,
			Dictionary<string, string> query = null) where TObject : new()
		{
			var result = await httpClient.GetAsync(QueryHelpers.AddQueryString(requestUri, query));

			TObject resultResponse = default(TObject);
			ApiError error = null;

			if (result.IsSuccessStatusCode)
			{
				var a = await result.Content.ReadAsStringAsync();
				Console.WriteLine(a);
				resultResponse = JsonConvert.DeserializeObject<TObject>(a);;
			}
			else
			{
				error = JsonConvert.DeserializeObject<ApiError>(await result.Content.ReadAsStringAsync());;
			}

			return new ApiObjectResponse<TObject>
			{
				Error = error,
				Response = resultResponse,
				StatusCode = result.StatusCode
			};
		}

		public static async Task<ApiObjectResponse<TObject>> ApiPostAsync<TObject>(this HttpClient httpClient, string requestUri, object content) where TObject : new()
		{
			var myContent = JsonConvert.SerializeObject(content);
			var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
			var byteContent = new ByteArrayContent(buffer);

			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			byteContent.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("charset", "utf-8"));

			var result = await httpClient.PostAsync(requestUri, byteContent);

			TObject resultResponse = default(TObject);
			ApiError error = null;

			if (result.IsSuccessStatusCode)
			{
				var a = await result.Content.ReadAsStringAsync();
				resultResponse = JsonConvert.DeserializeObject<TObject>(a);
			}
			else
			{
				error = JsonConvert.DeserializeObject<ApiError>(await result.Content.ReadAsStringAsync());;
			}

			return new ApiObjectResponse<TObject>
			{
				Error = error,
				Response = resultResponse,
				StatusCode = result.StatusCode
			};
		}

		public static async Task<ApiResponse> ApiUpdateAsync(this HttpClient httpClient, string requestUri, object content)
		{
			var myContent = JsonConvert.SerializeObject(content);
			var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
			var byteContent = new ByteArrayContent(buffer);

			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			byteContent.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("charset", "utf-8"));

			var result = await httpClient.PutAsync(requestUri, byteContent);

			ApiError error = null;

			if (!result.IsSuccessStatusCode)
			{
				error = JsonConvert.DeserializeObject<ApiError>(await result.Content.ReadAsStringAsync());;
			}

			return new ApiResponse
			{
				Error = error,
				StatusCode = result.StatusCode
			};
		}


		public static async Task<ApiResponse> ApiDeleteAsync(this HttpClient httpClient, string requestUri)
		{
			var result = await httpClient.DeleteAsync(requestUri);

			ApiError error = null;

			if (!result.IsSuccessStatusCode)
			{
				error = JsonConvert.DeserializeObject<ApiError>(await result.Content.ReadAsStringAsync());;
			}

			return new ApiResponse
			{
				Error = error,
				StatusCode = result.StatusCode
			};
		}
	}
}